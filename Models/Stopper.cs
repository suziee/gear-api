using System;

namespace Api.Gear.Models
{
    public class Stopper
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int StrengthInKilonewtons { get; set; }
        public double WidthInMillimeters { get; set; }
        public double? SmallerWidthInMillimeters { get; set; } // for offsets
        public double LengthInMillimeters { get; set; }
        public double WeightInGrams { get; set; }
        public Guid Guid { get; set; }
        public int Id { get; set; }
    }
}