using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRAM1.Models
{
    public class FlickrModel
    {
        public System.Collections.ObjectModel.Collection<string> Tag { get; set; }
        public string PicUrl { get; set; }
        public string Title { get; set; }

    }
}