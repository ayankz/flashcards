namespace Flashcards.Api.Models;

public class PageScan
{
    public int Id
    {
        get; set;
    }
    public string ImagePath { get; set; } = string.Empty;
    public PageScanStatus Status { get; set; } = PageScanStatus.Pending;
    public PageAnalysis? PageAnalysis
    {
        get; set;
    }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}