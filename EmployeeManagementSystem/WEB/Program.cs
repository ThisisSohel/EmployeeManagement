using DAO.DB;
using DAO.Repositories;
using Microsoft.EntityFrameworkCore;
using Service.Services;

namespace WEB
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			// Register the repository as scoped
			builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

			// Register the service as scoped
			builder.Services.AddScoped<IEmployeeService, EmployeeService>();
			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Employee}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
