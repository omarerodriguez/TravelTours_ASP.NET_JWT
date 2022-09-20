using System.ComponentModel.DataAnnotations;

namespace TravelTours.Core.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool State { get; set; }
    }
}