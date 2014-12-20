using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookReview.Models.Data;
using BookReview.Models.Cookies;

namespace BookReview.Controllers {
  public class BookController : Controller {
    private BookEntities db = new BookEntities();

    // GET: Book
    public ActionResult Index() {
      return View(db.Books.ToList());
    }
    [HttpGet]
    // GET: Book/Details/5
    public ActionResult Details(Guid? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Book book = db.Books.Find(id);
      if (book == null) {
        return HttpNotFound();
      }
      return View(book);
    }

    [HttpPost]
    public ActionResult SaveReview(Review review) {
      if (AuthCookie.Current.ID == Guid.Empty) { return HttpNotFound(); }

      if (ModelState.IsValid) {
        review.ID = Guid.NewGuid();
        db.Reviews.Add(review);
        db.SaveChanges();
        return RedirectToAction("Details", new { id = review.BookID });
      }
      //else {
      //  foreach (var x in ModelState) {
      //    Response.Write(string.Format("{0} {1}<br />", x.Key, x.Value));
      //  }
      //  return null;
      //}

      return RedirectToAction("Index", "Home");
    }

    // GET: Book/Create
    public ActionResult Create() {
      return View();
    }

    // POST: Book/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Book book) {
      if (ModelState.IsValid) {

        if (book.ISBN10 == null && book.ISBN13 == null) {
          Response.Write("<script>alert('Exception: Please Enter at least one ISBN number.')</script>");

        }
        else {
          if (book.ISBN10 != null) {
            book.ISBN13 = BookReview.Models.IsbnConverter.ConvertIsbn10(book.ISBN10);
          }
          if (book.ISBN13 != null) {
            book.ISBN10 = BookReview.Models.IsbnConverter.ConvertIsbn13(book.ISBN13);
          }

          book.ID = Guid.NewGuid();
          db.Books.Add(book);
          db.SaveChanges();
          return RedirectToAction("Index");
        }

      }


      return View(book);
    }

    // GET: Book/Edit/5
    public ActionResult Edit(Guid? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Book book = db.Books.Find(id);
      if (book == null) {
        return HttpNotFound();
      }
      return View(book);
    }

    // POST: Book/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Book book) {
      if (ModelState.IsValid) {
        db.Entry(book).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      }
      return View(book);
    }

    // GET: Book/Delete/5
    public ActionResult Delete(Guid? id) {
      if (id == null) {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Book book = db.Books.Find(id);
      if (book == null) {
        return HttpNotFound();
      }
      return View(book);
    }

    // POST: Book/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(Guid id) {
      Book book = db.Books.Find(id);
      db.Books.Remove(book);
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
