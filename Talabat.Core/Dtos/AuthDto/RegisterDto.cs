using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Dtos.AuthDto
{
	public class RegisterDto
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Display Name is required")]
		[StringLength(50, ErrorMessage = "Display Name cannot be longer than 50 characters")]
		public string DisplayName { get; set; }

		[Required(ErrorMessage = "Phone Number is required")]
		[Phone(ErrorMessage = "Invalid Phone Number")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
			ErrorMessage = "Password must be at least 6 characters long and contain at least 1 uppercase, 1 lowercase, 1 number, and 1 special character (@$!%*?&).")]
		public string Password { get; set; }

	}
}
