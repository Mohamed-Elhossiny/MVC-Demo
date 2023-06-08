using System.ComponentModel.DataAnnotations;
using Lab.Models;

namespace Lab.Validation
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            MvcContext db = new MvcContext();
            string name = value.ToString();
            Course coureReq = validationContext.ObjectInstance as Course;
            Course courseDB = db.Courses.FirstOrDefault(c => c.Name == name && c.Dept_Id == coureReq.Dept_Id);
            if (courseDB == null)
            {
                return ValidationResult.Success;
            }
            else if (coureReq.Id == courseDB.Id)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Course Name is Founded");

        }
    }
}
