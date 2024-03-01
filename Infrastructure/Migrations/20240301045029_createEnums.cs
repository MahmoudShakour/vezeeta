using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patientDiscounts_Discounts_DiscountId",
                table: "patientDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_patientDiscounts_Patients_PatientId",
                table: "patientDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_patientDiscounts",
                table: "patientDiscounts");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "patientDiscounts",
                newName: "PatientDiscounts");

            migrationBuilder.RenameIndex(
                name: "IX_patientDiscounts_DiscountId",
                table: "PatientDiscounts",
                newName: "IX_PatientDiscounts_DiscountId");

            migrationBuilder.RenameColumn(
                name: "BookingStatus",
                table: "Bookings",
                newName: "Status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientDiscounts",
                table: "PatientDiscounts",
                columns: new[] { "PatientId", "DiscountId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiscounts_Discounts_DiscountId",
                table: "PatientDiscounts",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDiscounts_Patients_PatientId",
                table: "PatientDiscounts",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiscounts_Discounts_DiscountId",
                table: "PatientDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDiscounts_Patients_PatientId",
                table: "PatientDiscounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientDiscounts",
                table: "PatientDiscounts");

            migrationBuilder.RenameTable(
                name: "PatientDiscounts",
                newName: "patientDiscounts");

            migrationBuilder.RenameIndex(
                name: "IX_PatientDiscounts_DiscountId",
                table: "patientDiscounts",
                newName: "IX_patientDiscounts_DiscountId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bookings",
                newName: "BookingStatus");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_patientDiscounts",
                table: "patientDiscounts",
                columns: new[] { "PatientId", "DiscountId" });

            migrationBuilder.AddForeignKey(
                name: "FK_patientDiscounts_Discounts_DiscountId",
                table: "patientDiscounts",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_patientDiscounts_Patients_PatientId",
                table: "patientDiscounts",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
