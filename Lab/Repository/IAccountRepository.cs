using Lab.Models;

namespace Lab.Repository
{
	public interface IAccountRepository
	{
		void Create(Account account);
		bool Find(string userName, string password);
		Account Get(string userName, string password);
		string GetRoles(int id);

        void Save();
	}
}