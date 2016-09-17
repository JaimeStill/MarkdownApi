using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarkdownApi.Web.Controllers
{
    public class HomeController : Controller
    {
        public Task<ActionResult> Index()
        {
            return Task.Run(() =>
            {
                return (ActionResult)View();
            });
        }
    }
}