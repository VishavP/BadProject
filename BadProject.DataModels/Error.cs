using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadProject.DataModels
{
    public sealed class Error
    {
        public string Message { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public Error(string Message, DateTimeOffset dateTime) 
        {
            this.Message = Message;
            this.DateTime = dateTime;
        }

    }
}
