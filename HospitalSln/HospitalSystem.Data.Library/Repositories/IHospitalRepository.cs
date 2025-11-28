namespace HospitalSystem.Data.Repositories
{
	public interface IHospitalRepository
	{
		IQueryable<Models.Appointment> Appointments { get; }
		void CreateAppointment(Models.Appointment appointment);
		void UpdateAppointment(Models.Appointment appointment);
		void DeleteAppointment(Models.Appointment appointment);
		Models.Appointment? GetAppointmentById(long id);

		IQueryable<Models.Doctor> Doctors { get; }
		void CreateDoctor(Models.Doctor doctor);
		void UpdateDoctor(Models.Doctor doctor);
		void DeleteDoctor(Models.Doctor doctor);
		Models.Doctor? GetDoctorById(long id);
	}
}
