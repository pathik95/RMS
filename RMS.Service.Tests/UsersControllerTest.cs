using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMS.Service.Controllers;
using RMS.ViewModel;
using System.Web.Http.Results;
using System.Transactions;

namespace RepairTracker.Tests
{
    [TestClass]
    public class UsersControllerTest
    {
        private readonly UserController userController;

        public UsersControllerTest()
        {
            userController = new UserController();
        }

        TransactionScope _transScope;
        #region Additional test attributes


        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _transScope = new TransactionScope();
        }
        //
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            _transScope.Dispose();
        }
        //
        #endregion

        [TestMethod]
        public void LoginSuccessTest()
        {
            var loginViewModel = new LoginViewModel()
            {
                UserName = "ClerkU1",
                Password = "clerkPassword1",
            };

            var response = userController.CheckUserCredentials(loginViewModel);
            var contentResult = response as JsonResult<UserDetailsViewModel>;

            Assert.AreEqual(1, contentResult.Content.UserId);
            Assert.AreEqual("ClerkU1", contentResult.Content.UserName);
            Assert.AreEqual("Clerk", contentResult.Content.RoleName);
        }

        [TestMethod]
        public void LoginWrongCredentialsTest()
        {
            var loginViewModel = new LoginViewModel()
            {
                UserName = "ClerkU1",
                Password = "wrongPassword",
            };

            var response = userController.CheckUserCredentials(loginViewModel);
            Assert.IsInstanceOfType(response, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void LoginModalwithEmptyUsernamePassword()
        {
            var loginViewModel = new LoginViewModel()
            {

            };
            var response = userController.CheckUserCredentials(loginViewModel);
            Assert.IsInstanceOfType(response, typeof(UnauthorizedResult));
        }
    }
}
