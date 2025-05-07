using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forum.Models
{
    public class UserActivityData
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public int TotalRecipes { get; set; }
        public int TotalComments { get; set; }
        public DateTime? LastLogin { get; set; } //Nullable DateTime
        public DateTime? RegistrationDate { get; set; }
    }
}
