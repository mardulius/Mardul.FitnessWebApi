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
        private readonly UserManager<User> _userManager;

        public WorkoutController(FitnessContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {

            var userId = User.Identity.Name;

            var works = db.Users
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
               .FirstOrDefault();

            //var works = db.Users
            //     .Where(x => x.Id == userId)
            //      .Select(x => new UserDto()
            //      {

            //          Workouts = x.Workouts.Select(w => new WorkoutDto()
            //          {
            //              Id = w.Id,
            //              Name = w.Name,
            //              Exercises = w.Exercises.Select(e => new ExerciseDto
            //              {
            //                  Id = e.Id,
            //                  Name = e.Name,
            //                  MuscleGroupName = e.MuscleGroup.Name
            //              }),
            //          })
            //      }).ToList();


            if (works == null)
            {
                return BadRequest();
            }

            return Ok(works.Workouts);

        }



        [HttpPost]
        public async Task<ActionResult> Post(Workout workout)
        {
            var signUser = await _userManager.GetUserAsync(HttpContext.User);
            var user = db.Users.FirstOrDefault(a => a == signUser);

            if (workout == null)
            {
                return BadRequest();
            }
            user.Workouts.Add(workout);
            db.Users.Update(user);
            await db.SaveChangesAsync();

            return Ok(user.Workouts);


        }
    }
}
