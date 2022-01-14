using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NSA1.Web.Authentication
{
    public class RegisterMemberModel
    {
        [EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Password must be a minimum of 8 characters with at least capital letter and at least one symbol and at least one number")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,25}$", ErrorMessage = "Password must be a minimum of 8 characters with at least capital letter and at least one symbol and at least one number and maximum of 25 characters")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
