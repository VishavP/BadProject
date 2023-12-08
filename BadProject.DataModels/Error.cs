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
        public DateTime DateTime { get; set; }
        public Error(string Message, DateTime dateTime) 
        {
            this.Message = Message;
            this.DateTime = dateTime;
        }

    }
}
