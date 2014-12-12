using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReview.Models.Cookies {
  public sealed class AuthCookie {
    public Guid ID { get; set; }
    public string DisplayName { get; set; }
    public List<string> Groups { get; set; }

    public static AuthCookie Current {
      get {
        var ctx = HttpContext.Current;

        try {
          var cookie = ctx.Request.Cookies["FPU"];
          return JsonConvert.DeserializeObject<AuthCookie>(cookie.Value);
        }
        catch {
          return new AuthCookie { ID = Guid.Empty, DisplayName = "", Groups = new List<string>() };
        }
      }
    }

    public void Save() {
      var ctx = HttpContext.Current;

      var cookie = new HttpCookie("FPU", JsonConvert.SerializeObject(this)) {
        Expires = DateTime.Now.AddDays(14),
        HttpOnly = true,
        Path = "/"
      };

      ctx.Response.Cookies.Add(cookie);
    }

    public void Remove() {
      try {
        var cookie = HttpContext.Current.Request.Cookies["FPU"];
        HttpContext.Current.Response.Cookies.Remove("FPU");
        cookie.Expires = DateTime.Now.AddDays(-10);
        cookie.Value = null;
        HttpContext.Current.Response.SetCookie(cookie);
      }
      catch {

      }
    }
  }
}