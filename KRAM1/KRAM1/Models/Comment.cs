using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KRAM1.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PictureId { get; set; }
        [MaxLength(100)]
        public string Text { get; set; }

        public virtual string UserId { get; set; }
        public string UserName { get; set; }

        public virtual Picture Picture { get; set; }
    }
}