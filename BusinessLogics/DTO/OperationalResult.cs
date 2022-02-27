using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogics.DTO
{
    public class OperationalResult
    {
        //True or False status of operation
        public bool Status { get; set; } = false;
        //Single messsage from operation
        public string Message { get; set; }
        //Result of successfully operation
        public List<object> Data { get; set; } = new List<object>();
        //error list of failed operation
        public List<Error> ErrorList { get; set; }
    }

    public class Error
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
