using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectionsPortal.Server.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Collection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollectionCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CustomString1Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomString2Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomString3Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomInt1Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomInt2Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomInt3Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomText1Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomText2Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomText3Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomBool1Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomBool2Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomBool3Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomDate1Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomDate2Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomDate3Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_CollectionCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CollectionCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false),
                    CustomString1Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomString2Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomString3Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomInt1Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomInt2Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomInt3Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomText1Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomText2Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomText3Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomBool1Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomBool2Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomBool3Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomDate1Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomDate2Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomDate3Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionItem_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItemItemTag",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItemItemTag", x => new { x.ItemsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_CollectionItemItemTag_CollectionItem_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "CollectionItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionItemItemTag_ItemTag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "ItemTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItem_CollectionId",
                table: "CollectionItem",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItemItemTag_TagsId",
                table: "CollectionItemItemTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_CategoryId",
                table: "Collections",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollectionItemItemTag");

            migrationBuilder.DropTable(
                name: "CollectionItem");

            migrationBuilder.DropTable(
                name: "ItemTag");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "CollectionCategory");
        }
    }
}
