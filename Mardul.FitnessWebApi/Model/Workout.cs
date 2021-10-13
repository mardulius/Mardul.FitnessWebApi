using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mardul.FitnessWebApi.Model
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Exercise> Exercises{ get; set; } = new List<Exercise>();

        public List<User> Users { get; set; } = new List<User>();
    }
}
