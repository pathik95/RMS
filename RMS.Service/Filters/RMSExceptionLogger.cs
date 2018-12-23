using RMS.BAL;
using RMS.DAL;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace RMS.Service.Filters
{
    public class RMSExceptionLogger : ExceptionLogger
    {
        private LogService logService;

        public RMSExceptionLogger()
        {
            logService = new LogService();
        }

        public override void Log(ExceptionLoggerContext context)
        {
            base.Log(context);

            Task t = logService.LogError(CreateErrorInfo(context));
            t.Wait(-1);
        }

        private ErrorInfo CreateErrorInfo(ExceptionLoggerContext exceptionLoggerContext)
        {
            ErrorInfo errorInfo = new ErrorInfo();
            errorInfo.Source = exceptionLoggerContext.Exception.Source;
            errorInfo.Message = exceptionLoggerContext.Exception.Message;
            errorInfo.StackTrace = exceptionLoggerContext.Exception.StackTrace;
            errorInfo.InnerException = exceptionLoggerContext.Exception.InnerException == null ? string.Empty : exceptionLoggerContext.Exception.InnerException.ToString();
            //exceptionLoggerContext.RequestContext.Principal.Identity.Name
            errorInfo.Caller = HttpContext.Current.User.Identity.Name;
            errorInfo.LogTime = DateTime.Now;
            errorInfo.RequestURI = exceptionLoggerContext.Request.RequestUri.ToString();
            return errorInfo;
        }

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            return logService.LogError(CreateErrorInfo(context));
        }

    }
}