using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto.EntityModels
{
    public class ProblemReport
    {
        public ProblemReport()
        {
            CreatedDate = DateTime.Now;
        }
        public Guid ProblemId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public int YourQuestion { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
