using System;
using System.Collections.Generic;
using System.Text;

namespace Krungsri.AppUser.Models
{
    public class TopUpModel
    {
        public string BookBank { get; set; }
        public string Name { get; set; }
        public string MoneyAmount { get; set; }
        public int AdminId { get; set; }
        public string ExpiryDate { get; set; }
        public string Ref { get; set; }
    }
}
