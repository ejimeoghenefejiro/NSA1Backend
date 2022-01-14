using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto.EntityModels
{
    public class CommentUser
    {
        public int CommentUserId { get; set; }

        public int CommentId { get; set; }

        public string MemberId { get; set; }

    }
}
