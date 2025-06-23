using Microsoft.EntityFrameworkCore;
using HouseholdManager.Data;
using HouseholdManager.Models;
using HouseholdManager.Services;

namespace HouseholdManager
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure to listen on all network interfaces
            builder.WebHost.UseUrls("http://0.0.0.0:5000", "https://0.0.0.0:5001");

            // Add services to the container
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Add Entity Framework
            builder.Services.AddDbContext<HouseholdContext>(options =>
                options.UseSqlite("Data Source=household.db"));

            // Add application services
            builder.Services.AddScoped<HouseholdService>();
            builder.Services.AddScoped<MealService>();
            builder.Services.AddScoped<MealPlanService>();
            builder.Services.AddScoped<IngredientService>();
            builder.Services.AddScoped<ChoreService>();
            builder.Services.AddScoped<MaintenanceService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapRazorPages();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            // Ensure database is created
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HouseholdContext>();
                await context.Database.EnsureCreatedAsync();
            }

            await app.RunAsync();
        }
    }
}
