using Application.Activities.Queries;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Persistence;

namespace API.Controllers
{
    public class ActivitiesController(IMediator mediator/*, AppDbContext context*/) : BaseApiController
    {
        /*** Ancienne manière de faire de l'injection de dépendance ***/
        /*
        private readonly AppDbContext context;
        public ActivitiesController(AppDbContext context)
        {           this.context = context;        }
        */

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            //return await context.Activities.ToListAsync();
            return await mediator.Send(new GetActivityList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityDetail(string id)
        {
            // ❌ await context.Activities.Where(a => a.Id == id).FirstAsync();
            // var activity = await context.Activities.FindAsync(id);
            // if (activity == null) return NotFound(); // return 404
            // return activity;
            return await mediator.Send(new GetActivityDetails.Query { Id = id });
        }
    }
}