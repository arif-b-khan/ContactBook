using System;

namespace ContactBook.WebApi.Controllers.Helpers
{
    public class ApiHelper
    {
        public static bool TryExecuteContext(Action contextExecution, out Exception exception)
        {
            exception = null;
            bool status = true;
            try
            {
                contextExecution();
            }
            catch (Exception ex)
            {
                status = false;
                exception = ex;
            }
            return status;
        }
    }
}