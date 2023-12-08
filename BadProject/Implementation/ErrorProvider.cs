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
        private static Queue<DateTime> _errors = new Queue<DateTime>();
        public ErrorProvider()
        {
            _errors = new Queue<DateTime>();
        }
        public IEnumerable<DateTime> GetErrorsByMaxDate(DateTime maxDate)
        {
            return _errors.Where(err => err > maxDate);
        }

        public void AddError(DateTime date)
        {
            _errors.Enqueue(date);
        }

        public Queue<DateTime> GetErrors()
        {
            return _errors;
        }
    }
}
