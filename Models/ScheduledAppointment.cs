using System;
using System.Text.Json.Serialization;

namespace EmployeeScheduler.Models;
public class ScheduledAppointment
{
    [JsonIgnore]
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public int PersonId { get; set; }
    public DateTime AppointmentTime { get; set; }
    public bool IsNewPatientAppointment { get; set; }
}