using BookReview.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;


namespace BookReview.Models {
  public class Duplicates {
    public string Username { get; set; }

    public static void Main(string[] args) {
      var valid = false;

      using (var b = new BookEntities()) {
        
        //var duplicate = from x in b.Users.FirstOrDefault(x => x.Username == this.Username)

      }

    }

  }
}