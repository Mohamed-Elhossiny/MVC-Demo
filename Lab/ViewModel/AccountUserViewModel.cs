using System.ComponentModel.DataAnnotations;

namespace Lab.ViewModel
{
	public class AccountUserViewModel
	{
		public int Id { get; set; }
		public string Username { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }
		
		[DataType(DataType.Password),Compare("Password")]
		public string ConfirmPassword { get; set; }
	}
}
