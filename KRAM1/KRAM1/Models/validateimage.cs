using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace KRAM1.Models
    {
    public static class HttpPostedFileBaseExtensions
        {
       //Minstorlek 512bytes
        public const int ImageMinimumBytes = 512;

       //MaxStorlek 10mb, men vi äöndrar
        public const int ImageMaximumBytes = 10*1024*1024;

    
        private static readonly List<string> ImageMimeTypes = new List<string>
                                   {
                                       "image/jpg",
                                       "image/jpeg",
                                       "image/pjpeg",
                                       "image/gif",
                                       "image/x-png",
                                       "image/png"
                                   };

       //Bild extensions
        private static readonly List<string> ImageFileExtensions = new List<string>
                                   {
                                       ".jpg",
                                       ".png",
                                       ".gif",
                                       ".jpeg"
                                   };
         
       
        public static void ValidateImageFile(this HttpPostedFileBase file)
            {
            if(!file.ValidMinimumImageSize())
                {
                  //Får fixa error meddelande här
                throw new ArgumentException($"The Image file must be bigger than  {ImageMinimumBytes} bytes.");
                }

            if(!file.ValidMaximumImageSize())
                {
                  //Får fixa error meddelande här
                throw new ArgumentException($"The Image file must be smaller than {ImageMaximumBytes/(1024*1024)} MB.");
                }

            if(!file.ImageFile())
                {
                //Får fixa error meddelande här
               throw new ArgumentException($"You uploaded a file that was not an image.");
                }
            }


        public static bool ValidMinimumImageSize(this HttpPostedFileBase file)
            {
            return file.ContentLength>ImageMinimumBytes;
            }

      
        public static bool ValidMaximumImageSize(this HttpPostedFileBase file)
            {
            return file.ContentLength<=ImageMaximumBytes;
            }


        public static bool ImageFile(this HttpPostedFileBase file)
            {
            var contentType = file.ContentType.ToLower();
                      
              try
                 {
                //Den försöker spara det som en bild för att se om det verkligen är en bild och inte en .exe fil som är omdöpt
                Image.FromStream(file.InputStream, true, true);
                  }
                 catch(Exception)
                            {

                return false;
                            }
            // Kollar MimeType, bildformaten
            if(ImageMimeTypes.All(x => x!=contentType))
                {
                return false;
                }

            // Kollar extension
            if(ImageFileExtensions.All(x => !file.FileName.EndsWith(x)))
                {
                return false;
                }

            return true;
            }
        }
    }