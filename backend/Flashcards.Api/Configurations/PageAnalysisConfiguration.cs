using Flashcards.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Flashcards.Api.Configurations;

public class PageAnalysisConfiguration : IEntityTypeConfiguration<PageAnalysis>
{
    public void Configure(EntityTypeBuilder<PageAnalysis> builder)
    {
        builder.HasMany(pa => pa.CandidateWords)
            .WithOne(cw => cw.PageAnalysis)
            .HasForeignKey(cw => cw.PageAnalysisId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}