﻿using Microsoft.EntityFrameworkCore;

namespace Lab.Models
{
	public class MvcContext : DbContext
	{
		public DbSet<Course> Courses { get; set; }
		public DbSet<CourseResult> CourseResults { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Trainee> Trainees { get; set; }
		public DbSet<Account> Accounts { get; set; }
        public MvcContext():base()
        {
            
        }
        public MvcContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=.;Database=Mvc;Trusted_Connection=True;TrustServerCertificate=True;");
			base.OnConfiguring(optionsBuilder);
		}
	}
}
