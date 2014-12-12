using BookReview.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookReview.ApiControllers {
  public class LoginController : ApiController {

    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> Post(AuthenticationModel auth) {      
      var valid = await auth.IsValidAsync();
      if (!valid) { return Unauthorized(); }
      
      return Ok();
    }

    protected override void Dispose(bool disposing) {
      if (disposing) {
        
      }
      base.Dispose(disposing);
    }

  }
}