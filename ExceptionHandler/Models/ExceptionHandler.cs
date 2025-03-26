using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler.Models
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message = "Content Not Found") : base(message) { }
    }
    public class SuccessWithErrorException : Exception
    {
        public SuccessWithErrorException(string? message = null) : base(message) { }
    }
    public class BadRequestException : Exception
    {
        public BadRequestException(string? message = null) : base(message) { }
    }
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string? message = null) : base(message) { }
    }
}
