using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRAM1.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
    }
}