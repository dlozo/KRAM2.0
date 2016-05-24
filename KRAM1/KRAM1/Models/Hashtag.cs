using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KRAM1.Models
{
    public class Hashtag
    {
        public int Id { get; set; }

        public string Name { get; set; }

 
        public virtual IList<Picture> Picture { get; set; }
    }
}