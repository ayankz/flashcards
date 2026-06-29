using Flashcards.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flashcards.Api.Configurations;

public class PageScanConfiguration : IEntityTypeConfiguration<PageScan>
{
    public void Configure(EntityTypeBuilder<PageScan> builder)
    {
        builder.HasOne(ps => ps.PageAnalysis)
            .WithOne(pa => pa.PageScan)
            .HasForeignKey<PageAnalysis>(pa => pa.PageScanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
