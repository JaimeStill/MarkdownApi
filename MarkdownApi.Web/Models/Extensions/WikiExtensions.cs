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
                    markdown = x.Markdown,
                    category = new CategoryModel
                    {
                        id = x.CategoryId,
                        name = x.Category.Name
                    },
                    sidebar = new SidebarModel
                    {
                        id = x.SidebarId,
                        markdown = x.Sidebar.Markdown
                    },
                    documents = x.Documents.Select(y => new DocumentModel
                    {
                        id = y.Id,
                        index = y.Index,
                        title = y.Title,
                        markdown = y.Markdown
                    }).OrderBy(y => y.index).AsEnumerable()
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }

        public static Task<WikiModel> GetWiki(this AppDbContext context, int id)
        {
            return Task.Run(() =>
            {
                var wiki = context.Wikis.Include("Category").Include("Sidebar").Include("Documents").FirstOrDefault(x => x.Id == id);

                var model = new WikiModel
                {
                    id = wiki.Id,
                    name = wiki.Name,
                    description = wiki.Description,
                    markdown = wiki.Markdown,
                    category = new CategoryModel
                    {
                        id = wiki.CategoryId,
                        name = wiki.Category.Name
                    },
                    sidebar = new SidebarModel
                    {
                        id = wiki.SidebarId,
                        markdown = wiki.Sidebar.Markdown
                    },
                    documents = wiki.Documents.Select(x => new DocumentModel
                    {
                        id = x.Id,
                        markdown = x.Markdown,
                        title = x.Title
                    }).OrderBy(x => x.index).AsEnumerable()
                };

                return model;
            });
        }

        public static async Task<WikiModel> AddWiki(this AppDbContext context, WikiModel model)
        {
            if (await model.Validate(context))
            {
                var category = await context.CreateCategoryIfNotExists(model.category.name);

                var wiki = new Wiki
                {
                    Name = model.name,
                    Description = model.description,
                    Markdown = model.markdown,
                    CategoryId = category.Id,
                    Sidebar = new Sidebar
                    {
                        Markdown = model.sidebar.markdown
                    }
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
                var wiki = context.Wikis.Include("Category").Include("Sidebar").FirstOrDefault(x => x.Id == model.id);

                if (!(model.category.name.ToLower().Equals(wiki.Category.Name.ToLower())))
                {
                    await context.RenameCategory(model.category);
                }

                wiki.Name = model.name;
                wiki.Description = model.description;
                wiki.Markdown = model.markdown;
                wiki.Sidebar.Markdown = model.sidebar.markdown;

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

            if (string.IsNullOrEmpty(model.category.name))
            {
                throw new Exception("The provided wiki does not specify a category");
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
            var sidebar = await context.Sidebars.FindAsync(wiki.SidebarId);
            var category = model.category.name;

            context.Sidebars.Remove(sidebar);
            context.Wikis.Remove(wiki);
            await context.SaveChangesAsync();
            await context.DeleteCategoryIfEmpty(category);
        }
    }
}