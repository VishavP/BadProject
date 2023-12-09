using BadProject.DataModels;
using System;
using System.Collections.Generic;

namespace BadProject.Interfaces
{
    public interface IErrorProvider
    {
        IEnumerable<Error> GetErrorsByMinDate(DateTime minDate);
        Queue<Error> GetErrors();
        void AddError(Error error);
    }
}
