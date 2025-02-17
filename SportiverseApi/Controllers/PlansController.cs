using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportiverseApi.Models.Exercise;

namespace SportiverseApi.Controllers
{
    [Route("api/plans")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PlansController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Plan>>> getPlans()
        {
            return Ok(await _dataContext.TrainingPlans.Include(p => p.Exercises).ThenInclude(e => e.Exercise).ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Plan>>> AddPlan(Plan plan)
        {
            Plan _plan = new Plan();
            _plan.Name = plan.Name;
            _plan.Description = plan.Description;
            _plan.Exercises = new List<PlanExercises>();

            _dataContext.Add(_plan);
            await _dataContext.SaveChangesAsync();

            plan.Exercises.ForEach(p =>
            {
                var _exercise = _dataContext.Exercises.Find(p.ID);
                PlanExercises planExercises = new PlanExercises();

                if(_exercise != null)
                {
                    planExercises.Exercise = _exercise;
                }

                var _plan_temp = _dataContext.TrainingPlans.Find(_plan.ID);
                if (_plan_temp != null)
                {
                    planExercises.PlanID = _plan_temp.ID;
                }

                _dataContext.Add(planExercises);
                _dataContext.SaveChanges();
            });

            return Ok(await _dataContext.TrainingPlans.Include(p => p.Exercises).ThenInclude(e => e.Exercise).ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Plan>>> EditPlan(Plan plan)
        {
            if (plan == null) return BadRequest("Invalid data");

            var plans = await _dataContext.TrainingPlans.Include(p => p.Exercises).ToListAsync();
            if (plans == null) return BadRequest("No pnans stored in database");

            Plan _plan = plans.Find(p => p.ID == plan.ID);
            if (_plan == null) return BadRequest("Plan with ID doesnt exist");

            _plan.Exercises = plan.Exercises;
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.TrainingPlans.Include(p => p.Exercises).ThenInclude(e => e.Exercise).ToListAsync());
        }

        [HttpDelete("{planId}")]
        public async Task<ActionResult<List<Plan>>> DeletePlan(int planId)
        {
            var plans = await _dataContext.TrainingPlans.Include(p => p.Exercises).ToListAsync();
            var plan = plans.Find(p => p.ID == planId);
            if (plan == null) return BadRequest("Plan with ID doesnt exist");

            plan.Exercises.ForEach(e =>
            {
                var planExercises = _dataContext.PlanExercises.Find(e.ID);
                if (planExercises != null) _dataContext.Remove(planExercises);
            });

            _dataContext.Remove(plan);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.TrainingPlans.Include(p => p.Exercises).ThenInclude(e => e.Exercise).ToListAsync());
        }

        [HttpPatch("link-exercise")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<List<Plan>>> LinkExercisesToPlan(int planId, int exerciseId)
        {
            var plans = await _dataContext.TrainingPlans.Include(p => p.Exercises).ToListAsync();
            var plan = plans.Find(p => p.ID == planId);
            var exercise = await _dataContext.Exercises.FindAsync(exerciseId);

            if (plan == null) return BadRequest("Plan not found");
            if (exercise == null) return BadRequest("Exercise not found");

            var planExercises = new PlanExercises();
            planExercises.PlanID = plan.ID;
            planExercises.Exercise = exercise;

            _dataContext.Add(planExercises);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.TrainingPlans.Include(p => p.Exercises).ThenInclude(e => e.Exercise).ToListAsync());
        }


        [HttpPatch("unlink-all-exercises")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult<string>> UnlinkAllExercises(int planId)
        {
            var plans = await _dataContext.TrainingPlans.Include(p => p.Exercises).ToListAsync();
            var plan = plans.Find(p => p.ID == planId);

            if (plan == null) return BadRequest("Plan not found");

            plan.Exercises.ForEach(e =>
            {
                var planExercises = _dataContext.PlanExercises.Find(e.ID);
                if (planExercises != null) _dataContext.Remove(planExercises);
            });
            await _dataContext.SaveChangesAsync();
            
            return Ok(await _dataContext.TrainingPlans.Include(p => p.Exercises).ToListAsync());
        }
    }
}
