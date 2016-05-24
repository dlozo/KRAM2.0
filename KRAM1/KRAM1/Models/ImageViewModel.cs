using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KRAM1.Models
    {
    public class ImageViewModel
        {
        public string userID { get; set; }
         [Required]
    public string imageName { get; set; }

    public bool isprofilepic { get; set; }

    [DataType(DataType.Html)]
    public string Caption { get; set; }

    [DataType(DataType.Upload)]
   public HttpPostedFileBase ImageUpload { get; set; }
            }

        }
    