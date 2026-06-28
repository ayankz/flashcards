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
    }
}