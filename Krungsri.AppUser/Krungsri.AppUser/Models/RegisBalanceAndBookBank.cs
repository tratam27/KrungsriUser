using System;
using System.Collections.Generic;
using System.Text;

namespace Krungsri.AppUser.Models
{
    public class RegisBalanceAndBookBank
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public decimal Balance { get; set; }
        public string BookBank { get; set; }        
        public string PhoneNumber { get; set; }
    }
}
