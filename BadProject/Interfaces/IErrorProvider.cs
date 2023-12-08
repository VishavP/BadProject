using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadProject.Interfaces
{
    public interface IErrorProvider
    {
        IEnumerable<DateTime> GetErrorsByMaxDate(DateTime maxDate);
        Queue<DateTime> GetErrors();
        void AddError(DateTime date);
    }
}
