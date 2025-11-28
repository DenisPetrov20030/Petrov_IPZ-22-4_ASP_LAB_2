using System.ComponentModel.DataAnnotations;

namespace HospitalSystem.Models.ViewModels
{
	public class LoginModel
	{
		[Required(ErrorMessage = "¬вед≥ть ≥м'€ користувача або email")]
		[Display(Name = "≤м'€ користувача або Email")]
		public string UserName { get; set; } = string.Empty;

		[Required(ErrorMessage = "¬вед≥ть пароль")]
		[DataType(DataType.Password)]
		[Display(Name = "ѕароль")]
		public string Password { get; set; } = string.Empty;

		public string? ReturnUrl { get; set; }
	}
}
