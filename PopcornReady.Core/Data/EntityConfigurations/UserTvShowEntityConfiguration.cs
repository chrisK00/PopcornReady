using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PopcornReady.Core.Data.Entities;

namespace PopcornReady.Core.Data.EntityConfigurations
{
    public class UserTvShowEntityConfiguration : IEntityTypeConfiguration<UserTvShow>
    {
        public void Configure(EntityTypeBuilder<UserTvShow> builder)
        {
            builder.HasKey(pk => new { pk.TvShowId, pk.UserId });

            builder.HasOne<AppUser>().WithMany()
                .HasForeignKey(x => x.UserId);
            builder.HasOne<TvShow>().WithMany()
                .HasForeignKey(x => x.TvShowId);
        }
    }
}
