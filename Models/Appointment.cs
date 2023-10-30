using System;

namespace EmployeeScheduler.Models;
public class Appointment
{
    public int DoctorId { get; set; }
    public int PersonId { get; set; }
    public DateTime AppointmentTime { get; set; }
    public bool isNewPatientAppointment { get; set; }
    public int RequestId { get; set; }
}