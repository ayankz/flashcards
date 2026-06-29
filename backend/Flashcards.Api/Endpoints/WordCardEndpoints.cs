using Flashcards.Api.Data;
using Flashcards.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Api.Endpoints;

public static class WordCardEndpoints
{
    public static void MapWordCardEndpoints(this WebApplication app)
    {
        app.MapGet("/api/wordcards", async (AppDbContext dbContext) =>
        {
            var wordCards = await dbContext.WordCards.ToListAsync();
            return Results.Ok(wordCards);
        })
        .WithName("GetWordCards")
        .WithOpenApi();

        app.MapPost("/api/wordcards", async (CreateWordCardRequest request, AppDbContext dbContext) =>
        {
            var validationResult = ValidateWordCard(
                request.Word,
                request.Translation,
                request.Example);

            if (validationResult is not null)
            {
                return validationResult;
            }

            var newWordCard = new WordCard
            {
                Word = request.Word,
                Translation = request.Translation,
                Example = request.Example
            };

            dbContext.WordCards.Add(newWordCard);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/api/wordcards/{newWordCard.Id}", newWordCard);
        })
        .WithName("CreateWordCard")
        .WithOpenApi();

        app.MapGet("/api/wordcards/{id}", async (int id, AppDbContext dbContext) =>
        {
            var wordCard = await dbContext.WordCards.FindAsync(id);

            if (wordCard is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(wordCard);
        })
        .WithName("GetWordCardById")
        .WithOpenApi();

        app.MapDelete("/api/wordcards/{id}", async (int id, AppDbContext dbContext) =>
        {
            var wordCard = await dbContext.WordCards.FindAsync(id);

            if (wordCard is null)
            {
                return Results.NotFound();
            }

            dbContext.WordCards.Remove(wordCard);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteWordCard")
        .WithOpenApi();

        app.MapPut("/api/wordcards/{id}", async (int id, UpdateWordCardRequest request, AppDbContext dbContext) =>
        {
            var validationResult = ValidateWordCard(
     request.Word,
     request.Translation,
     request.Example);

            if (validationResult is not null)
            {
                return validationResult;
            }

            var wordCard = await dbContext.WordCards.FindAsync(id);

            if (wordCard is null)
            {
                return Results.NotFound();
            }

            wordCard.Word = request.Word;
            wordCard.Translation = request.Translation;
            wordCard.Example = request.Example;

            await dbContext.SaveChangesAsync();

            return Results.Ok(wordCard);
        })
        .WithName("UpdateWordCard")
        .WithOpenApi();

        app.MapPost("/api/wordcards/from-candidates", async (CreateWordCardsFromCandidatesRequest request, AppDbContext dbContext) =>
        {
            if (request.CandidateWordIds.Count == 0)
            {
                return Results.BadRequest("At least one candidate word ID is required.");
            }

            var candidateWords = await dbContext.CandidateWords
                .Where(cw => request.CandidateWordIds.Contains(cw.Id))
                .ToListAsync();

            if (candidateWords.Count == 0)
            {
                return Results.NotFound("No candidate words found for the provided IDs.");
            }

            if (request.CandidateWordIds.Count != candidateWords.Count)
            {
                return Results.BadRequest("Some candidate word IDs do not exist.");
            }

            var wordCards = candidateWords.Select(cw => new WordCard
            {
                Word = cw.Word,
                Translation = cw.Translation,
                Example = cw.Example
            }).ToList();

            dbContext.WordCards.AddRange(wordCards);
            await dbContext.SaveChangesAsync();

            return Results.Ok(wordCards);
        })
        .WithName("CreateWordCardsFromCandidates")
        .WithOpenApi();
    }

    private static IResult? ValidateWordCard(
    string word,
    string translation,
    string example)
    {
        if (string.IsNullOrWhiteSpace(word) ||
            string.IsNullOrWhiteSpace(translation) ||
            string.IsNullOrWhiteSpace(example))
        {
            return Results.BadRequest("All fields are required.");
        }

        return null;
    }
}
