using MarkdownApi.Data;
using MarkdownApi.Web.Models;
using MarkdownApi.Web.Models.Extensions;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace MarkdownApi.Web.Controllers
{
    public class CategoryApiController : ApiController
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

        [Route("api/categories/getCategories")]
        [HttpGet]
        public async Task<IEnumerable<CategoryModel>> GetCategories()
        {
            return await Context.GetCategories();
        }

        [Route("api/categories/getCategory/{id}")]
        [HttpPost]
        public async Task<CategoryModel> GetCategory(int id)
        {
            return await Context.GetCategory(id);
        }

        [Route("api/categories/renameCategory")]
        [HttpPost]
        public async Task<bool> RenameCategory(CategoryModel model)
        {
            return await Context.RenameCategory(model);
        }
    }
}
