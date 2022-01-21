using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.API_Solution.Models
{
    public class MvcStudent
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public string City { get; set; }
    }
}