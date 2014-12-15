﻿using BookReview.Models.Cookies;
using System.Web.Mvc;

namespace BookReview.Controllers {
  public class HomeController : Controller {
    // Gets all information about the currently logged in person.
    private AuthCookie user = AuthCookie.Current;

    public ActionResult Index() {
      bool isAdmin = user.Groups.Contains("Administrator");  //WILL WORK THROUGHOUT THE PROJECT
      bool isUSer = user.Groups.Contains("User"); //THAT IS WHAT RETURNS
      return View();
    }

    public ActionResult About() {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact() {
      ViewBag.Message = "Your contact page.";

      return View();
    }

    public ActionResult Logout() {
      AuthCookie.Current.Remove();
      return RedirectToAction("Index", "Home");
    }
  }
}