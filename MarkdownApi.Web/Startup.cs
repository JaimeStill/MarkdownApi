using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using MarkdownApi.Data;

[assembly: OwinStartup(typeof(MarkdownApi.Web.Startup))]

namespace MarkdownApi.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(AppDbContext.Create);
        }
    }
}
