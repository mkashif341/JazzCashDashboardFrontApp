using ExceptionHandler.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResponseHandler.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ResponseHandler.Middleware
{
    public class ResponseHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            try
            {
                await _next(context);

                if (!context.Response.HasStarted)
                {

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    var responseBodyString = await new StreamReader(responseBodyStream).ReadToEndAsync();
                    var responseBody = /*responseBodyString;*/
                        JsonConvert.DeserializeObject<JToken>(responseBodyString);

                    if (responseBody is null)
                        throw new NotFoundException();

                    var response = new ResponseDTO
                    {
                        status = "Success",
                        message = "Requested entity has been found",
                        result = responseBody/*Encrypt(responseBody, responseEncryption.SaltKey)*/
                    };

                    context.Response.Body = originalBodyStream;
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status200OK;

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
                else
                {
                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    await responseBodyStream.CopyToAsync(originalBodyStream);
                }
            }
            catch (Exception ex)
            {
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseDto = new ResponseDTO { status = "Error", message = "An Unexpected Error Occurred" };
                int statusCode = StatusCodes.Status500InternalServerError;

                switch (ex)
                {
                    case NotFoundException nf:
                        responseDto = new ResponseDTO { status = "Failure", message = nf.Message };
                        statusCode = StatusCodes.Status200OK;
                        break;

                    case SuccessWithErrorException swee:
                        responseDto = new ResponseDTO { status = "Failure", message = swee.Message };
                        statusCode = StatusCodes.Status200OK;
                        break;

                    case BadRequestException br:
                        responseDto = new ResponseDTO { status = "Error", message = br.Message };
                        statusCode = StatusCodes.Status400BadRequest;
                        break;

                    case UnAuthorizedException uae:
                        responseDto = new ResponseDTO { status = "Error", message = uae.Message };
                        statusCode = StatusCodes.Status401Unauthorized;
                        break;

                    case NotImplementedException nie:
                        responseDto = new ResponseDTO { status = "Error", message = "oops someone forgot the implementation of method" };
                        statusCode = StatusCodes.Status401Unauthorized;
                        break;

                    default:
                        context.Response.Headers.TryGetValue("traceId", out var traceId);
                        responseDto = new ResponseDTO
                        {
                            status = "Error",
                            message = "An Unexpected Error Occurred",
                            result = new { traceId = traceId.FirstOrDefault()?.ToString() }
                        };
                        break;
                }

                context.Response.Body = originalBodyStream;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(responseDto));
            }

            //finally
            //{
            //    context.Response.Body = originalBodyStream;
            //}
        }
        private string Encrypt(string plainText, string key)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.GenerateIV();
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(aes.IV, 0, aes.IV.Length); // Prepend IV to the encrypted data
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (var sw = new StreamWriter(cs))
                            {
                                sw.Write(plainText);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        //private async Task WriteResponseAsync(HttpContext context, int statusCode, string status, string message, object data)
        //{
        //    object response;

        //    if (statusCode == StatusCodes.Status500InternalServerError)
        //    {
        //        context.Response.Headers.TryGetValue("traceId", out var traceId);

        //        response = new
        //        {
        //            StatusCode = statusCode,
        //            status = status,
        //            message = message,
        //            TraceId = traceId.FirstOrDefault()?.ToString()
        //        };
        //    }

        //    else
        //    {
        //        response = new ResponseDTO
        //        {
        //            StatusCode = statusCode,
        //            status = status,
        //            message = message,
        //            Data = data
        //        };
        //    }

        //    context.Response.ContentType = "application/json";
        //    context.Response.StatusCode = statusCode;
        //    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));

        //}

    }
}
