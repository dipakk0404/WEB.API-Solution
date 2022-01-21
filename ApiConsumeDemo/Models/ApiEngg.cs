using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiConsumeDemo.Models
{
    public class ApiEngg
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public string City { get; set; }
    }
}