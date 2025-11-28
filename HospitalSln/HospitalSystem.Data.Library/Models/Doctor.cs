using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Data.Models
{
	public class Doctor
	{
		public long DoctorID { get; set; }

		[Required(ErrorMessage = "Будь ласка, введіть повне ім'я лікаря")]
		[StringLength(100, ErrorMessage = "Ім'я не може перевищувати 100 символів")]
		public string FullName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Будь ласка, вкажіть спеціалізацію")]
		[StringLength(50, ErrorMessage = "Спеціалізація не може перевищувати 50 символів")]
		public string Specialization { get; set; } = string.Empty;

		[Required(ErrorMessage = "Будь ласка, вкажіть відділення")]
		[StringLength(50, ErrorMessage = "Назва відділення не може перевищувати 50 символів")]
		public string Department { get; set; } = string.Empty;

		[Required(ErrorMessage = "Будь ласка, введіть номер телефону")]
		[Phone(ErrorMessage = "Невірний формат номера телефону")]
		[StringLength(20, ErrorMessage = "Номер телефону не може перевищувати 20 символів")]
		public string PhoneNumber { get; set; } = string.Empty;

		[Required(ErrorMessage = "Будь ласка, введіть електронну адресу")]
		[EmailAddress(ErrorMessage = "Невірний формат електронної адреси")]
		[StringLength(100, ErrorMessage = "Електронна адреса не може перевищувати 100 символів")]
		public string Email { get; set; } = string.Empty;

		public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
	}
}
