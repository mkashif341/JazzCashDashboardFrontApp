using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseHandler.DTO
{
    public class ResponseDTO
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public object? result { get; set; } = null;
    }
}
