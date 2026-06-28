namespace Flashcards.Api.Models;

public class WordCard
{
    public int Id { get; set; }
    public string Word { get; set; } = string.Empty;
    public string Translation { get; set; } = string.Empty;
    public string Example { get; set; } = string.Empty;
}