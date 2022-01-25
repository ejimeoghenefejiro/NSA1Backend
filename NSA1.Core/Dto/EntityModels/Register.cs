using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto.EntityModels
{
    public class Register : IdentityUser
    {
        public Register()
        {
            DateCreated = DateTime.Now;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateOfBith { get; set; }       
        public string Region { get; set; }
        public string CityOfResidence { get; set; }
        public string StreetAddreee { get; set; }
        public string ZipPostal { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public byte[] verification { get; set; }
        public bool? IsverifiedModel { get; set; }
        public bool? IsAModel { get; set; }
        public bool? IsMember { get; set; }
        public bool IsClub { get; set; }
        public bool ISAdminUser { get; set; }
        public bool DetailCompleted { get; set; }

    }
}
