using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto.EntityModels
{
    public  class ClubAvaliabilityTime
    {
        public ClubAvaliabilityTime()
        {
            DateCreated = DateTime.Now;
        }
        public Guid ClubAvaliabilityId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? Status { get; set; }
        public string DayName { get; set; }
        public Guid ClubId { get; set; }
        public virtual Club Club_ { get; set; }
    }
}
