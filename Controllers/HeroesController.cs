using HeroTest.Models;
using HeroTest.Models.ActionFilters;
using HeroTest.Models.domains;
using HeroTest.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeroTest.Controllers;
[ApiController]
[Route("[controller]")]
public class HeroesController : ControllerBase
{

    //private static readonly string[] Summaries = new[]
    //{
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

    //private readonly ILogger<HeroesController>? _logger;

    private readonly IHeroService heroService;

    public HeroesController(IHeroService heroService)
    {  
        this.heroService = heroService;
    }

    [HttpGet]
    public IActionResult GetHeroes()
    {
        return Ok(this.heroService.GetHeroes());
    }

    [TypeFilter(typeof(HeroValidateHeroIdFilterAttribute))]
    [HttpGet("{id}")]
    public IActionResult GetHeroById(int id)
    {
        return Ok(HttpContext.Items["Hero"]);
    }


    [TypeFilter(typeof(HeroValidateCreateHeroFilterAttribute))]
    [HttpPost]
    public IActionResult CreateHero([FromBody] HeroDto hero)
    {
        var heroCreated = this.heroService.CreateHero(hero);

        return CreatedAtAction("GetHeroById", new { id = heroCreated.Id }, heroCreated);
    }

    [TypeFilter(typeof(HeroValidateHeroIdFilterAttribute))]
    [HttpPut("{id}")]
    public IActionResult UpdateHero(int id)
    {
        this.heroService.UpdateHero(id);
        return NoContent();
    }

    //public HeroesController(ILogger<HeroesController> logger)
    //{
    //    _logger = logger;
    //}

    //public IEnumerable<WeatherForecast> Get()
    //{
    //    return Enumerable.Range(1, 500).Select(index => new WeatherForecast
    //    {
    //        Date = DateTime.Now.AddDays(index),
    //        TemperatureC = Random.Shared.Next(-20, 55),
    //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //    })
    //    .ToArray();
    //}
}

