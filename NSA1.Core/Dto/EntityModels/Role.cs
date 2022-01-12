
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto.EntityModels
{
    public class Role : IdentityRole<string>
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string CreatedById { get; set; }
        public bool IsActive { get; set; }
    }
}
