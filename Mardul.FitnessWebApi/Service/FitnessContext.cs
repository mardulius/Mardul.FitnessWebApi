using Mardul.FitnessWebApi.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mardul.FitnessWebApi.Service
{
    //public class FitnessContext : DbContext    
    public class FitnessContext : IdentityDbContext

    {

        public FitnessContext(DbContextOptions<FitnessContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
    }
}
