namespace Flashcards.Api.Models;

public class PageAnalysis
{
    public int Id
    {
        get; set;
    }
    public int PageScanId
    {
        get; set;
    }
    public string ExtractedText { get; set; } = string.Empty;
    public string TranslatedText { get; set; } = string.Empty;
    public PageScan PageScan { get; set; } = null!;
    public ICollection<CandidateWord> CandidateWords { get; set; } = new List<CandidateWord>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
