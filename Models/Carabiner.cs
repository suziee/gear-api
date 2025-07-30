using System;

namespace Api.Gear.Models
{
    public class Carabiner
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public double? LengthInMillimeters { get; set; }
        public double? WidthInMillimeters { get; set; }
        public int ClosedMajorAxisKilonewtons { get; set; }
        public int ClosedMinorAxisKilonewtons { get; set; }
        public int OpenedMajorAxisKilonewtons { get; set; }
        public double WeightInGrams { get; set; }
        public double? GateOpenClearanceInMillimeters { get; set; }
        public Guid Guid { get; set; }
        public int Id { get; set; }
    }
}