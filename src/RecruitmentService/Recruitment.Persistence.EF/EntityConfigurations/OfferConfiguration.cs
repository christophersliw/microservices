using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recruitment.Domain.Enities;

namespace Recruitment.Persistence.EF.EntityConfigurations;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.ToTable("Offer").HasKey("Id");
        builder.Property(o => o.Name).IsRequired();
    }
}