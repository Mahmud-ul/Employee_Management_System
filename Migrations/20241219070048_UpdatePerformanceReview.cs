using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePerformanceReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PerformanceReviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PerformanceReviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Departments");
        }
    }
}
