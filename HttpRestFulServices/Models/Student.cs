using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HttpRestFulServices.Models
{
    public class Emp
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<int> DpId { get; set; }
    }
}