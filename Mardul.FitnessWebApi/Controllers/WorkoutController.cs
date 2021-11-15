using Mardul.FitnessWebApi.Dto;
using Mardul.FitnessWebApi.Model;
using Mardul.FitnessWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mardul.FitnessWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly FitnessContext db;
        private readonly UserManager<User> userManager;

        public WorkoutController(FitnessContext context, UserManager<User> userManager)
        {
            db = context;
            this.userManager = userManager;

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {

            var userId = User.Identity.Name;

            var works = await db.Users
                 .Where(x => x.Id == userId)
                  .Select(x => new UserDto()
                  {

                      Workouts = x.Workouts.Select(w => new WorkoutDto()
                      {
                          Id = w.Id,
                          Name = w.Name,
                          Exercises = w.Exercises.Select(e => new ExerciseDto
                          {
                              Id = e.Id,
                              Name = e.Name,
                              MuscleGroupName = e.MuscleGroup.Name
                          }),
                      })
                  })
               .FirstOrDefaultAsync();

            if (works == null)
            {
                return BadRequest();
            }

            return Ok(works.Workouts);

        }



        [HttpPost]
        public async Task<ActionResult> Post(Workout workout)
        {
            var signUser = await userManager.GetUserAsync(HttpContext.User);
            var User = db.Users.FirstOrDefault(a => a == signUser);

            if (workout == null)
            {
                return BadRequest();
            }
            User.Workouts.Add(workout);
            db.Users.Update(User);
            await db.SaveChangesAsync();

            return Ok(User.Workouts);


        }
    }
}
