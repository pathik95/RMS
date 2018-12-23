using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.ViewModel
{
    public class ReportViewModel
    {
        public ChartsViewModel DoughnutChartDetails { get; set; }
        public ChartsViewModel BarChartDetails { get; set; }
        public double? AverageResponseTime { get; set; }
    }

    public class ChartsViewModel
    {
        public List<string> ChartLable { get; set; }
        public List<int> ChartData { get; set; }
    }

    public class ChartItemViewModel
    {
        public string Lable { get; set; }
        public int Value { get; set; }
    }
}
