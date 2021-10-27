using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mardul.FitnessWebApi.Model
{
    public class User : IdentityUser
    {
        public List<Workout> Workouts { get; set; } = new List<Workout>();


    }
}
