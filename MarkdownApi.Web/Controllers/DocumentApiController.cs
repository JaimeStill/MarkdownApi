using MarkdownApi.Data;
using MarkdownApi.Web.Models;
using MarkdownApi.Web.Models.Extensions;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace MarkdownApi.Web.Controllers
{
    public class DocumentApiController : ApiController
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

        [Route("api/documents/getDocuments/{id}")]
        [HttpPost]
        public async Task<IEnumerable<DocumentModel>> GetDocuments(int id)
        {
            return await Context.GetDocuments(id);
        }

        [Route("api/documents/getDocument/{id}")]
        [HttpPost]
        public async Task<DocumentModel> GetDocument(int id)
        {
            return await Context.GetDocument(id);
        }

        [Route("api/documents/addDocument")]
        [HttpPost]
        public async Task<DocumentModel> AddDocument(DocumentModel model)
        {
            return await Context.AddDocument(model);
        }

        [Route("api/documents/updateDocument")]
        [HttpPost]
        public async Task UpdateDocument(DocumentModel model)
        {
            await Context.UpdateDocument(model);
        }

        [Route("api/documents/deleteDocument")]
        [HttpPost]
        public async Task DeleteDocument(DocumentModel model)
        {
            await Context.DeleteDocument(model);
        }
    }
}
