using System;
using System.Collections.Generic;

#nullable disable

namespace ChickenSimulator.Models
{
    public partial class Farm
    {
        public Farm()
        {
            Chickens = new HashSet<Chicken>();
        }

        public int FarmId { get; set; }
        public string Name { get; set; }
        public int? Seeds { get; set; }

        public virtual ICollection<Chicken> Chickens { get; set; }
    }
}
