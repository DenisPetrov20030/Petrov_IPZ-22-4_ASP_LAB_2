using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models
{
	public class HospitalDbContext : DbContext
	{
		public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }
		public DbSet<Appointment> Appointments => Set<Appointment>();
	}
}
