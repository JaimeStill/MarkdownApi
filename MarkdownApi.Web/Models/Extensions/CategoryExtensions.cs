using MarkdownApi.Data;
using MarkdownApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarkdownApi.Web.Models.Extensions
{
    public static class CategoryExtensions
    {
        public static Task<IEnumerable<CategoryModel>> GetCategories(this AppDbContext context)
        {
            return Task.Run(() =>
            {
                var model = context.Categories.Select(x => new CategoryModel
                {
                    id = x.Id,
                    name = x.Name,
                    wikis = x.Wikis.Select(y => new WikiModel
                    {
                        id = y.Id,
                        name = y.Name,
                        description = y.Description
                    }).OrderBy(y => y.name).AsEnumerable()
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }

        public static async Task<bool> RenameCategory(this AppDbContext context, CategoryModel model)
        {
            if (string.IsNullOrEmpty(model.name))
            {
                throw new Exception("The provided category name must have a value");
            }

            var check = context.Categories.FirstOrDefault(x => !(x.Id == model.id) && x.Name.ToLower().Equals(model.name.ToLower()));

            if (check != null)
            {
                await model.RedirectWikiCategory(context, check.Id);
                await context.DeleteCategoryIfEmpty(model.name);
                return true;
            }
            else
            {
                var category = await context.Categories.FindAsync(model.id);
                category.Name = model.name;
                await context.SaveChangesAsync();
                return false;
            }
        }

        public static async Task RedirectWikiCategory(this CategoryModel model, AppDbContext context, int id)
        {
            var wikis = context.Wikis.Where(x => x.CategoryId == model.id);

            foreach (var wiki in wikis)
            {
                wiki.CategoryId = id;
            }

            await context.SaveChangesAsync();
        }

        public static async Task DeleteCategoryIfEmpty(this AppDbContext context, string name)
        {
            var category = context.Categories.Include("Wikis").FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));

            if (category.Wikis.Count < 1)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }

        public static async Task<Category> CreateCategoryIfNotExists(this AppDbContext context, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("The provided category name must have a value");
            }

            var model = new CategoryModel
            {
                name = name
            };

            var category = context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));

            if (category == null)
            {
                category = new Category
                {
                    Name = name
                };

                context.Categories.Add(category);
                await context.SaveChangesAsync();
            }

            return category;
        }
    }
}