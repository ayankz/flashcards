namespace Flashcards.Api.Models;

public class CreateWordCardsFromCandidatesRequest
{
    public ICollection<int> CandidateWordIds { get; set; } = new List<int>();
}