using Microsoft.Owin;
using System.Net.Http;
using System.Web;

namespace MarkdownApi.Web.Models.Extensions
{
    public static class UtilityExtensions
    {
        public static IOwinContext GetOwinContext(this HttpRequestMessage request)
        {
            var context = request.Properties["MS_HttpContext"] as HttpContextWrapper;

            if (context != null)
            {
                return HttpContextBaseExtensions.GetOwinContext(context.Request);
            }

            return null;
        }
    }
}