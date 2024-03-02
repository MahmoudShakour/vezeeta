using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDoctorSpecialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSpecializations",
                table: "DoctorSpecializations");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DoctorSpecializations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSpecializations",
                table: "DoctorSpecializations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecializations_SpecializationId_DoctorId",
                table: "DoctorSpecializations",
                columns: new[] { "SpecializationId", "DoctorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSpecializations",
                table: "DoctorSpecializations");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSpecializations_SpecializationId_DoctorId",
                table: "DoctorSpecializations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DoctorSpecializations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSpecializations",
                table: "DoctorSpecializations",
                columns: new[] { "SpecializationId", "DoctorId" });
        }
    }
}
