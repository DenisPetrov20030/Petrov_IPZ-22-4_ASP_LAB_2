using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models
{
	public class Appointment
	{
		public long? AppointmentID { get; set; }

		[Required(ErrorMessage = "Будь ласка, введіть ім'я пацієнта")]
		[StringLength(100, ErrorMessage = "Ім'я пацієнта не може перевищувати 100 символів")]
		public string PatientName { get; set; } = String.Empty;

		[Required(ErrorMessage = "Будь ласка, введіть ім'я лікаря")]
		[StringLength(100, ErrorMessage = "Ім'я лікаря не може перевищувати 100 символів")]
		public string DoctorName { get; set; } = String.Empty;

		[Required(ErrorMessage = "Будь ласка, вкажіть дату і час прийому")]
		[DataType(DataType.DateTime)]
		public DateTime AppointmentDate { get; set; }

		[Required(ErrorMessage = "Будь ласка, вкажіть відділення")]
		[StringLength(50, ErrorMessage = "Назва відділення не може перевищувати 50 символів")]
		public string Department { get; set; } = String.Empty;

		[StringLength(500, ErrorMessage = "Примітки не можуть перевищувати 500 символів")]
		public string Notes { get; set; } = String.Empty;

		public long? DoctorID { get; set; }

		public Doctor? Doctor { get; set; }
	}
}
