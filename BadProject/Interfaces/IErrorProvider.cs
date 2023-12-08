using BadProject.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadProject.Interfaces
{
    public interface IErrorProvider
    {
        IEnumerable<Error> GetErrorsByMinDate(DateTime minDate);
        Queue<Error> GetErrors();
        void AddError(Error error);
    }
}
