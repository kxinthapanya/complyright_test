using HeroTest.Models.domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace HeroTest.Models.ActionFilters
{
    public class HeroValidateHeroIdFilterAttribute : ActionFilterAttribute
    {
        private readonly SampleContext db;

        public HeroValidateHeroIdFilterAttribute(SampleContext db)
        {
            this.db = db;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {  
            base.OnActionExecuting(context);

            var heroId = context.ActionArguments["id"] as int?;

            if(heroId.HasValue )
            {
                if (heroId <= 0)
                {
                    context.ModelState.AddModelError("HeroId", "HeroId is invalid.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                } 
                else
                {
                    var hero = this.db.Heroes
                               .Include("Brand")
                               .Select(x => new HeroDto()
                               {
                                   Id = x.Id,
                                   Name = x.Name,
                                   Alias = x.Alias,
                                   BrandId = x.Brand.Id,
                                   BrandName = x.Brand.Name,
                                   CreatedOn = x.CreatedOn,
                                   IsActive = x.IsActive,
                                   UpdatedOn = x.UpdatedOn
                               })
                               .Where(x => x.Id == heroId)
                               .FirstOrDefault();


                    if (hero == null )
                    {
                        context.ModelState.AddModelError("HeroId", "Hero not found.");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new BadRequestObjectResult(problemDetails);
                    }
                    else
                    {
                        var heroDto = new HeroDto()
                        {
                            Id = hero.Id,
                            Name = hero.Name,
                            Alias = hero.Alias,
                            IsActive = hero.IsActive,
                            CreatedOn = hero.CreatedOn,
                            UpdatedOn = hero.UpdatedOn,
                            BrandId = hero.BrandId,
                            BrandName = hero.BrandName
                        };

                        context.HttpContext.Items["Hero"] = heroDto;
                    }
                }
            }

        }
    }
}
