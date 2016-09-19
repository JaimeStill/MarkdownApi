using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarkdownApi.Web.Controllers
{
    public class AdminController : Controller
    {
        public Task<ActionResult> Index()
        {
            return Task.Run(() =>
            {
                return (ActionResult)View();
            });
        }

        [Route("Admin/Wiki/{id:int?}")]
        public Task<ActionResult> Wiki(int? id)
        {
            return Task.Run(() =>
            {
                return (ActionResult)View(id);
            });
        }

        [Route("Admin/Wiki/{wikiId:int}/Document/{documentId:int?}")]
        public Task<ActionResult> Document(int wikiId, int? documentId)
        {
            return Task.Run(() =>
            {
                ViewBag.WikiId = wikiId;
                return (ActionResult)View(documentId);
            });
        }
    }
}