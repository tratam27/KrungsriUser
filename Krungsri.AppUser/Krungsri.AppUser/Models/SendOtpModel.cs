using System;
using System.Collections.Generic;
using System.Text;

namespace Krungsri.AppUser.Models
{
    public class SendOtpModel
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string Ref { get; set; }
    }
}
