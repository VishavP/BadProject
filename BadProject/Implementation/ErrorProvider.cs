using BadProject.DataModels;
using BadProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadProject.Implementation
{
    public class ErrorProvider : IErrorProvider
    {
        private Queue<Error> _errors = new Queue<Error>();
        public ErrorProvider()
        {
            _errors = new Queue<Error>();
        }
        public IEnumerable<Error> GetErrorsByMinDate(DateTime minDate)
        {
            return _errors.Where(err => err.DateTime > minDate);
        }

        public void AddError(Error error)
        {
            _errors.Enqueue(error);
        }

        public Queue<Error> GetErrors()
        {
            return _errors;
        }

        public void ClearErrors() 
        {
            this._errors.Clear();
        }
    }
}
