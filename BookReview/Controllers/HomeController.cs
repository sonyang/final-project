using BookReview.Models.Cookies;
using System.Web.Mvc;

namespace BookReview.Controllers {
  public class HomeController : Controller {
    // Gets all information about the currently logged in person.
    private AuthCookie user = AuthCookie.Current;

    public ActionResult Index() {
      bool isAdmin = user.Groups.Contains("Administrator");
      bool isUser = user.Groups.Contains("User");
      return View();
    }

    public ActionResult About() {
      ViewBag.Message = "What is Novel Busters?";

      return View();
    }

    public ActionResult Contact() {
      ViewBag.Message = "Bussiness Office";

      return View();
    }

    public ActionResult Logout() {
      AuthCookie.Current.Remove();
      return RedirectToAction("Index", "Home");
    }
  }
}