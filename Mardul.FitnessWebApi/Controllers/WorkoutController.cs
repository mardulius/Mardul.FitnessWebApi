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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var signUser = await _userManager.GetUserAsync(HttpContext.User);

            var users = db.Users
                .Include(x => x.Workouts)
                .ThenInclude(x => x.Exercises)
               .ToList();

            var us = users.FirstOrDefault(a => a == signUser);
            

            return Ok(us);
        }


        [Authorize]
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
