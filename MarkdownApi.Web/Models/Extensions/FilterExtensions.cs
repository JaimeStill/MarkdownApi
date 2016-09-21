using MarkdownApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MarkdownApi.Web.Models.Extensions
{
    public static class FilterExtensions
    {
        public static Task<IEnumerable<CategoryModel>> FindCategories(this AppDbContext context, string categories)
        {
            return Task.Run(() =>
            {
                var model = context.Categories.Where(x => x.Name.ToLower().Contains(categories.ToLower())).Select(x => new CategoryModel
                {
                    id = x.Id,
                    name = x.Name,
                    wikis = x.Wikis.Select(y => new WikiModel
                    {
                        id = y.Id,
                        name = y.Name,
                        description = y.Description,
                        markdown = y.Markdown
                    }).OrderBy(y => y.name).AsEnumerable()
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }

        public static Task<IEnumerable<CategoryModel>> FindWikis(this AppDbContext context, string wikis)
        {
            return Task.Run(() =>
            {
                var categories = context.Wikis.Where(x => x.Name.ToLower().Contains(wikis.ToLower())).Select(x => x.Category);

                var model = categories.Select(x => new CategoryModel
                {
                    id = x.Id,
                    name = x.Name,
                    wikis = x.Wikis.Select(y => new WikiModel
                    {
                        id = y.Id,
                        name = y.Name,
                        description = y.Description,
                        markdown = y.Markdown
                    }).OrderBy(y => y.name).AsEnumerable()
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }

        public static Task<IEnumerable<CategoryModel>> FindDocuments(this AppDbContext context, string documents)
        {
            return Task.Run(() =>
            {
                var categories = context.Documents.Where(x => x.Title.ToLower().Contains(documents.ToLower())).Select(x => x.Wiki.Category);

                var model = categories.Select(x => new CategoryModel
                {
                    id = x.Id,
                    name = x.Name,
                    wikis = x.Wikis.Select(y => new WikiModel
                    {
                        id = y.Id,
                        name = y.Name,
                        description = y.Description,
                        markdown = y.Markdown
                    }).OrderBy(y => y.name).AsEnumerable()
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }
    }
}