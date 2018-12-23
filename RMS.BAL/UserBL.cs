using RMS.DAL;
using RMS.Utilities;
using RMS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BAL
{
    public class UserBL
    {
        /// <summary>
        /// Method to verify user credentials in database
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public UserDetailsViewModel CheckUserCredentials(string userName, string password)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var userDetails = context.USP_AuthenticateUser(userName, password).Select(
                    u => new UserDetailsViewModel()
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        EmailId = u.EmailId,
                        RoleName = u.RoleName,
                        DisplayName = u.DisplayName,
                        AuthorizationToken = CryptoHelper.Encrypt(String.Concat(userName, Constants.TOKEN_SEPARATOR, password), Constants.ENCRYPTION_KEY)
                    }).SingleOrDefault();

                return userDetails;
            }
        }

        /// <summary>
        /// To get userid from username
        /// </summary>
        /// <param name="userName">username</param>
        /// <returns></returns>
        public int? GetUserId(string userName)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var userId = context.UserMaster.Where(s => s.UserName == userName).Select(s => s.UserId).SingleOrDefault();
                return userId;
            }
        }

        public string GetUserRoles(string userName, string password)
        {
            using (RMSDbContext context = new RMSDbContext())
            {
                var roleName = context.USP_AuthenticateUser(userName, password).Select(s => s.RoleName).SingleOrDefault();
                return roleName;
            }
        }
    }
}