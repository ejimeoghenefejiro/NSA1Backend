using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto
{
     public class WelcomeRequest
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
    }
    public class ForgotPasswordEmail
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
