using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using Weed.Models;

namespace Weed.Controllers
{
    public class DispensaryController : Controller
    {
        [HttpGet("/dispensaries")]
        public ActionResult Index()
        {
            string cityName ="Seattle";
            Dictionary<string,object> model = new Dictionary<string,object>{};
            List<Dispensary> allDispensaries = Dispensary.GetAll();
            List<Dispensary> allAddresses = Dispensary.GetAddresses(cityName);
            model.Add("dispensaries", allDispensaries);
            model.Add("addresses", allAddresses);
            return View(model);
        }

        [HttpGet("/dispensaries/{license}")]
        public ActionResult Show(int license)
        {
            List<Comment> foundComments = Comment.GetCommentsByLicense(license);
            Dispensary foundDispensary = Dispensary.FindByLicense(license);
            Dictionary<string, object> model = new Dictionary<string, object> {};
            model.Add("dispensary", foundDispensary);
            model.Add("comments", foundComments);
            return View(model);
        }

        [HttpPost("/dispensaries/searchbyname")]
        public ActionResult SearchName(string DispensaryName)
        {  Dispensary foundDispensary = Dispensary.FindByName(DispensaryName);
           return View("Show", foundDispensary);     
        }

        [HttpPost("/dispensaries/searchbycity")]
        public ActionResult SearchCity(string city)
        {   
            Dictionary<string, object> model = new Dictionary<string, object>{};
            List<Dispensary> allDispensaries = Dispensary.FindByCity(city);
            model.Add("dispensaries", allDispensaries);
            model.Add("city", city);
            return View(model);
        }

        [HttpPost("/dispensaries/{license}/comments")]
        public ActionResult CreateComment(int license, string review, int rating)
        {
            Comment newComment = new Comment(review, rating, license);
            newComment.Save();
            List<Comment> foundComments = Comment.GetCommentsByLicense(license);
            Dispensary foundDispensary = Dispensary.FindByLicense(license);
            Dictionary<string, object> model = new Dictionary<string, object> {};
            model.Add("dispensary", foundDispensary);
            model.Add("comments", foundComments);
            return View("Show", model); 
        }
    }
}
