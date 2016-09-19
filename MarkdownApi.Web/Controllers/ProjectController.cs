using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarkdownApi.Web.Controllers
{
    public class ProjectController : Controller
    {
        public Task<ActionResult> Index()
        {
            return Task.Run(() =>
            {
                return (ActionResult)View();
            });
        }

        [Route("Project/{id:int}")]
        public Task<ActionResult> Project(int id)
        {
            return Task.Run(() =>
            {
                return (ActionResult)View();
            });
        }
    }
}