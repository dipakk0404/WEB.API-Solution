using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWEBAPICONSUM.Models
{
    public class StudentData
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> TrainerId { get; set; }

    }
}