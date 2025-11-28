using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Data.Models
{
	public class HospitalDbContext : DbContext
	{
		public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }
		
		public DbSet<Appointment> Appointments => Set<Appointment>();
		public DbSet<Doctor> Doctors => Set<Doctor>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Doctor>()
				.HasMany(d => d.Appointments)
				.WithOne(a => a.Doctor)
				.HasForeignKey(a => a.DoctorID)
				.OnDelete(DeleteBehavior.SetNull);
		}
	}
}
