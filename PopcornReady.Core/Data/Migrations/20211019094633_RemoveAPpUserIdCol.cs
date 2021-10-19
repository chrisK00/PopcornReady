using Microsoft.EntityFrameworkCore.Migrations;

namespace PopcornReady.Core.Data.Migrations
{
    public partial class RemoveAPpUserIdCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTvShows_AppUsers_AppUserId",
                table: "UserTvShows");

            migrationBuilder.DropIndex(
                name: "IX_UserTvShows_AppUserId",
                table: "UserTvShows");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserTvShows");

            migrationBuilder.CreateIndex(
                name: "IX_UserTvShows_UserId",
                table: "UserTvShows",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTvShows_AppUsers_UserId",
                table: "UserTvShows",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTvShows_AppUsers_UserId",
                table: "UserTvShows");

            migrationBuilder.DropIndex(
                name: "IX_UserTvShows_UserId",
                table: "UserTvShows");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "UserTvShows",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTvShows_AppUserId",
                table: "UserTvShows",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTvShows_AppUsers_AppUserId",
                table: "UserTvShows",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
