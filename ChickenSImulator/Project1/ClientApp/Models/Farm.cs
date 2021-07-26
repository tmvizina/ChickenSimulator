using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChickenSimulator.ClientApp.Models
{
    public class Farm
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public int Seeds { get; set; }
  }
}
