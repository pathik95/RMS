using RMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BAL
{

    public class LogService
    {
        private readonly RMSDbContext context;

        public LogService()
        {
            context = new RMSDbContext();
        }

        public async Task LogError(ErrorInfo errorInformation)
        {
            context.ErrorInfo.Add(errorInformation);
            await context.SaveChangesAsync();
        }

        public void LogErrorInSync(ErrorInfo errorInformation)
        {
            context.ErrorInfo.Add(errorInformation);
            context.SaveChanges();
        }
    }

}
