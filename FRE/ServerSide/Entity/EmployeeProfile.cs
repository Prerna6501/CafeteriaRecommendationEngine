using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Entity
{
    public class EmployeeProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DietPreference { get; set; }
        public string SpiceLevel { get; set; }
        public string CuisinePreference { get; set; }
        public bool HasSweetTooth { get; set; }
        public User User { get; set; }
    }
}
