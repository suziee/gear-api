using System;

namespace Api.Gear.Models
{
    public class Sling
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public double LengthInCentimeters { get; set; }
        public double WidthInMillimeters { get; set; }
        public int StrengthInKilonewtons { get; set; }
        public double WeightInGrams { get; set; }
        public Guid Guid { get; set; }
        public int Id { get; set; }
    }
}