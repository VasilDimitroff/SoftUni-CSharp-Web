using SUS.HTTP;
using SUS.MvcFramework;

namespace Andreys.App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/Products/All");
            }

            return this.View();
        }
    }
}
