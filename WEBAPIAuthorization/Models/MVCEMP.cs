using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBAPIAuthorization.Models
{
    public class MVCEMP
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

        public virtual Role Role { get; set; }

    }

}