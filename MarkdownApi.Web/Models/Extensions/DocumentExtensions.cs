using MarkdownApi.Data;
using MarkdownApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkdownApi.Web.Models.Extensions
{
    public static class DocumentExtensions
    {
        public static Task<IEnumerable<DocumentModel>> GetDocuments(this AppDbContext context, int wikiId)
        {
            return Task.Run(() =>
            {
                var model = context.Documents.Where(x => x.WikiId == wikiId).Select(x => new DocumentModel
                {
                    id = x.Id,
                    title = x.Title,
                    markdown = x.Markdown,
                    wiki = new WikiModel
                    {
                        name = x.Wiki.Name,
                        description = x.Wiki.Description,
                        id = wikiId
                    }
                }).OrderBy(x => x.title).AsEnumerable();

                return model;
            });
        }

        public static async Task<DocumentModel> GetDocument(this AppDbContext context, int documentId)
        {
            var document = await context.Documents.FindAsync(documentId);
            var wiki = context.Wikis.Include("Category").FirstOrDefault(x => x.Id == document.WikiId);

            var model = new DocumentModel
            {
                id = document.Id,
                title = document.Title,
                markdown = document.Markdown,
                wiki = new WikiModel
                {
                    id = wiki.Id,
                    name = wiki.Name,
                    description = wiki.Description,
                    category = new CategoryModel
                    {
                        id = wiki.CategoryId,
                        name = wiki.Category.Name
                    }
                }
            };

            return model;            
        }

        public static async Task<DocumentModel> AddDocument(this AppDbContext context, DocumentModel model)
        {
            if (await model.Validate(context))
            {
                var document = new Document
                {
                    Title = model.title,
                    Markdown = model.markdown,
                    WikiId = model.wiki.id
                };

                context.Documents.Add(document);
                await context.SaveChangesAsync();

                model.id = document.Id;

                return model;
            }

            return new DocumentModel();
        }

        public static async Task UpdateDocument(this AppDbContext context, DocumentModel model)
        {
            if (await model.Validate(context, true))
            {
                var document = await context.Documents.FindAsync(model.id);

                document.Title = model.title;
                document.Markdown = model.markdown;

                await context.SaveChangesAsync();
            }
        }

        public static async Task<bool> Validate(this DocumentModel model, AppDbContext context, bool updating = false)
        {
            if (updating)
            {
                if (!(model.id > 0))
                {
                    throw new Exception("The provided document does not specify an ID");
                }
            }

            if (await model.Exists(context, updating))
            {
                throw new Exception("The provided document name has already been used in the wiki");
            }

            if (string.IsNullOrEmpty(model.title))
            {
                throw new Exception("The provided document does not specify a title");
            }

            return true;
        }

        public static Task<bool> Exists(this DocumentModel model, AppDbContext context, bool updating)
        {
            return Task.Run(() =>
            {
                Document document;

                if (updating)
                {
                    document = context.Documents.FirstOrDefault(x => x.WikiId == model.wiki.id && !(x.Id == model.id) && x.Title.ToLower().Equals(model.title.ToLower()));
                }
                else
                {
                    document = context.Documents.FirstOrDefault(x => x.WikiId == model.wiki.id && x.Title.ToLower().Equals(model.title.ToLower()));
                }

                return document == null ? false : true;
            });
        }

        public static async Task DeleteDocument(this AppDbContext context, DocumentModel model)
        {
            var document = await context.Documents.FindAsync(model.id);
            context.Documents.Remove(document);
            await context.SaveChangesAsync();
        }
    }
}