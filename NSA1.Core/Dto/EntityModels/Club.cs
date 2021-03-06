using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto.EntityModels
{
    public class Club
    {
        public Club()
        {
            ClubAvaliabilityTimes = new HashSet<ClubAvaliabilityTime>();
        }
        [Key]
        public Guid ClubProfileId { get; set; }
        [Required(ErrorMessage ="Club Name Required")]
        public string ClubName { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        public string street { get; set; }
        public string site { get; set; }
        public string Phone { get; set; }
        public string HomePhone { get; set; }
        public string Appartment { get; set; }
        public string Country { get; set; }
        public string ZipPostal { get; set; }
        public string Site { get; set; }
        public byte[] Cover { get; set; }
        public byte[] LogoType { get; set; }
        public bool ClubStatus { get; set; } = true; // 0/1 the club is active or not active
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string Address { get; set; }
        public bool? IsClubAccountDeleted { get; set; }
        public string RegisterId { get; set; } 
        public Register Register { get; set; }  
        public virtual ICollection<ClubAvaliabilityTime> ClubAvaliabilityTimes { get; set;}
    }
}
