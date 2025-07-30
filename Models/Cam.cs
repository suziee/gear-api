using System;

namespace Api.Gear.Models
{
    public class Cam
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int StrengthInKilonewtons { get; set; }
        public double UsableMinInMillimeters { get; set; }
        public double UsableMaxInMillimeters { get; set; }
        public double UsableMinInInches { get; set; }
        public double UsableMaxInInches { get; set; }
        public double WeightInGrams { get; set; }
        public Guid Guid { get; set; }
        public int Id { get; set; }
    }
}