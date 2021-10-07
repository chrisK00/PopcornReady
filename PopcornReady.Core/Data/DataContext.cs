using Microsoft.EntityFrameworkCore;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Data.EntityConfigurations;

namespace PopcornReady.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(UserTvShowEntityConfiguration).Assembly);
        }

        // TODO: Remove when implement identity user
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<UserTvShow> UserTvShows { get; set; }
        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Episode> Episodes { get; set; }
    }
}