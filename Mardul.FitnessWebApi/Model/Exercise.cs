using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mardul.FitnessWebApi.Model
{
    public class Exercise
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int MuscleGroupId { get; set; }


        public MuscleGroup MuscleGroup { get; set; }


        public List<Workout> Workouts { get; set; } = new List<Workout>();
    }
}
