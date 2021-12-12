using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PopcornReadyV2.Business.Data.Entities;

namespace PopcornReadyV2.Business.Data.EntityConfigurations
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
