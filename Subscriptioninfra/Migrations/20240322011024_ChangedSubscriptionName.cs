using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubscriptionInfra.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSubscriptionName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Subscriptions",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Subscriptions",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
