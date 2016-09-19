using MarkdownApi.Data;
using MarkdownApi.Web.Models;
using MarkdownApi.Web.Models.Extensions;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MarkdownApi.Web.Controllers
{
    public class ProjectApiController : ApiController
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

        [Route("api/projects/getProjects")]
        [HttpGet]
        public async Task<IEnumerable<ProjectModel>> GetProjects()
        {
            return await Context.GetProjects();
        }

        [Route("api/projects/getProject/{id}")]
        [HttpPost]
        public async Task<ProjectModel> GetProject(int id)
        {
            return await Context.GetProject(id);
        }

        [Route("api/projects/addProject")]
        [HttpPost]
        public async Task<ProjectModel> AddProject(ProjectModel model)
        {
            return await Context.AddProject(model);
        }

        [Route("api/projects/updateProject")]
        [HttpPost]
        public async Task UpdateProject(ProjectModel model)
        {
            await Context.UpdateProject(model);
        }

        [Route("api/projects/deleteProject")]
        [HttpPost]
        public async Task DeleteProject(ProjectModel model)
        {
            await Context.DeleteProject(model);
        }
    }
}
