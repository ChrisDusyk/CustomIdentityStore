using System.ComponentModel.DataAnnotations;

namespace CustomIdentityStore.Models.AccountViewModels
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}