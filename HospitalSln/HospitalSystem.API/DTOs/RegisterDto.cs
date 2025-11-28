using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.API.DTOs
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Введіть ім'я користувача")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введіть електронну адресу")]
		[EmailAddress(ErrorMessage = "Невірний формат електронної адреси")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введіть пароль")]
		[MinLength(8, ErrorMessage = "Пароль має містити не менше 8 символів")]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Підтвердіть пароль")]
		[Compare("Password", ErrorMessage = "Паролі не співпадають")]
		public string ConfirmPassword { get; set; } = string.Empty;

		[Required(ErrorMessage = "Вкажіть роль")]
		public string Role { get; set; } = "Patient";
	}
}
