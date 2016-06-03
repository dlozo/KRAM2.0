using KRAM1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace KRAM1.Controllers
{
    public class ImageApiController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET api/<controller>
        public List<PublicApiModel> Get()
        {
            var images = new List<PublicApiModel>();
            // PublicApiModel model = new PublicApiModel();

            foreach (var image in context.Pictures.ToList())
            {
                images.Add(new PublicApiModel { Hashtag = image.Hashtag.Name, ImageUrl = image.PicUrl });
            }
            return images;
        }


        //GET api/<controller>/5
        public List<PublicApiModel> Get(string searchString)
        {
            var allImages = context.Pictures.ToList();

            var imagesBasedOnHashtag = new List<PublicApiModel>();
            foreach (var image in allImages)
            {
                if (image.Hashtag.Name == searchString)
                {
                    imagesBasedOnHashtag.Add(new PublicApiModel
                    {
                        Hashtag = image.Hashtag.Name,
                        ImageUrl = image.PicUrl
                    });
                }
            }

            return imagesBasedOnHashtag;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}