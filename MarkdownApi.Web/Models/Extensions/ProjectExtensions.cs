using MarkdownApi.Data;
using MarkdownApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MarkdownApi.Web.Models.Extensions
{
    public static class ProjectExtensions
    {
        public static Task<IEnumerable<ProjectModel>> GetProjects(this AppDbContext context)
        {
            return Task.Run(() =>
            {
                var model = context.Projects.Select(x => new ProjectModel
                {
                    id = x.Id,
                    name = x.Name,
                    description = x.Description
                }).OrderBy(x => x.name).AsEnumerable();

                return model;
            });
        }

        public async static Task<ProjectModel> GetProject(this AppDbContext context, int id)
        {
            var project = await context.Projects.FindAsync(id);

            var model = new ProjectModel
            {
                name = project.Name,
                description = project.Description
            };

            return model;
        }

        public static async Task<ProjectModel> AddProject(this AppDbContext context, ProjectModel model)
        {
            if (await model.Validate(context))
            {
                var project = new Project
                {
                    Name = model.name,
                    Description = model.description
                };

                context.Projects.Add(project);
                await context.SaveChangesAsync();

                model.id = project.Id;

                return model;
            }

            return new ProjectModel();
        }

        public static async Task UpdateProject(this AppDbContext context, ProjectModel model)
        {
            if (await model.Validate(context, true))
            {
                var project = await context.Projects.FindAsync(model.id);

                project.Name = model.name;
                project.Description = model.description;

                await context.SaveChangesAsync();
            }
        }

        public static async Task<bool> Validate(this ProjectModel model, AppDbContext context, bool updating = false)
        {
            if (updating)
            {
                if (!(model.id > 0))
                {
                    throw new Exception("The provided project does not specify an ID");
                }
            }

            if (await model.Exists(context, updating))
            {
                throw new Exception("The provided project name has already been used");
            }

            if (string.IsNullOrEmpty(model.name))
            {
                throw new Exception("The provided project does not specify a name");
            }

            if (string.IsNullOrEmpty(model.description))
            {
                throw new Exception("The provided project does not specify a description");
            }

            return true;
        }

        public static Task<bool> Exists(this ProjectModel model, AppDbContext context, bool updating)
        {
            return Task.Run(() =>
            {
                Project project;

                if (updating)
                {
                    project = context.Projects.FirstOrDefault(x => !(x.Id == model.id) && x.Name.ToLower().Equals(model.name.ToLower()));
                }
                else
                {
                    project = context.Projects.FirstOrDefault(x => x.Name.ToLower().Equals(model.name.ToLower()));
                }

                return project == null ? false : true;
            });
        }

        public static async Task DeleteProject(this AppDbContext context, ProjectModel model)
        {
            var project = await context.Projects.FindAsync(model.id);
            context.Projects.Remove(project);
            await context.SaveChangesAsync();
        }
    }
}