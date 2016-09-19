using MarkdownApi.Data;
using MarkdownApi.Web.Models;
using MarkdownApi.Web.Models.Extensions;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace MarkdownApi.Web.Controllers
{
    public class WikiApiController : ApiController
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

        [Route("api/wikis/getWikis")]
        [HttpGet]
        public async Task<IEnumerable<WikiModel>> GetWikis()
        {
            return await Context.GetWikis();
        }

        [Route("api/wikis/findWikis")]
        [HttpPost]
        public async Task<IEnumerable<WikiModel>> FindWikis(string wikis)
        {
            return await Context.FindWikis(wikis);
        }

        [Route("api/wikis/getWiki/{id}")]
        [HttpPost]
        public async Task<WikiModel> GetWiki(int id)
        {
            return await Context.GetWiki(id);
        }

        [Route("api/wikis/addWiki")]
        [HttpPost]
        public async Task<WikiModel> AddWiki(WikiModel model)
        {
            return await Context.AddWiki(model);
        }

        [Route("api/wikis/updateWiki")]
        [HttpPost]
        public async Task UpdateWiki(WikiModel model)
        {
            await Context.UpdateWiki(model);
        }

        [Route("api/wikis/deleteWiki")]
        [HttpPost]
        public async Task DeleteWiki(WikiModel model)
        {
            await Context.DeleteWiki(model);
        }
    }
}
