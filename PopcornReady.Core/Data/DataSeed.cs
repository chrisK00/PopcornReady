using Microsoft.EntityFrameworkCore;
using PopcornReady.Core.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PopcornReady.Core.Data
{
    public static class DataSeed
    {
        public static void SeedUsers(DataContext context)
        {
            context.Database.Migrate();

            if (context.AppUsers.Any())
            {
                return;
            }

            var users = new List<AppUser>
            {
                new AppUser {Id = 1, UserName = "Chris"},
                new AppUser { Id = 2, UserName = "Mr Robot"},
                new AppUser { Id = 3, UserName = "Frenchie"}
            };

            context.AddRange(users);
            context.SaveChanges();
        }
    }
}
