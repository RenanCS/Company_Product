using Microsoft.EntityFrameworkCore.Migrations;

namespace Company.ProducApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategoriaProduto",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategoriaProduto", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "tblProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Perishable = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblProduto_tblCategoriaProduto_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tblCategoriaProduto",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblCategoriaProduto",
                columns: new[] { "CategoryId", "Active", "Description", "Name" },
                values: new object[,]
                {
                    { 1, true, "Eletrodomésticos", "Eletrônico" },
                    { 2, true, "Produtos para Informática", "Informática" },
                    { 3, true, "Aparelhos e acessórios", "Celulares" },
                    { 4, true, "Artigos para vestuário em geral", "Moda" },
                    { 5, true, "Livros", "Livros" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProduto_CategoryId",
                table: "tblProduto",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProduto");

            migrationBuilder.DropTable(
                name: "tblCategoriaProduto");
        }
    }
}
