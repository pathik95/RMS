using RMS.BAL;
using RMS.Utilities;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RMS.Service.Filters
{
    public class AuthenticateUser : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count > 0)
            {
                return;
            };

            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string token = actionContext.Request.Headers.Authorization.Parameter;
                string decodedString = CryptoHelper.Decrypt(token, Constants.ENCRYPTION_KEY);
                string[] tokensValues = decodedString.Split(Constants.TOKEN_SEPARATOR);

                var roleName = new UserBL().GetUserRoles(tokensValues[0], tokensValues[1]);

                if (roleName != null)
                {
                    IPrincipal principal = new GenericPrincipal(new GenericIdentity(tokensValues[0]), new string[] { roleName });
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                }
                else
                {
                    //The user is unauthorize and return 401 status  
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
            //}
        }
    }
}