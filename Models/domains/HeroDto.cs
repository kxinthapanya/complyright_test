using System.ComponentModel.DataAnnotations;

namespace HeroTest.Models.domains
{
    public class HeroDto 
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Alias { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int BrandId { get; set; }
        [Required]
        public string? BrandName { get; set; }
    }
}
