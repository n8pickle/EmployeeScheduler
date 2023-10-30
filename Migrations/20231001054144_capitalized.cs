using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeScheduler.Migrations
{
    /// <inheritdoc />
    public partial class capitalized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isNewPatientAppointment",
                table: "Appointments",
                newName: "IsNewPatientAppointment");

            migrationBuilder.RenameColumn(
                name: "appointmentTime",
                table: "Appointments",
                newName: "AppointmentTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsNewPatientAppointment",
                table: "Appointments",
                newName: "isNewPatientAppointment");

            migrationBuilder.RenameColumn(
                name: "AppointmentTime",
                table: "Appointments",
                newName: "appointmentTime");
        }
    }
}
