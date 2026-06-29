namespace Flashcards.Api.Models;

public class CandidateWord
{
    public int Id
    {
        get; set;
    }
    public int PageAnalysisId
    {
        get; set;
    }
    public string Word { get; set; } = string.Empty;
    public string Translation { get; set; } = string.Empty;
    public string Example { get; set; } = string.Empty;
    public PageAnalysis PageAnalysis { get; set; } = null!;
}
