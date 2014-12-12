using BookReview.Models.Cookies;
using BookReview.Models.Data;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Models {
  public class AuthenticationModel {
    public string Username { get; set; }
    public string Password { get; set; }

    public async Task<bool> IsValidAsync() {
      var valid = false;
      this.Password = this.Password.Length != 40 ? HashedPassword(this.Password) : this.Password;

      using (var b = new BookEntities()) {
        var user = await b.Users.FirstOrDefaultAsync(x => x.Username == this.Username && x.Password == this.Password);
        if (user != null) {
          var cookie = new AuthCookie { 
            ID = user.ID, 
            DisplayName = string.Format("{0} {1}", user.FirstName, user.LastName),
            Groups = user.Groups.Select(x => x.Name).ToList()
          };

          cookie.Save();
          valid = true;
        }
      }

      return valid;
    }

    private string HashedPassword(string plain) {
      var hash = "";
      if (!string.IsNullOrEmpty(plain.Trim())) {
        var bytes = Encoding.UTF8.GetBytes(plain);
        try {
          hash = BitConverter.ToString(SHA1.Create().ComputeHash(bytes)).Replace("-", "").ToLower();
        }
        catch {
          hash = "";
        }
      }

      return hash;
    }
  }
}