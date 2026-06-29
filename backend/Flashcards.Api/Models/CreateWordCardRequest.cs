namespace Flashcards.Api.Models;

public record CreateWordCardRequest(
    string Word,
    string Translation,
    string Example
);
