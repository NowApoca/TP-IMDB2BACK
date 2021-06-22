using Microsoft.EntityFrameworkCore.Migrations;

namespace Prueba11.Migrations
{
    public partial class Prueba11ContextEscuelaDatabaseContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Celebrities",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    rating = table.Column<int>(nullable: false),
                    image = table.Column<string>(nullable: true),
                    surname = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    biography = table.Column<string>(nullable: true),
                    bornDate = table.Column<string>(nullable: true),
                    genres = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Celebrities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    image = table.Column<string>(nullable: true),
                    subtitle = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false),
                    rating = table.Column<double>(nullable: false),
                    summary = table.Column<string>(nullable: true),
                    director = table.Column<string>(nullable: true),
                    productor = table.Column<string>(nullable: true),
                    writers = table.Column<string>(nullable: true),
                    stars = table.Column<string>(nullable: true),
                    productorCountry = table.Column<string>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    releaseDate = table.Column<string>(nullable: true),
                    duration = table.Column<int>(nullable: false),
                    genre = table.Column<string>(nullable: true),
                    budget = table.Column<string>(nullable: true),
                    earns = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LinkedCelebrityWithCelebrities",
                columns: table => new
                {
                    celebrityId1 = table.Column<string>(nullable: false),
                    celebrityId2 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedCelebrityWithCelebrities", x => new { x.celebrityId1, x.celebrityId2 });
                });

            migrationBuilder.CreateTable(
                name: "LinkedItemWithCelebrities",
                columns: table => new
                {
                    itemId = table.Column<string>(nullable: false),
                    celebrityId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedItemWithCelebrities", x => new { x.itemId, x.celebrityId });
                });

            migrationBuilder.CreateTable(
                name: "LinkedItemWithItems",
                columns: table => new
                {
                    itemId1 = table.Column<string>(nullable: false),
                    itemId2 = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkedItemWithItems", x => new { x.itemId1, x.itemId2 });
                });

            migrationBuilder.CreateTable(
                name: "RatingCelebrities",
                columns: table => new
                {
                    userName = table.Column<string>(nullable: false),
                    celebrityId = table.Column<string>(nullable: false),
                    rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingCelebrities", x => new { x.celebrityId, x.userName });
                });

            migrationBuilder.CreateTable(
                name: "RatingItems",
                columns: table => new
                {
                    userName = table.Column<string>(nullable: false),
                    itemId = table.Column<string>(nullable: false),
                    rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingItems", x => new { x.userName, x.itemId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userName = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userName);
                });

            migrationBuilder.CreateTable(
                name: "CommentCelebrities",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    ParentCommentCelebrityid = table.Column<string>(nullable: true),
                    Celebrityid = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentCelebrities", x => x.id);
                    table.ForeignKey(
                        name: "FK_CommentCelebrities_Celebrities_Celebrityid",
                        column: x => x.Celebrityid,
                        principalTable: "Celebrities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentCelebrities_CommentCelebrities_ParentCommentCelebrityid",
                        column: x => x.ParentCommentCelebrityid,
                        principalTable: "CommentCelebrities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentCelebrities_Users_userName",
                        column: x => x.userName,
                        principalTable: "Users",
                        principalColumn: "userName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentItems",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    ParentCommentItemid = table.Column<string>(nullable: true),
                    Itemid = table.Column<int>(nullable: true),
                    userName = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    isDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_CommentItems_Items_Itemid",
                        column: x => x.Itemid,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentItems_CommentItems_ParentCommentItemid",
                        column: x => x.ParentCommentItemid,
                        principalTable: "CommentItems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentItems_Users_userName",
                        column: x => x.userName,
                        principalTable: "Users",
                        principalColumn: "userName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    token = table.Column<string>(nullable: false),
                    expired = table.Column<bool>(nullable: false),
                    userName = table.Column<string>(nullable: true),
                    issuedAt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.token);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_userName",
                        column: x => x.userName,
                        principalTable: "Users",
                        principalColumn: "userName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReactionCommentCelebritys",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    CommentCelebrityid = table.Column<string>(nullable: true),
                    reactionType = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionCommentCelebritys", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReactionCommentCelebritys_CommentCelebrities_CommentCelebrityid",
                        column: x => x.CommentCelebrityid,
                        principalTable: "CommentCelebrities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReactionCommentCelebritys_Users_userName",
                        column: x => x.userName,
                        principalTable: "Users",
                        principalColumn: "userName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReactionCommentItems",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    CommentItemid = table.Column<string>(nullable: true),
                    reactionType = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactionCommentItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReactionCommentItems_CommentItems_CommentItemid",
                        column: x => x.CommentItemid,
                        principalTable: "CommentItems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReactionCommentItems_Users_userName",
                        column: x => x.userName,
                        principalTable: "Users",
                        principalColumn: "userName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentCelebrities_Celebrityid",
                table: "CommentCelebrities",
                column: "Celebrityid");

            migrationBuilder.CreateIndex(
                name: "IX_CommentCelebrities_ParentCommentCelebrityid",
                table: "CommentCelebrities",
                column: "ParentCommentCelebrityid");

            migrationBuilder.CreateIndex(
                name: "IX_CommentCelebrities_userName",
                table: "CommentCelebrities",
                column: "userName");

            migrationBuilder.CreateIndex(
                name: "IX_CommentItems_Itemid",
                table: "CommentItems",
                column: "Itemid");

            migrationBuilder.CreateIndex(
                name: "IX_CommentItems_ParentCommentItemid",
                table: "CommentItems",
                column: "ParentCommentItemid");

            migrationBuilder.CreateIndex(
                name: "IX_CommentItems_userName",
                table: "CommentItems",
                column: "userName");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionCommentCelebritys_CommentCelebrityid",
                table: "ReactionCommentCelebritys",
                column: "CommentCelebrityid");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionCommentCelebritys_userName",
                table: "ReactionCommentCelebritys",
                column: "userName");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionCommentItems_CommentItemid",
                table: "ReactionCommentItems",
                column: "CommentItemid");

            migrationBuilder.CreateIndex(
                name: "IX_ReactionCommentItems_userName",
                table: "ReactionCommentItems",
                column: "userName");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_userName",
                table: "Sessions",
                column: "userName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkedCelebrityWithCelebrities");

            migrationBuilder.DropTable(
                name: "LinkedItemWithCelebrities");

            migrationBuilder.DropTable(
                name: "LinkedItemWithItems");

            migrationBuilder.DropTable(
                name: "RatingCelebrities");

            migrationBuilder.DropTable(
                name: "RatingItems");

            migrationBuilder.DropTable(
                name: "ReactionCommentCelebritys");

            migrationBuilder.DropTable(
                name: "ReactionCommentItems");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "CommentCelebrities");

            migrationBuilder.DropTable(
                name: "CommentItems");

            migrationBuilder.DropTable(
                name: "Celebrities");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
