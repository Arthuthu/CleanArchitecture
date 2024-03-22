using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserInfra.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubscriptionIdProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Users");
        }
    }
}
