using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext: IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Measurement> Measurements { get; set; }
    public DbSet<DayPlanRecipe> DayPlanRecipes { get; set; }
    public DbSet<DayPlan> DayPlans { get; set; }
    public DbSet<WeekPlan> WeekPlans { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Recipe to User
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.AppUser)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ingredient to User
        modelBuilder.Entity<Ingredient>()
            .HasOne(i => i.AppUser)
            .WithMany(u => u.Ingredients)
            .HasForeignKey(i => i.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Recipe -> Measurements: Cascade delete
        modelBuilder.Entity<Measurement>()
            .HasOne(m => m.Recipe)
            .WithMany(r => r.Measurements)
            .HasForeignKey(m => m.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Ingredient -> Measurements: Changed from Restrict to Cascade delete
        modelBuilder.Entity<Measurement>()
            .HasOne(m => m.Ingredient)
            .WithMany()
            .HasForeignKey(m => m.IngredientId)
            .OnDelete(DeleteBehavior.Cascade); // Changed from Restrict to Cascade

        // DayPlanRecipe -> DayPlan (Many-to-One)
        modelBuilder.Entity<DayPlanRecipe>()
            .HasOne(dpr => dpr.DayPlan)
            .WithMany(dp => dp.DayPlanRecipes)
            .HasForeignKey(dpr => dpr.DayPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        // DayPlanRecipe -> Recipe (Many-to-One): Consider changing to Cascade
        modelBuilder.Entity<DayPlanRecipe>()
            .HasOne(dpr => dpr.Recipe)
            .WithMany()
            .HasForeignKey(dpr => dpr.RecipeId)
            .OnDelete(DeleteBehavior.Restrict); // Consider changing to Cascade if appropriate

        // WeekPlan -> DayPlans (One-to-Many) using WeekPlanId in DayPlan
        modelBuilder.Entity<WeekPlan>()
            .HasMany(wp => wp.DayPlans)
            .WithOne(dp => dp.WeekPlan)
            .HasForeignKey(dp => dp.WeekPlanId)
            .OnDelete(DeleteBehavior.Cascade);

        // DayPlan -> AppUser
        modelBuilder.Entity<DayPlan>()
            .HasOne(dp => dp.AppUser)
            .WithMany(u => u.DayPlans)
            .HasForeignKey(dp => dp.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // WeekPlan -> AppUser
        modelBuilder.Entity<WeekPlan>()
            .HasOne(wp => wp.AppUser)
            .WithMany(u => u.WeekPlans)
            .HasForeignKey(wp => wp.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}