using Microsoft.EntityFrameworkCore;
using PopcornReady.Core.Data.Entities;

namespace PopcornReady.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TvShow> TvShows { get; set; }
        public DbSet<Episode> Episodes { get; set; }
    }
}
