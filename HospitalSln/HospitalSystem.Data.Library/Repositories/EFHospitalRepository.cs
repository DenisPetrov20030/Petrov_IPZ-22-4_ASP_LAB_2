using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Data.Repositories
{
	public class EFHospitalRepository : IHospitalRepository
	{
		private Models.HospitalDbContext context;
		public EFHospitalRepository(Models.HospitalDbContext ctx)
		{
			context = ctx;
		}

		public IQueryable<Models.Appointment> Appointments => context.Appointments.Include(a => a.Doctor);

		public void CreateAppointment(Models.Appointment appointment)
		{
			context.Appointments.Add(appointment);
			context.SaveChanges();
		}

		public void UpdateAppointment(Models.Appointment appointment)
		{
			context.Appointments.Update(appointment);
			context.SaveChanges();
		}

		public void DeleteAppointment(Models.Appointment appointment)
		{
			context.Appointments.Remove(appointment);
			context.SaveChanges();
		}

		public Models.Appointment? GetAppointmentById(long id)
		{
			return context.Appointments.Include(a => a.Doctor).FirstOrDefault(a => a.AppointmentID == id);
		}

		public IQueryable<Models.Doctor> Doctors => context.Doctors.Include(d => d.Appointments);

		public void CreateDoctor(Models.Doctor doctor)
		{
			context.Doctors.Add(doctor);
			context.SaveChanges();
		}

		public void UpdateDoctor(Models.Doctor doctor)
		{
			context.Doctors.Update(doctor);
			context.SaveChanges();
		}

		public void DeleteDoctor(Models.Doctor doctor)
		{
			context.Doctors.Remove(doctor);
			context.SaveChanges();
		}

		public Models.Doctor? GetDoctorById(long id)
		{
			return context.Doctors.Include(d => d.Appointments).FirstOrDefault(d => d.DoctorID == id);
		}
	}
}
