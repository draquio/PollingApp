using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollOptions_Polls_PollId",
                table: "PollOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_PollOptions_Polls_PollId",
                table: "PollOptions",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollOptions_Polls_PollId",
                table: "PollOptions");

            migrationBuilder.AddForeignKey(
                name: "FK_PollOptions_Polls_PollId",
                table: "PollOptions",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id");
        }
    }
}
