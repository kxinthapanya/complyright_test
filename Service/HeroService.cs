using HeroTest.Models;
using HeroTest.Models.domains;
using Microsoft.EntityFrameworkCore;
using System;

namespace HeroTest.Service
{
    public class HeroService : IHeroService
    {
        private readonly SampleContext db;

        public HeroService(SampleContext db)
        {
            this.db = db;
        }

        public HeroDto CreateHero(HeroDto hero)
        {
            var heroToCreate = new Hero()
            {
                Name = hero.Name,
                Alias = hero.Alias,            
                BrandId = hero.BrandId,   
                
            };

            this.db.Add(heroToCreate);
            this.db.SaveChanges();
         
            hero.Id = heroToCreate.Id;
  
            return hero;
        }

        public IList<HeroDto> GetHeroes()
        {
            var heroes = this.db.Heroes
               //.AsNoTracking()
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
               .ToList();

            return heroes;
        }

        public void UpdateHero(int id)
        {
            var heroToUpdate = this.db.Heroes.Find(id);
            heroToUpdate.IsActive = false;
            this.db.Update(heroToUpdate);
            this.db.SaveChanges();
        }
    }
}
