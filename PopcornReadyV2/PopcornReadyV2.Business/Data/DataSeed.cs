using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PopcornReady.Core.Services;
using PopcornReadyV2.Business.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopcornReadyV2.Business.Data
{
    public static class DataSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, AppDbContext context, ITvShowsService tvShowsService, IMapper mapper)
        {
            await context.Database.EnsureCreatedAsync();

            if (userManager.Users.Any())
                return;

            var users = new List<AppUser>
            {
                new AppUser { UserName = "chris@gmail.com", Email = "chris@gmail.com"},
                new AppUser { UserName = "hi@gmail.com", Email = "hi@gmail.com"},
                new AppUser { UserName = "ck@gmail.com", Email = "ck@gmail.com"}
            };

            foreach (var item in users)
            {
                await userManager.CreateAsync(item, "password");
            }

            string[] tvShowsToFind = { "the flash", "supergirl", "you" };

            foreach (var item in tvShowsToFind)
            {
                var tvShow = await tvShowsService.FindAsync(item);
                await tvShowsService.AddAsync(tvShow.Name, users[0].Id);
            }
        }
    }
}
