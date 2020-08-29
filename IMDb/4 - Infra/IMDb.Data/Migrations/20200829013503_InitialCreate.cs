using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDb.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "IMDb");

            migrationBuilder.CreateTable(
                name: "Cast",
                schema: "IMDb",
                columns: table => new
                {
                    CastId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    CastType = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cast", x => x.CastId);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                schema: "IMDb",
                columns: table => new
                {
                    MovieId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Genre = table.Column<byte>(nullable: false),
                    Mean = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "IMDb",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "CastOfMovie",
                schema: "IMDb",
                columns: table => new
                {
                    CastOfMovieId = table.Column<Guid>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false),
                    CastId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastOfMovie", x => x.CastOfMovieId);
                    table.ForeignKey(
                        name: "FK_CastOfMovie_Cast_CastId",
                        column: x => x.CastId,
                        principalSchema: "IMDb",
                        principalTable: "Cast",
                        principalColumn: "CastId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastOfMovie_Movie_MovieId",
                        column: x => x.MovieId,
                        principalSchema: "IMDb",
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "IMDb",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "IMDb",
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingOfMovie",
                schema: "IMDb",
                columns: table => new
                {
                    RatingOfMovieId = table.Column<Guid>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    MovieId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingOfMovie", x => x.RatingOfMovieId);
                    table.ForeignKey(
                        name: "FK_RatingOfMovie_Movie_MovieId",
                        column: x => x.MovieId,
                        principalSchema: "IMDb",
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingOfMovie_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "IMDb",
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastOfMovie_CastId",
                schema: "IMDb",
                table: "CastOfMovie",
                column: "CastId");

            migrationBuilder.CreateIndex(
                name: "IX_CastOfMovie_MovieId",
                schema: "IMDb",
                table: "CastOfMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingOfMovie_MovieId",
                schema: "IMDb",
                table: "RatingOfMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingOfMovie_UserId",
                schema: "IMDb",
                table: "RatingOfMovie",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "IMDb",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastOfMovie",
                schema: "IMDb");

            migrationBuilder.DropTable(
                name: "RatingOfMovie",
                schema: "IMDb");

            migrationBuilder.DropTable(
                name: "Cast",
                schema: "IMDb");

            migrationBuilder.DropTable(
                name: "Movie",
                schema: "IMDb");

            migrationBuilder.DropTable(
                name: "User",
                schema: "IMDb");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "IMDb");
        }
    }
}
