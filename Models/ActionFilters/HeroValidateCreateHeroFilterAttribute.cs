using HeroTest.Models.domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HeroTest.Models.ActionFilters
{
    public class HeroValidateCreateHeroFilterAttribute : ActionFilterAttribute
    {
        private readonly SampleContext db;

        public HeroValidateCreateHeroFilterAttribute(SampleContext db)
        {
            this.db = db;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var hero = context.ActionArguments["Hero"] as HeroDto;

            if(hero == null)
            {
                context.ModelState.AddModelError("Hero", "Hero object is null.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                var existingHero = this.db.Heroes.FirstOrDefault(x => x.Name == hero.Name && x.Alias == hero.Alias);

                if (existingHero != null) 
                {
                    context.ModelState.AddModelError("Hero", "Hero already exists.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }
}
