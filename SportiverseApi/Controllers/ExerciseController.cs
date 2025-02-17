using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportiverseApi.Models.Exercise;

namespace SportiverseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ExerciseController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Exercise>>> GetExercises()
        {
            return Ok(await _dataContext.Exercises.ToListAsync());
        }

        [HttpPost, Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<Exercise>>> AddExercise(Exercise Exercise)
        {
            Exercise _exercise = new Exercise();
            _exercise.Name = Exercise.Name;
            _exercise.Description = Exercise.Description;

            _dataContext.Add(_exercise);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Exercises.ToListAsync());
        }

        [HttpPut, Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<Exercise>>> UpdateExercise(Exercise updatedExercise)
        {
            var Exercise = await _dataContext.Exercises.FindAsync(updatedExercise.Id);

            if (Exercise == null) return BadRequest("Exercise item not found");

            Exercise.Name = updatedExercise.Name;
            Exercise.Description = updatedExercise.Description;
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Exercises.ToListAsync());
        }

        [HttpDelete("{ExerciseId}"), Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<Exercise>>> DeleteExercise(int ExerciseId)
        {
            var Exercise = await _dataContext.Exercises.FindAsync(ExerciseId);

            if (Exercise == null) return BadRequest("Exercise not item found");

            _dataContext.Exercises.Remove(Exercise);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.Exercises.ToListAsync());
        }
    }
}
