using System.Text.Json.Serialization;
using Lab.Models;
using Lab.Repository;
using Microsoft.EntityFrameworkCore;

namespace Lab
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			//builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
			
			builder.Services.AddDbContext<MvcContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("Mvc")));

			builder.Services.AddScoped<ICourseRepository, CourseRepository>();
			builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			builder.Services.AddScoped<IInstructorRepository,InstructorRepository>();
			builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
			builder.Services.AddScoped<ICourseResultRepository, CourseResultRepository>();

			builder.Services.AddSession(option =>
			{
				option.IdleTimeout = TimeSpan.FromMinutes(10);
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseSession();

			//app.MapControllerRoute(
			//	name:"Route1",
			//	pattern: "r1/{id?:int}",
			//	defaults: new {controller="Trainee",action= "ShowCourseDetails" });

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}