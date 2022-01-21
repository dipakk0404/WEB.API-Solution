using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JwtAuthenticationMvc.Models
{
    public class Employee
    {


        public int Id { get; set; }
        public string EmpName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public Nullable<int> Age { get; set; }
    }

    public class UserCred
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> RoleId { get; set; }

    }
}