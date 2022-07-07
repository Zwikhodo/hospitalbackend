using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HospitalSystem.Authorization.Roles;
using HospitalSystem.Authorization.Users;
using HospitalSystem.MultiTenancy;
using HospitalSystem.Domain;

namespace HospitalSystem.EntityFrameworkCore
{
    public class HospitalSystemDbContext : AbpZeroDbContext<Tenant, Role, User, HospitalSystemDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeLog> EmployeeLogs { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescribedTest> PrescribedTests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Employee>("Employee")
                .HasValue<Patient>("Patient");
            modelBuilder.Entity<Person>()
                .Property("Discriminator")
                .IsRequired();
        }

        public HospitalSystemDbContext(DbContextOptions<HospitalSystemDbContext> options)
            : base(options)
        {
        }
    }
}
