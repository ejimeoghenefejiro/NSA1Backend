using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto.EntityModels
{
    public class Update
    {
        [ScaffoldColumn(false)]
        public int UpdateId { get; set; }

        public string Updatemsg { get; set; }

        public double? status { get; set; }

        public int GoalId { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Update()
        {
            UpdateDate = DateTime.Now;

        }

    }
}
