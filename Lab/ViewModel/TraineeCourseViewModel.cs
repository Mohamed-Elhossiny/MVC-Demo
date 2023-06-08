using Lab.Models;

namespace Lab.ViewModel
{
    public class TraineeCourseViewModel
    {
        public string T_Name { get; set; }
        public string C_Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public string Color { get; set; }
        public List<Trainee> traineeList { get; set; }
    }
}
