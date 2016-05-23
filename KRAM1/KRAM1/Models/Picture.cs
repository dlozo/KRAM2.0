using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRAM1.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string PicUrl { get; set; }
        public DateTime TimeStamp { get; set; }
        public virtual IList<Hashtag> Hashtag { get; set; }
        public virtual IList<Reaction> Reaction { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual IList<Comment> Comment { get; set; }
    }
}