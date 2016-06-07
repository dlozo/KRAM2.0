using FlickrNet;
using KRAM1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRAM1.Controllers
{
    public class FlickrApiController : Controller
    {
        // GET: FlickrApi
        public ActionResult Index()
        {
            //Flickr flickr = new Flickr();
            //flickr.ApiKey = "897fe3ffdd53547c7ab2812719da5bda";
            //flickr.ApiSecret = "07046d5330b89db8";

            //var listOfFlickrResults = new List<Photo>();
            //var photoSearch = new PhotoSearchOptions { Tags = searchInput };
            //var flickrImages = flickr.PhotosSearch(photoSearch);
            //var newList = new List<FlickrModel>();

            //foreach (var image in flickrImages)
            //{
            //    listOfFlickrResults.Add(image);
            //    newList.Add(new FlickrModel { Tag = image.Tags, PicUrl = image.LargeUrl, Title = image.Title });
            //}
            //return Json(newList, JsonRequestBehavior.AllowGet);

            return View();
        }

        public ActionResult FlickrSearch(string flickrSearchInput)
        {
            Flickr flickr = new Flickr();
            flickr.ApiKey = "897fe3ffdd53547c7ab2812719da5bda";
            flickr.ApiSecret = "07046d5330b89db8";

            var listOfFlickrResults = new List<Photo>();
            var photoSearch = new PhotoSearchOptions { Tags = flickrSearchInput };
            var flickrImages = flickr.PhotosSearch(photoSearch);
            var newList = new List<FlickrModel>();

            foreach (var image in flickrImages)
            {
                listOfFlickrResults.Add(image);
                newList.Add(new FlickrModel
                {
                    Tag = image.Tags,
                    PicUrl = image.LargeUrl,
                    //Title = image.Title
                });
            }
            return View(newList);
            //   return Json(newList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FlickrSearchResults(List<FlickrModel> searchResults)
        {
            return View(searchResults);
        }
    }
}