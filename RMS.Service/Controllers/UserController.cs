using RMS.BAL;
using RMS.ViewModel;
using System.Web.Http;

namespace RMS.Service.Controllers
{
    public class UserController : ApiController
    {
        private UserBL userBL;

        public UserController()
        {
            userBL = new UserBL();
        }

        /// <summary>
        /// Method to authenticate user 
        /// </summary>
        /// <param name="loginViewModel">contains username and password entered by user</param>
        /// <returns></returns>
        [AllowAnonymous]
        [ActionName("Login")]
        [HttpPost]
        public IHttpActionResult CheckUserCredentials(LoginViewModel loginViewModel)
        {
            var userDetails = userBL.CheckUserCredentials(loginViewModel.UserName, loginViewModel.Password);
            if (userDetails != null)
                return Json(userDetails);
            else
                return Unauthorized();
        }
    }
}
