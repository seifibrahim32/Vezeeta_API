using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Times__timesID__4959E263",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Times__D2130A06C961870D",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_timesID",
                table: "Times");

            migrationBuilder.RenameColumn(
                name: "discountCode",
                table: "Times",
                newName: "discountDoctorCode");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentBookingId",
                table: "Times",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Times__D2130A06851B1C6A",
                table: "Times",
                column: "discountID");

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    discountCode = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    discountType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Discount__3D87979B1AA57117", x => x.discountCode);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Times_AppointmentBookingId",
                table: "Times",
                column: "AppointmentBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Times_discountDoctorCode",
                table: "Times",
                column: "discountDoctorCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Appointments_AppointmentBookingId",
                table: "Times",
                column: "AppointmentBookingId",
                principalTable: "Appointments",
                principalColumn: "bookingID");

            migrationBuilder.AddForeignKey(
                name: "FK__Times__discountD__019E3B86",
                table: "Times",
                column: "discountDoctorCode",
                principalTable: "Discount",
                principalColumn: "discountCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Times_Appointments_AppointmentBookingId",
                table: "Times");

            migrationBuilder.DropForeignKey(
                name: "FK__Times__discountD__019E3B86",
                table: "Times");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Times__D2130A06851B1C6A",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_AppointmentBookingId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_discountDoctorCode",
                table: "Times");

            migrationBuilder.DropColumn(
                name: "AppointmentBookingId",
                table: "Times");

            migrationBuilder.RenameColumn(
                name: "discountDoctorCode",
                table: "Times",
                newName: "discountCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Times__D2130A06C961870D",
                table: "Times",
                column: "discountID");

            migrationBuilder.CreateIndex(
                name: "IX_Times_timesID",
                table: "Times",
                column: "timesID");

            migrationBuilder.AddForeignKey(
                name: "FK__Times__timesID__4959E263",
                table: "Times",
                column: "timesID",
                principalTable: "Appointments",
                principalColumn: "bookingID");
        }
    }
}
