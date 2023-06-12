using Lab.Models;

namespace Lab.Repository
{
	public interface ITraineeRepository
	{
		List<Trainee> GetAll();
		Trainee GetById(int T_Id);
		void Add(Trainee trainee);
		void Update(int T_id, Trainee trainee);
		void Delete(int T_Id);
		void Save();
	}
}
