using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.API.DTOs
{
	public class LoginDto
	{
		[Required(ErrorMessage = "¬вед≥ть ≥м'€ користувача або email")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "¬вед≥ть пароль")]
		public string Password { get; set; } = string.Empty;
	}
}
