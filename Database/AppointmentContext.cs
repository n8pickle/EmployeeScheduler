using EmployeeScheduler.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace EmployeeScheduler.Database
{
    public class AppointmentContext : DbContext
    {
        public DbSet<ScheduledAppointment> Appointments { get; set; }
        public string DbPath { get; }

        public AppointmentContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "employeeScheduler.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}