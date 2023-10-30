using System.Net.Http.Json;
using System.Text.Json;
using EmployeeScheduler.Models;
using EmployeeScheduler.Database;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;


// get initial state from endpoints
HttpClient client = new HttpClient();

client.BaseAddress = new UriBuilder("http://scheduling-interview-2021-265534043.us-west-2.elb.amazonaws.com").Uri;

// Console.WriteLine("Starting Http Request...");
// var scheduleResult = await client.GetAsync("http://scheduling-interview-2021-265534043.us-west-2.elb.amazonaws.com/api/Scheduling/Schedule?token=7edb774d-0af4-4469-81a1-c6e8acc562d5");
// Console.WriteLine("Finished Request");

// var initialSchedule = JsonSerializer.Deserialize<ScheduledAppointment[]>(await scheduleResult.Content.ReadAsStringAsync());

// if (initialSchedule == null)
// {
//     Console.WriteLine("there was no data returned from the initial state");
//     return;
// }

var db = new AppointmentContext();
// await db.AddRangeAsync(initialSchedule);
// db.SaveChanges();

bool hasNextAppointment;
do
{
    // get next appointment
    var appointmentResponse = await client.GetAsync("/api/Scheduling/AppointmentRequest?token=7edb774d-0af4-4469-81a1-c6e8acc562d5");

    var wasSuccess = int.TryParse(appointmentResponse.StatusCode.ToString(), out int status);
    if (!wasSuccess)
    {
        return;
    }

    hasNextAppointment = status == 200;
    var nextAppointment = JsonSerializer.Deserialize<AppointmentRequest>(await appointmentResponse.Content.ReadAsStringAsync());
    if (nextAppointment == null)
    {
        continue;
    }

    // Get next available schedule time
    var newAppointmentTime = getAvailableAppointment(nextAppointment);

    Appointment newAppointment = new Appointment()
    {

    };
    // schedule appointment
    try
    {
        await client.PostAsync("/api/Scheduling/Schedule?token=7edb774d-0af4-4469-81a1-c6e8acc562d5", JsonContent.Create(newAppointment));
    }
    catch (Exception e)
    {
        Console.WriteLine("There was an error: ", e);
    }
} while (hasNextAppointment);

async Task<DateTime> getAvailableAppointment(AppointmentRequest nextApp)
{
    // Getting Preferred Days
    var appointmentsOnAvailDays = await db.Appointments.OrderBy((x) => x.AppointmentTime).Where((a) => nextApp.PreferredDays.Any((pd) => pd.Date == a.AppointmentTime.Date) && nextApp.PreferredDocs.Any((d) => a.DoctorId == d)).ToListAsync();

    var time = getValidAppointmentTime(appointmentsOnAvailDays, nextApp.PreferredDays);

    return time;
}

DateTime getValidAppointmentTime(List<ScheduledAppointment> appts, List<DateTime> days)
{
    var apptTimes = appts.Select(a => a.AppointmentTime).ToList();
    // Get Unscheduled Hours for all days
    List<DateTime> allhours = new List<DateTime>();
    foreach (var day in days)
    {
        for (int i = 8; i <= 4; i++)
        {
            allhours.Add(new DateTime(day.Year, day.Month, day.Day, i, 0, 0));
        }
    }


    allhours.Except(apptTimes).Where((date) => ((date.Month == 11 || date.Month == 12) && date.Year == 2021) && ())

}

