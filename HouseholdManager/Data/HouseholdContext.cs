using Microsoft.EntityFrameworkCore;
using HouseholdManager.Models;

namespace HouseholdManager.Data
{
    public class HouseholdContext : DbContext
    {
        public HouseholdContext(DbContextOptions<HouseholdContext> options) : base(options)
        {
        }

        public DbSet<Household> Households { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MealIngredient> MealIngredients { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<MealPlanEntry> MealPlanEntries { get; set; }
        public DbSet<Chore> Chores { get; set; }
        public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=household.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and constraints
            
            // Household relationships
            modelBuilder.Entity<FamilyMember>()
                .HasOne(fm => fm.Household)
                .WithMany(h => h.Members)
                .HasForeignKey(fm => fm.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Household)
                .WithMany(h => h.Meals)
                .HasForeignKey(m => m.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingredient>()
                .HasOne(i => i.Household)
                .WithMany(h => h.Ingredients)
                .HasForeignKey(i => i.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            // Meal-Ingredient many-to-many relationship
            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Meal)
                .WithMany(m => m.MealIngredients)
                .HasForeignKey(mi => mi.MealId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Ingredient)
                .WithMany(i => i.MealIngredients)
                .HasForeignKey(mi => mi.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MealPlan>()
                .HasOne(mp => mp.Household)
                .WithMany(h => h.MealPlans)
                .HasForeignKey(mp => mp.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chore>()
                .HasOne(c => c.Household)
                .WithMany(h => h.Chores)
                .HasForeignKey(c => c.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MaintenanceTask>()
                .HasOne(mt => mt.Household)
                .WithMany(h => h.MaintenanceTasks)
                .HasForeignKey(mt => mt.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Household)
                .WithMany(h => h.Notes)
                .HasForeignKey(n => n.HouseholdId)
                .OnDelete(DeleteBehavior.Cascade);

            // MealPlan relationships
            modelBuilder.Entity<MealPlanEntry>()
                .HasOne(mpe => mpe.MealPlan)
                .WithMany(mp => mp.Entries)
                .HasForeignKey(mpe => mpe.MealPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MealPlanEntry>()
                .HasOne(mpe => mpe.Meal)
                .WithMany()
                .HasForeignKey(mpe => mpe.MealId)
                .OnDelete(DeleteBehavior.SetNull);

            // Chore assignments
            modelBuilder.Entity<Chore>()
                .HasOne(c => c.AssignedTo)
                .WithMany(fm => fm.AssignedChores)
                .HasForeignKey(c => c.AssignedToId)
                .OnDelete(DeleteBehavior.SetNull);

            // Note creator relationship
            modelBuilder.Entity<Note>()
                .HasOne(n => n.CreatedBy)
                .WithMany()
                .HasForeignKey(n => n.CreatedById)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}