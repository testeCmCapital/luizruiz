using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmCapital.Backoffice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTax1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Taxs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 26, 15, 13, 11, 852, DateTimeKind.Local).AddTicks(7738));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 26, 15, 13, 11, 852, DateTimeKind.Local).AddTicks(7007),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 26, 14, 3, 48, 830, DateTimeKind.Local).AddTicks(8899));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Taxs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 26, 14, 3, 48, 830, DateTimeKind.Local).AddTicks(8899),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 5, 26, 15, 13, 11, 852, DateTimeKind.Local).AddTicks(7007));
        }
    }
}
