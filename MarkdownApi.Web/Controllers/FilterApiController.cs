using MarkdownApi.Data;
using MarkdownApi.Web.Models;
using MarkdownApi.Web.Models.Extensions;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace MarkdownApi.Web.Controllers
{
    public class FilterApiController : ApiController
    {
        private AppDbContext context;

        public AppDbContext Context
        {
            get
            {
                return context ?? Request.GetOwinContext().Get<AppDbContext>();
            }
            private set
            {
                context = value;
            }
        }

        [Route("api/filters/findCategories")]
        [HttpPost]
        public async Task<IEnumerable<CategoryModel>> FindCategories(string categories)
        {
            return await Context.FindCategories(categories);
        }

        [Route("api/filters/findWikis")]
        [HttpPost]
        public async Task<IEnumerable<CategoryModel>> FindWikis(string wikis)
        {
            return await Context.FindWikis(wikis);
        }

        [Route("api/filters/findDocuments")]
        [HttpPost]
        public async Task<IEnumerable<CategoryModel>> FindDocuments(string documents)
        {
            return await Context.FindDocuments(documents);
        }
    }
}
