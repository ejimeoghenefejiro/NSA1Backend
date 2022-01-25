using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto
{
    public class UpdateClubView
    {
        public string ClubName { get; set; }
        public string Description { get; set; }
        public string street { get; set; }
        public string site { get; set; }
        public string Phone { get; set; }
        public string HomePhone { get; set; }
        public string Appartment { get; set; }
        public string Country { get; set; }
        public string ZipPostal { get; set; }
        public string Site { get; set; }       
        public bool ClubStatus { get; set; } = true; // 0/1 the club is active or not active
        public string Address { get; set; }
    }
}
