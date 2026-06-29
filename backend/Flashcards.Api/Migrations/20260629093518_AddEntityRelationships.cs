using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flashcards.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PageAnalyses_PageScanId",
                table: "PageAnalyses",
                column: "PageScanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateWords_PageAnalysisId",
                table: "CandidateWords",
                column: "PageAnalysisId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateWords_PageAnalyses_PageAnalysisId",
                table: "CandidateWords",
                column: "PageAnalysisId",
                principalTable: "PageAnalyses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageAnalyses_PageScans_PageScanId",
                table: "PageAnalyses",
                column: "PageScanId",
                principalTable: "PageScans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateWords_PageAnalyses_PageAnalysisId",
                table: "CandidateWords");

            migrationBuilder.DropForeignKey(
                name: "FK_PageAnalyses_PageScans_PageScanId",
                table: "PageAnalyses");

            migrationBuilder.DropIndex(
                name: "IX_PageAnalyses_PageScanId",
                table: "PageAnalyses");

            migrationBuilder.DropIndex(
                name: "IX_CandidateWords_PageAnalysisId",
                table: "CandidateWords");
        }
    }
}
