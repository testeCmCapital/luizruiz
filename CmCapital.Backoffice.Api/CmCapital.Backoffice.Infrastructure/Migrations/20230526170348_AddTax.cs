using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmCapital.Backoffice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 26, 14, 3, 48, 830, DateTimeKind.Local).AddTicks(8899));

            migrationBuilder.CreateTable(
                name: "Taxs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InitialValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taxs");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Purchases");
        }
    }
}
