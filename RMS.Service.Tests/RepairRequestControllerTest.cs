using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMS.Service.Controllers;
using RMS.ViewModel;
using System.Transactions;

namespace RepairTracker.Tests
{
    [TestClass]
    public class RepairRequestControllerTest
    {
        private readonly RepairRequestController repairRequestController;

        public RepairRequestControllerTest()
        {
            repairRequestController = new RepairRequestController();
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
        public void CreateRequestTest()
        {
            var createRequestViewModel = new CreateRequestViewModel()
            {

                CityId = 1,
                CompanyName = "HP",
                CreatedBy = 1,
                CustomerAddress = "b/1 test appartment",
                CustomerContact = "0",
                CustomerName = "Test User",
                Expense = 10000,
                ModelNumber = "Envy 15",
                PinCode = "380011",
                ProblemDetails = "My laptop is not working",
                StateId = 1
            };

            //Modal state fail test 
            var response = repairRequestController.CreateRequest(createRequestViewModel);
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

            createRequestViewModel.CustomerContact = "9737669480";

            //Try after entering all valid values
            response = repairRequestController.CreateRequest(createRequestViewModel);
            Assert.IsInstanceOfType(response, typeof(OkResult));

        }

        [TestMethod]
        public void GetRequestDetailsTest()
        {
            //Request Id exits in database 
            var response = repairRequestController.GetRequestDetails(1);
            var contentResult = response as JsonResult<RequestDetailsViewModel>;

            Assert.AreEqual(1, contentResult.Content.RequestId);
            Assert.AreEqual("HP", contentResult.Content.CompanyName);
            Assert.AreEqual("ENVY 15", contentResult.Content.ModelNumber);
            Assert.AreEqual("1000", contentResult.Content.Expense);
            Assert.AreEqual("My laptop is not working", contentResult.Content.ProblemDetails);
            Assert.AreEqual("9737669480", contentResult.Content.CustomerContact);
            Assert.AreEqual("", contentResult.Content.CustomerName);
            Assert.AreEqual("ClerkU1", contentResult.Content.CreatedBy);
            Assert.AreEqual("19-Jan-2018", contentResult.Content.CreatedOn);

            //RequestId do not exists in database
            response = repairRequestController.GetRequestDetails(1);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));

        }

        [TestMethod]
        public void CompleteRequestTest()
        {
            var updateStatus = new UpdateRequestStatusViewModel()
            {
                Comment = "Request completed",
                RequestId = 1,
                UserId = 3
            };

            var response = repairRequestController.CompleteRequest(updateStatus);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void RejectRequestTest()
        {
            var updateStatus = new ApprovalViewModel()
            {
                Comment = "Request Rejected",
                RequestId = 1,
                IsApproved = false,
                UserId = 4
            };

            var response = repairRequestController.ApproveorRejectRequest(updateStatus);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void AcceptRequestTest()
        {
            CompleteRequestTest();
            var updateStatus = new ApprovalViewModel()
            {
                Comment = "Request Approved",
                RequestId = 1,
                IsApproved = true,
                UserId = 4
            };

            var response = repairRequestController.ApproveorRejectRequest(updateStatus);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }


        [TestMethod]
        public void CloseRequestTest()
        {
            var updateStatus = new ApprovalViewModel()
            {
                Comment = "Request Closed",
                RequestId = 1,
                IsApproved = true,
                UserId = 5
            };

            var response = repairRequestController.ApproveorRejectRequest(updateStatus);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void GetCitiesTest()
        {
            //For state id 1
            var response = repairRequestController.GetCities(1);
            var contentResult = response as JsonResult<List<CityDropDownViewModel>>;

            Assert.AreEqual(2, contentResult.Content.Count);

            //For state id 2
            response = repairRequestController.GetCities(2);
            contentResult = response as JsonResult<List<CityDropDownViewModel>>;

            Assert.AreEqual(1, contentResult.Content.Count);

            //For state id 5 which is not in dbs
            response = repairRequestController.GetCities(5);
            contentResult = response as JsonResult<List<CityDropDownViewModel>>;

            Assert.AreEqual(0, contentResult.Content.Count);
        }

        [TestMethod]
        public void GetStateTest()
        {
            //For state id 1
            var response = repairRequestController.GetStates();
            var contentResult = response as JsonResult<List<StateDropDownViewModel>>;

            Assert.AreEqual(2, contentResult.Content.Count);
            Assert.AreEqual(1, contentResult.Content.First().StateId);
            Assert.AreEqual("Gujarat", contentResult.Content.First().StateName);
            Assert.AreEqual("Maharastra", contentResult.Content[2].StateName);
        }
    }
}
