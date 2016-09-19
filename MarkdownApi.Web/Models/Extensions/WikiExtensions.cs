using MarkdownApi.Data;
using MarkdownApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MarkdownApi.Web.Models.Extensions
{
    public static class WikiExtensions
    {
        public static Task<IEnumerable<WikiModel>> GetWikis(this AppDbContext context)
        {
            return Task.Run(() =>
            {
                var model = context.Wikis.Select(x => new WikiModel
                {
                    id = x.Id,
                    name = x.Name,
                    description = x.Description,
                    markdown = x.Markdown
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }

        public static Task<IEnumerable<WikiModel>> FindWikis(this AppDbContext context, string wikis)
        {
            return Task.Run(() =>
            {
                var model = context.Wikis.Where(x => x.Name.ToLower().Contains(wikis.ToLower())).Select(x => new WikiModel
                {
                    id = x.Id,
                    name = x.Name,
                    description = x.Description,
                    markdown = x.Markdown
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }

        public async static Task<WikiModel> GetWiki(this AppDbContext context, int id)
        {
            var wiki = await context.Wikis.FindAsync(id);

            var model = new WikiModel
            {
                id = wiki.Id,
                name = wiki.Name,
                description = wiki.Description,
                markdown = wiki.Markdown,
                documents = wiki.Documents.Select(x => new DocumentModel
                {
                    id = x.Id,
                    markdown = x.Markdown,
                    title = x.Title
                }).AsEnumerable()
            };

            return model;
        }

        public static async Task<WikiModel> AddWiki(this AppDbContext context, WikiModel model)
        {
            if (await model.Validate(context))
            {
                var wiki = new Wiki
                {
                    Name = model.name,
                    Description = model.description,
                    Markdown = model.markdown
                };

                context.Wikis.Add(wiki);
                await context.SaveChangesAsync();

                model.id = wiki.Id;

                return model;
            }

            return new WikiModel();
        }

        public static async Task UpdateWiki(this AppDbContext context, WikiModel model)
        {
            if (await model.Validate(context, true))
            {
                var wiki = await context.Wikis.FindAsync(model.id);

                wiki.Name = model.name;
                wiki.Description = model.description;
                wiki.Markdown = model.markdown;

                await context.SaveChangesAsync();
            }
        }

        public static async Task<bool> Validate(this WikiModel model, AppDbContext context, bool updating = false)
        {
            if (updating)
            {
                if (!(model.id > 0))
                {
                    throw new Exception("The provided wiki does not specify an ID");
                }
            }

            if (await model.Exists(context, updating))
            {
                throw new Exception("The provided wiki name has already been used");
            }

            if (string.IsNullOrEmpty(model.name))
            {
                throw new Exception("The provided wiki does not specify a name");
            }

            if (string.IsNullOrEmpty(model.description))
            {
                throw new Exception("The provided wiki does not specify a description");
            }

            return true;
        }

        public static Task<bool> Exists(this WikiModel model, AppDbContext context, bool updating)
        {
            return Task.Run(() =>
            {
                Wiki wiki;

                if (updating)
                {
                    wiki = context.Wikis.FirstOrDefault(x => !(x.Id == model.id) && x.Name.ToLower().Equals(model.name.ToLower()));
                }
                else
                {
                    wiki = context.Wikis.FirstOrDefault(x => x.Name.ToLower().Equals(model.name.ToLower()));
                }

                return wiki == null ? false : true;
            });
        }

        public static async Task DeleteWiki(this AppDbContext context, WikiModel model)
        {
            var documents = context.Documents.Where(x => x.WikiId == model.id);

            if (documents.Count() > 0)
            {
                context.Documents.RemoveRange(documents);
                await context.SaveChangesAsync();
            }

            var wiki = await context.Wikis.FindAsync(model.id);
            context.Wikis.Remove(wiki);
            await context.SaveChangesAsync();
        }
    }
}