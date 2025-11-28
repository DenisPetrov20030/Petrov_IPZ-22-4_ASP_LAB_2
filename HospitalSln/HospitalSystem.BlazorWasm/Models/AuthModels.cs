using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.BlazorWasm.Models
{
	public class LoginDto
	{
		[Required(ErrorMessage = "Будь ласка, введіть email або ім'я користувача")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Будь ласка, введіть пароль")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;
	}

	public class RegisterDto
	{
		[Required(ErrorMessage = "Будь ласка, введіть ім'я користувача")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Будь ласка, введіть email")]
		[EmailAddress(ErrorMessage = "Невірний формат email")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Будь ласка, введіть пароль")]
		[StringLength(100, MinimumLength = 8, ErrorMessage = "Пароль повинен містити мінімум 8 символів")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Будь ласка, підтвердіть пароль")]
		[Compare("Password", ErrorMessage = "Паролі не співпадають")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; } = string.Empty;

		public string Role { get; set; } = "Patient";
	}

	public class AuthResponseDto
	{
		public string Token { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
	}
}
