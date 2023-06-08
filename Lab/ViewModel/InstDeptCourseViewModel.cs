using Lab.Models;

namespace Lab.ViewModel
{
	public class InstDeptCourseViewModel
	{
        public int InsID { get; set; }
        public string InsName { get; set; }
        public decimal InsSalary { get; set; }
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public string CrsName { get; set; }
        public string ImageSrc { get; set; }
        public int CrsID { get; set; }
        public List<Department> DeptList { get; set; }

        public List<Course> CrsList { get; set; }
        public List<Instructor> InstList { get; set; }

    }
}
