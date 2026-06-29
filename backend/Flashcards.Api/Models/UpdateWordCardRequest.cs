namespace Flashcards.Api.Models;

public record UpdateWordCardRequest(
    string Word,
    string Translation,
    string Example
);
