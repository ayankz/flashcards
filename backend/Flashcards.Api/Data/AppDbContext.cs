using Flashcards.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<WordCard> WordCards => Set<WordCard>();
    public DbSet<PageScan> PageScans => Set<PageScan>();
    public DbSet<PageAnalysis> PageAnalyses => Set<PageAnalysis>();
    public DbSet<CandidateWord> CandidateWords => Set<CandidateWord>();
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}
