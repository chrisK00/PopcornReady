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
                new AppUser { UserName = "Chris"},
                new AppUser { UserName = "Mr Robot"},
                new AppUser { UserName = "Frenchie"}
            };

            context.AddRange(users);
            context.SaveChanges();
        }
    }
}
