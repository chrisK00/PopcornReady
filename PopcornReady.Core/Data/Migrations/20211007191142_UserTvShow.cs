using Microsoft.EntityFrameworkCore.Migrations;

namespace PopcornReady.Core.Data.Migrations
{
    public partial class UserTvShow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_UserTvShows_TvShows_TvShowId",
                table: "UserTvShows",
                column: "TvShowId",
                principalTable: "TvShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTvShows_TvShows_TvShowId",
                table: "UserTvShows");
        }
    }
}
