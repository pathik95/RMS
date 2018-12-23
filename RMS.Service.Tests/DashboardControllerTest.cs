using System;
using System.Transactions;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMS.Service.Controllers;
using RMS.ViewModel;

namespace RepairTracker.Tests
{
    [TestClass]
    public class DashboardControllerTest
    {

        private readonly DashboardController dashboardController;

        public DashboardControllerTest()
        {
            dashboardController = new DashboardController();
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
        public void GetMyRequestTest()
        {
            var response = dashboardController.GetMyRequests(1, 1);
            var contentResult = response as JsonResult<DashboardViewModel>;

            Assert.AreEqual(10, contentResult.Content.ItemsPerPage);
            Assert.AreEqual(7, contentResult.Content.TotalRequestsCount);
            Assert.AreEqual(7, contentResult.Content.RequestsList.Count);
            Assert.AreEqual(1, contentResult.Content.RequestsList[0].RequestId);
            Assert.AreEqual("HP", contentResult.Content.RequestsList[0].CompanyName);
            Assert.AreEqual("Inspiron 15", contentResult.Content.RequestsList[1].ModelNumber);
            Assert.AreEqual("Pathik Patel", contentResult.Content.RequestsList[1].CustomerName);
            Assert.AreEqual("Closed", contentResult.Content.RequestsList[0].Status);
            Assert.AreEqual("19-Dec-2018", contentResult.Content.RequestsList[0].CreatedOn);

            //fOR PAGE NO 2
            response = dashboardController.GetMyRequests(1, 2);
            contentResult = response as JsonResult<DashboardViewModel>;

            Assert.AreEqual(10, contentResult.Content.ItemsPerPage);
            Assert.AreEqual(7, contentResult.Content.TotalRequestsCount);
            Assert.AreEqual(0, contentResult.Content.RequestsList.Count);

            //for user id 2
            response = dashboardController.GetMyRequests(2, 1);
            contentResult = response as JsonResult<DashboardViewModel>;

            Assert.AreEqual(10, contentResult.Content.ItemsPerPage);
            Assert.AreEqual(0, contentResult.Content.TotalRequestsCount);
            Assert.AreEqual(0, contentResult.Content.RequestsList.Count);


        }

        [TestMethod]
        public void GetPendingRequestTest()
        {
            var response = dashboardController.GetPendingRequests(1);
            var contentResult = response as JsonResult<DashboardViewModel>;


            Assert.AreEqual(10, contentResult.Content.ItemsPerPage);
            Assert.AreEqual(2, contentResult.Content.TotalRequestsCount);
            Assert.AreEqual(2, contentResult.Content.RequestsList.Count);

            response = dashboardController.GetPendingRequests(2);
            contentResult = response as JsonResult<DashboardViewModel>;


            Assert.AreEqual(10, contentResult.Content.ItemsPerPage);
            Assert.AreEqual(2, contentResult.Content.TotalRequestsCount);
            Assert.AreEqual(0, contentResult.Content.RequestsList.Count);

        }

        [TestMethod]
        public void GetCompletedRequestTest()
        {
            var response = dashboardController.GetCompletedRequests(1);
            var contentResult = response as JsonResult<DashboardViewModel>;


            Assert.AreEqual(10, contentResult.Content.ItemsPerPage);
            Assert.AreEqual(2, contentResult.Content.TotalRequestsCount);
            Assert.AreEqual(2, contentResult.Content.RequestsList.Count);

            response = dashboardController.GetCompletedRequests(2);
            contentResult = response as JsonResult<DashboardViewModel>;


            Assert.AreEqual(10, contentResult.Content.ItemsPerPage);
            Assert.AreEqual(2, contentResult.Content.TotalRequestsCount);
            Assert.AreEqual(0, contentResult.Content.RequestsList.Count);
        }

        [TestMethod]
        public void GetApprovedRequestTest()
        {
            var response = dashboardController.GetApprovedRequests(1);
            var contentResult = response as JsonResult<DashboardViewModel>;


            Assert.AreEqual(10, contentResult.Content.ItemsPerPage);
            Assert.AreEqual(1, contentResult.Content.TotalRequestsCount);
            Assert.AreEqual(1, contentResult.Content.RequestsList.Count);

            response = dashboardController.GetApprovedRequests(2);
            contentResult = response as JsonResult<DashboardViewModel>;


            Assert.AreEqual(10, contentResult.Content.ItemsPerPage);
            Assert.AreEqual(1, contentResult.Content.TotalRequestsCount);
            Assert.AreEqual(0, contentResult.Content.RequestsList.Count);
        }

    }
}
