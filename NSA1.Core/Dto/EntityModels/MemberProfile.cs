using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto.EntityModels
{
   public class MemberProfile
    {
        public MemberProfile()
        {
            DateEdited = DateTime.Now;
        }
        [Key]
        public Guid MemberProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string Country { get; set; }
        public int FavoriteModel { get; set; }
        public string NickName { get; set; }
        public bool? Gender { get; set; }
        public int Age { get; set; }
        public byte[] ProfilePic { get; set; }
        public bool? IsMemberAccountDeleted { get; set; }
        public DateTime DateEdited { get; set; }
        public string RegisterId { get; set; }
        public Register Register { get; set; }
    }
}
