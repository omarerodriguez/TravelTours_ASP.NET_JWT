using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelTours.Core.Entities;

namespace TravelTours.Infraestructure.Config
{
    public class PlaceConfiguration: IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.Property(l => l.Id).IsRequired();
            builder.Property(l => l.Name).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Description).IsRequired();
            builder.Property(l => l.ApproximateCost).IsRequired();
            //RelationShips
            builder.HasOne(c => c.Category)
                .WithMany().HasForeignKey(l => l.CategoryId);

            builder.HasOne(p => p.Country)
                .WithMany().HasForeignKey(l => l.CountryId);
        }
    }
}
