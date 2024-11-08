using System;

namespace SMSC.Core.Exceptions
{
    public class StoredProcedureExecutionException : Exception
    {
        public StoredProcedureExecutionException(string errorMessage) : base($"Error Executing Stored Procedure. Error : {errorMessage}")
        {
            
        }
    }
}
