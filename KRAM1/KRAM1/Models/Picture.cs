﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KRAM1.Models
{
    public class Picture
    {
        public int Id { get; set; }
        [Required]
        public string PicUrl { get; set; }
        public DateTime TimeStamp { get; set; }
        [Required]
        public virtual Hashtag Hashtag { get; set; }
        public virtual IList<Reaction> Reaction { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual IList<Comment> Comments { get; set; }
    }
}