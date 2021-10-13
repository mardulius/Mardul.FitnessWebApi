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
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        FitnessContext db;
        
        public ExerciseController(FitnessContext context)
        {
            db = context;
            
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> Get()
        {
            
            return await db.Exercises.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> Get(int id)
        {
            Exercise exercise = await db.Exercises.FirstOrDefaultAsync(x => x.Id == id);
            if (exercise == null)
                return NotFound();
            return new ObjectResult(exercise);
        }
       
        [HttpPost]
        public async Task<ActionResult<Exercise>> Post(Exercise exercise)
        {
            if (exercise == null)
            {
                return BadRequest();
            }

            db.Exercises.Add(exercise);
            await db.SaveChangesAsync();
            return Ok(exercise);
        }
       
        [HttpPut]
        public async Task<ActionResult<Exercise>> Put(Exercise exercise)
        {
            if (exercise == null)
            {
                return BadRequest();
            }
            if (!db.Exercises.Any(x => x.Id == exercise.Id))
            {
                return NotFound();
            }

            db.Update(exercise);
            await db.SaveChangesAsync();
            return Ok(exercise);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Exercise>> Delete(int id)
        {
            Exercise exercise = db.Exercises.FirstOrDefault(x => x.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            db.Exercises.Remove(exercise);
            await db.SaveChangesAsync();
            return Ok(exercise);
        }

        
    }


}
