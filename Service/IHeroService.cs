using HeroTest.Models.domains;

namespace HeroTest.Service
{
    public interface IHeroService
    {
        IList<HeroDto> GetHeroes();
        HeroDto CreateHero(HeroDto hero);
        void UpdateHero(int id);

    }
}