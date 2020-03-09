
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDashboard
{
    public class CustomResponse
    {
        public int ResponseID { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDetails { get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
