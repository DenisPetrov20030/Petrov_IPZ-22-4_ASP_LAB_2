using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models.ViewModels
{
	public class ProfileModel
	{
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введіть електронну адресу")]
		[EmailAddress(ErrorMessage = "Некоректний формат адреси")]
		[Display(Name = "Email")]
		public string Email { get; set; } = string.Empty;

		[DataType(DataType.Password)]
		[Display(Name = "Новий пароль (залиште порожнім, якщо не змінюєте)")]
		[MinLength(8, ErrorMessage = "Пароль має містити мінімум 8 символів")]
		public string? NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Підтвердження нового пароля")]
		[Compare("NewPassword", ErrorMessage = "Паролі не співпадають")]
		public string? ConfirmNewPassword { get; set; }
	}
}
