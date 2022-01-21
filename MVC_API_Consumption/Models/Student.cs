using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API_Consumption.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Age { get; set; }
        public string Gender { get; set; }
        public Nullable<int> TrainerId { get; set; }
    }
}