using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookReview.Models.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using BookReview.Models.Cookies;

namespace BookReview.Controllers {
  public class UsersController : Controller {
    private BookEntities db = new BookEntities();
    private AuthCookie userOk = AuthCookie.Current;

    // GET: Users
    public ActionResult Index() {
      if (userOk.Groups.Contains("Administrator")) {
        ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name");
        return View(db.Users.ToList());
      }
      return HttpNotFound();
    }

    // GET: Users/Details/5
    public ActionResult Details(Guid? id) {
      if (userOk.Groups.Contains("Administrator")) {
        if (id == null) {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        User user = db.Users.Find(id);
        if (user == null) {
          return HttpNotFound();
        }

        return View(user);
      }
      return HttpNotFound();

    }
    // GET: Users/Create
    public ActionResult Create() {
      return View();
    }

    [HttpPost]
    public ActionResult SaveUser(User user, FormCollection fc) {
      if (userOk.Groups.Contains("Administrator")) {
        if (ModelState.IsValid) {
          if (Regex.IsMatch(user.Username, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")) {

            if (Regex.IsMatch(user.Password, @"\p{Lu}") && Regex.IsMatch(user.Password, @"\d")) {
              user.ID = Guid.NewGuid();
              user.Password = HashPassword(user.Password);

              var x = Guid.Parse(fc["GroupID"]);
              var g = db.Groups.Find(x);
              if (g != null) { user.Groups.Add(g); }

              db.Users.Add(user);
              db.SaveChanges();
              return RedirectToAction("Index");
            }

          }

        }
        return RedirectToAction("Index", "Home");
      }
      return HttpNotFound();
    }


    private string HashPassword(string plain) {
      var bytes = System.Text.Encoding.UTF8.GetBytes(plain);
      var hash = SHA1.Create().ComputeHash(bytes);  // 40 characters in length
      var read = BitConverter.ToString(hash).Replace("-", "");
      return read;
    }

    // GET: Users/Edit/5
    public ActionResult Edit(Guid? id) {
      if (userOk.Groups.Contains("Administrator")) {
        ViewBag.GroupID = new SelectList(db.Groups, "ID", "Name");

        if (id == null) {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        User user = db.Users.Find(id);

        if (user == null) {
          return HttpNotFound();
        }
        return View(user);
      }
      return HttpNotFound();
    }

    // POST: Users/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(User user, FormCollection fc) {
      if (userOk.Groups.Contains("Administrator")) {
        if (ModelState.IsValid) {
          db.Entry(user).State = EntityState.Modified;
          user.Password = user.Password.Length != 40 ? HashPassword(user.Password) : user.Password;

          var groups = db.Groups;
          foreach (var gr in groups) {
            gr.Users.Remove(user);
          }

          var x = Guid.Parse(fc["GroupID"]);
          var g = groups.FirstOrDefault(c => c.ID == x);
          if (g != null) { user.Groups.Add(g); }

          db.SaveChanges();
          return RedirectToAction("Index");
        }
        return View();
      }
      return HttpNotFound();
    }

    // GET: Users/Delete/5
    public ActionResult Delete(Guid? id) {
      if (userOk.Groups.Contains("Administrator")) {
        if (id == null) {
          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        User user = db.Users.Find(id);
        if (user == null) {
          return HttpNotFound();
        }
        return View(user);
      }
      return HttpNotFound();
    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(Guid id) {
      if (userOk.Groups.Contains("Administrator")) {
        User user = db.Users.Find(id);
        user.Groups.Clear();
        db.Users.Remove(user);
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return HttpNotFound();
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
