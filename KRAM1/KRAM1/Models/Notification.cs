using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRAM1.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Message { get; set; }
        public bool HasRead { get; set; }
        public DateTime Time { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
    }
}