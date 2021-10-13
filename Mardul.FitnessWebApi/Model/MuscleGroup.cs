using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mardul.FitnessWebApi.Model
{
    public class MuscleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}
