using Lab.Models;

namespace Lab.ViewModel
{
    public class CourseDepartmentInstrucViewModel
    {
        public int C_Id { get; set; }
        public string C_Name { get; set; }
        public int C_MinDegree { get; set; }
        public string D_Name { get; set; }
        public List<Course> C_List { get; set; }

    }
}
