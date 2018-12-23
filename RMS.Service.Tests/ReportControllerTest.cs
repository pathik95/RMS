using System;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepairTracker.Controllers;
using RMS.ViewModel;

namespace RepairTracker.Tests
{
    [TestClass]
    public class ReportControllerTest
    {

        private readonly ReportController reportController;

        public ReportControllerTest()
        {
            reportController = new ReportController();
        }

        [TestMethod]
        public void GetReportDetailsTest()
        {
            var response = reportController.GetReportDetails(1);
            var contentResult = response as JsonResult<ReportViewModel>;

            Assert.AreEqual(1, (int)contentResult.Content.AverageResponseTime);
            Assert.AreEqual(contentResult.Content.DoughnutChartDetails.ChartLable.Count, contentResult.Content.DoughnutChartDetails.ChartData.Count);
            Assert.IsTrue(contentResult.Content.DoughnutChartDetails.ChartLable.Contains("Pending"));
            Assert.IsTrue(contentResult.Content.DoughnutChartDetails.ChartLable.Contains("Completed"));
            Assert.IsTrue(contentResult.Content.DoughnutChartDetails.ChartLable.Contains("Approved"));
            Assert.IsTrue(contentResult.Content.DoughnutChartDetails.ChartLable.Contains("Closed"));
            Assert.AreEqual(contentResult.Content.BarChartDetails.ChartLable.Count, contentResult.Content.BarChartDetails.ChartData.Count);
        }
    }
}
