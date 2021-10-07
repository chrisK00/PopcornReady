using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PopcornReady.Core.Data.Entities;

namespace PopcornReady.Core.Data.EntityConfigurations
{
    public class TvShowEntityConfiguration : IEntityTypeConfiguration<TvShow>
    {
        public void Configure(EntityTypeBuilder<TvShow> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
