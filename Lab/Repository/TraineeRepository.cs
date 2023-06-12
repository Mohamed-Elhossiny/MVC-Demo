using Lab.Models;

namespace Lab.Repository
{
	public class TraineeRepository : ITraineeRepository
	{
		MvcContext db;
		public TraineeRepository(MvcContext _db)
		{
			this.db = _db;
		}
		public void Add(Trainee trainee)
		{
			db.Trainees.Add(trainee);
		}

		public void Delete(int T_Id)
		{
			Trainee trainee = db.Trainees.FirstOrDefault(t=>t.Id==T_Id);
			db.Trainees.Remove(trainee);
		}

		public List<Trainee> GetAll()
		{
			return db.Trainees.ToList();
		}

		public Trainee GetById(int T_Id)
		{
			return db.Trainees.FirstOrDefault(t=>t.Id==T_Id);
		}

		public void Save()
		{
			db.SaveChanges();
		}

		public void Update(int T_id, Trainee trainee)
		{
			throw new NotImplementedException();
		}
	}
}
