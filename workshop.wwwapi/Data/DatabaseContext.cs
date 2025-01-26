using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Globalization;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Data
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString;
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Appointment Key etc.. Add Here
            modelBuilder.Entity<Appointment>().
                HasKey(p => new { p.PatientId, p.DoctorId });

            modelBuilder.Entity<Appointment>(). // Entity is Appointment
                //HasOne<Doctor>().             // Note: if no navigation properties are used: Define relation type with generic
                HasOne(p => p.Doctor).          // Each Entry has exactly One Doctor
                WithMany(p => p.Appointments).  // where the Doctor has a list of Appointments
                HasForeignKey(p => p.DoctorId); // Each of the Doctors Appointments stores a foreign key that references the Doctor

            
            modelBuilder.Entity<Appointment>().
                HasOne(p => p.Patient).
                WithMany(pa => pa.Appointments).
                HasForeignKey(ap => ap.PatientId);

            //Seed Data Here
            var seeder = new Seeder(false);
            modelBuilder.Entity<Patient>().
                HasData(seeder.Patients);

            modelBuilder.Entity<Doctor>().
                HasData(seeder.Doctors);

            modelBuilder.Entity<Appointment>().
                HasData(seeder.Appointments);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "Database");
            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message)); //see the sql EF using in the console

        }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
