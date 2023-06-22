using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintMAUI.Pages
{
    public class PrintRequest
    {
        public String ReportName { get; set; } = "";
        public List<RepParamsURL> ReportParams { get; set; } = new List<RepParamsURL>();
    }
    public class RepParamsURL
    {
        public String param { get; set; }
        public object paramContent { get; set; }
    }
}
