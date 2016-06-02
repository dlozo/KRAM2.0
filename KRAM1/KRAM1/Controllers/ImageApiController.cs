﻿using KRAM1.Models;
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
            var allPics = context.Pictures.ToList();
            var newList = new List<PublicApiModel>();
            // PublicApiModel model = new PublicApiModel();

            foreach (var image in context.Pictures.ToList())
            {
                newList.Add(new PublicApiModel { Hashtag = image.Hashtag.Name, ImageUrl = image.PicUrl });
            }
            // var sorted = allPics.Where(x => x.Hashtag.Name == searchString).ToList();
            return newList;
        }


        // GET api/<controller>/5
        //public JsonResult Get(string searchString)
        //{
        //    var allImages = context.Pictures.ToList();

        //    var imagesBasedOnHashtag = allImages.Where(i => i.Hashtag.Name == searchString).ToList();
        //    return Json(imagesBasedOnHashtag.First(), JsonRequestBehavior.AllowGet);
        //}

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