using System.ComponentModel.DataAnnotations;

namespace Lab.Models
{
	public class Account
	{
        public int Id { get; set; }
        public string Username { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
