using System;
using System.Web;
using Elmah;

namespace App.Models.Extensions
{
    public static class ExceptionExtensions
    {
        public static void Log(this Exception ex)
        {
            if (HttpContext.Current == null)
            {
                var errorlog = ErrorLog.GetDefault(null);
                var error = new Error
                {
                    Message = ex.Message,
                    Detail = ex.ToString(),
                    Time = DateTime.Now,
                    Type = ex.GetType() + "[NoContext]",
                    HostName = Environment.MachineName
                };
                errorlog.Log(error);
            }
            else
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
    }
}