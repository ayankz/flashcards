using Flashcards.Api.Data;
using Flashcards.Api.Models;

namespace Flashcards.Api.Endpoints;

public static class PageScanEndpoints
{
    public static void MapPageScanEndpoints(this WebApplication app)
    {

        app.MapPost("/api/page-scans", async (CreatePageScanRequest request, AppDbContext dbContext) =>
         {
             var validationResult = ValidatePageScan(
                request.ImagePath);

             if (validationResult is not null)
             {
                 return validationResult;
             }
             var newPageScan = new PageScan
             {
                 ImagePath = request.ImagePath
             };

             dbContext.PageScans.Add(newPageScan);
             await dbContext.SaveChangesAsync();

             return Results.Created($"/api/page-scans/{newPageScan.Id}", newPageScan);
         })
         .WithName("CreatePageScan")
         .WithOpenApi();


        app.MapGet("/api/page-scans/{id}", async (int id, AppDbContext dbContext) =>
        {
            var pageScan = await dbContext.PageScans.FindAsync(id);

            if (pageScan is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(pageScan);
        })
        .WithName("GetPageScanById")
        .WithOpenApi();
    }
    private static IResult? ValidatePageScan(
         string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
        {
            return Results.BadRequest("Image path is required.");
        }

        return null;
    }
}
