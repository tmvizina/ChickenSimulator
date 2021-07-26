using System;
using System.Collections.Generic;

#nullable disable

namespace ChickenSimulator.Models
{
    public partial class Chicken
    {
        public int ChickenId { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public double? Smarts { get; set; }
        public double? Strength { get; set; }
        public double? Speed { get; set; }
        public double? Luck { get; set; }
        public string Color { get; set; }
        public int? FarmId { get; set; }

        public virtual Farm Farm { get; set; }
    }
}
