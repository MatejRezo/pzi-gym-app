using Microsoft.EntityFrameworkCore;
using SportiverseApi.Models.Auth;
using SportiverseApi.Models.Exercise;
using SportiverseApi.Models.News;
using SportiverseApi.Models.User;

namespace SportiverseApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Plan> TrainingPlans { get; set; }
        public DbSet<PlanExercises> PlanExercises { get; set; }
    }
}
