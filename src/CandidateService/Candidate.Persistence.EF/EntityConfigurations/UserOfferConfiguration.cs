using Candidate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidate.Persistence.EF.EntityConfigurations;

public class UserOfferConfiguration : IEntityTypeConfiguration<UserOffer>
{
    public void Configure(EntityTypeBuilder<UserOffer> builder)
    {
        builder.ToTable("UserOffer").HasKey("Id");
        builder.Property(uo => uo.ApplicationDate).IsRequired();
        builder.HasOne(uo => uo.User)
            .WithMany(u => u.UserOffers)
            .HasForeignKey(uo => uo.UserId)
            .IsRequired();
    }
}