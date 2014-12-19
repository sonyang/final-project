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

namespace BookReview.Controllers {
  public class UsersController : Controller {
    private BookEntities db = new BookEntities();

    // GET: Users
    public ActionResult Index() {
      return View(db.Users.ToList());
    }

    // GET: Users/Details/5
    public ActionResult Details(Guid? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      User user = db.Users.Find(id);
      if (user == null) {
        return HttpNotFound();
      }
      return View(user);
    }

    // GET: Users/Create
    public ActionResult Create() {
      return View();
    }

    // POST: Users/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(User user) {
      
      if (ModelState.IsValid) {

        if (user.Password == null || user.Username == null || user.FirstName == null || user.LastName == null) {
          Response.Write("<script>alert('Exception: Fields cannot be blank.')</script>");
        }
        else {
          if (Regex.IsMatch(user.Username, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")) {
            //Response.Write("<script>alert('Exception: valid.')</script>");

            if (Regex.IsMatch(user.Password, @"\p{Lu}") && Regex.IsMatch(user.Password, @"\d")) {
              user.ID = Guid.NewGuid();
              user.Password = HashPassword(user.Password);
              db.Users.Add(user);
              db.SaveChanges();
              return RedirectToAction("Index");
            }
            else {
              Response.Write("<script>alert('Exception: The password must contain 1 capital letter and at least one number.')</script>");
            }
          }
          else {
            Response.Write("<script>alert('Exception: The username must be a valid e-mail address.')</script>");

          }
        }


      }
  
      return View(user);
    }
    private string HashPassword(string plain) {
      var bytes = System.Text.Encoding.UTF8.GetBytes(plain);
      var hash = SHA1.Create().ComputeHash(bytes);  // 40 characters in length
      var read = BitConverter.ToString(hash).Replace("-", "");
      return read;
    }

    // GET: Users/Edit/5
    public ActionResult Edit(Guid? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      User user = db.Users.Find(id);
      if (user == null) {
        return HttpNotFound();
      }
      return View(user);
    }

    // POST: Users/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(User user) {
      if (ModelState.IsValid) {
        db.Entry(user).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(user);
    }

    // GET: Users/Delete/5
    public ActionResult Delete(Guid? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      User user = db.Users.Find(id);
      if (user == null) {
        return HttpNotFound();
      }
      return View(user);
    }

    // POST: Users/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(Guid id) {
      User user = db.Users.Find(id);
      db.Users.Remove(user);
      db.SaveChanges();
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        db.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}
