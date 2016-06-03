using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRAM1.Models
{
    public class Reaction
    {
        public enum ReactionType { Like,Dislike}

        public int Id { get; set; }
        public ReactionType LikeOrDislike { get; set; }
        //public int Like { get; set; }
        //public int Dislike { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual Picture Picture { get; set; }
    }
}