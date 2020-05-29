
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NewsFeedVn.Models;

namespace NewsFeedVn.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NewsFeedVn.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NewsFeedVn.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole {Name = "Admin"};

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "newsfeedvn"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "newsfeedvn",
                    Email = "newsfeedvn@yopmail.com"
                };

                manager.Create(user, "Abcabc123@@");
                manager.AddToRole(user.Id, "Admin");
            }

            //context.Database.ExecuteSqlCommand("TRUNCATE TABLE Categories");
            Category[] categories =
            {
                new Category()
                {
                    Id = 1,
                    Name = "Society"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Business"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Arts"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Technology"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Health"
                }
            };
            context.Categories.AddOrUpdate(categories);

        }
    }
}
