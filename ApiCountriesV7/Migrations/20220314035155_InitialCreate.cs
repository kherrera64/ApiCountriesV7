using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCountriesV7.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlphaCode2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlphaCode3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumericCode = table.Column<int>(type: "int", nullable: false),
                    LinkSubdivision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Independent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subdivisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subdivisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subdivisions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "AlphaCode2", "AlphaCode3", "Independent", "LinkSubdivision", "Name", "NumericCode" },
                values: new object[] { 1, "AF", "AFG", true, "https://en.wikipedia.org/wiki/ISO_3166-2:AF", "Afghanistan", 4 });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "AlphaCode2", "AlphaCode3", "Independent", "LinkSubdivision", "Name", "NumericCode" },
                values: new object[] { 2, "AX", "ALA", false, "https://en.wikipedia.org/wiki/ISO_3166-2:AX", "Aland Islands", 248 });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "AlphaCode2", "AlphaCode3", "Independent", "LinkSubdivision", "Name", "NumericCode" },
                values: new object[] { 3, "AL", "ALB", true, "https://en.wikipedia.org/wiki/ISO_3166-2:AL", "Albania", 8 });

            migrationBuilder.InsertData(
                table: "Subdivisions",
                columns: new[] { "Id", "Code", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, "AF-BDS", 1, "Badakhshān" },
                    { 2, "AF-BDG", 1, "Bādghīs" },
                    { 3, "AF-KAB", 1, "Kābul" },
                    { 4, "AL-01", 3, "Berat" },
                    { 5, "AL-04", 3, "Fier" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subdivisions_CountryId",
                table: "Subdivisions",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subdivisions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
