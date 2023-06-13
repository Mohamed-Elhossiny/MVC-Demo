using Lab.Models;

namespace Lab.Repository
{
	public class AccountRepository : IAccountRepository
	{
		MvcContext db;
		public AccountRepository(MvcContext _db)
		{
			db = _db;
		}
		public void Create(Account account)
		{
			db.Accounts.Add(account);
		}

		public bool Find(string userName, string password)
		{
			Account account = db.Accounts.FirstOrDefault(a => a.Username == userName && a.Password == password);
			if (account == null)
			{
				return false;
			}
			else { return true; }

		}

		public Account Get(string userName, string password)
		{
			return db.Accounts.FirstOrDefault(a => a.Username == userName && a.Password == password);
		}

		public void Save()
		{
			db.SaveChanges();
		}
        public string GetRoles(int id)
        {
            if (id % 2 == 0)
            {
                return "Admin";
            }
            return "Student";
        }
    }
}
