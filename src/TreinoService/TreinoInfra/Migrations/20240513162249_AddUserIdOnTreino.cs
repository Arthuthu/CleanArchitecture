using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TreinoInfra.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdOnTreino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Treinos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Treinos");
        }
    }
}
