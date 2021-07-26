using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChickenSimulator.Models;

namespace ChickenSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChickensController : ControllerBase
    {
        ChickenSimulatorContext db = new ChickenSimulatorContext();

        private readonly ChickenSimulatorContext _context;

        public ChickensController(ChickenSimulatorContext context)
        {
            _context = context;
        }

        // GET: api/Chickens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chicken>>> GetChickens()
        {
            return await _context.Chickens.ToListAsync();
        }

        // GET: api/Chickens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chicken>> GetChicken(int id)
        {
            var chicken = await _context.Chickens.FindAsync(id);

            if (chicken == null)
            {
                return NotFound();
            }

            return chicken;
        }

       [HttpGet("f={farmId}")]
       public List<Chicken> GetChickensByFarm(int farmId)
        {
            List<Chicken> chickens = db.Chickens.Where(x => x.FarmId == farmId).ToList();
            return chickens;
        }

        // PUT: api/Chickens/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChicken(int id, Chicken chicken)
        {
            if (id != chicken.ChickenId)
            {
                return BadRequest();
            }

            _context.Entry(chicken).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChickenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Chickens
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("a={name}/{farmId}")]
        public void AddChicken(string name, int farmId)
        {
            Chicken chicken = new Chicken();
            chicken.Name = name;
            Farm farm = db.Farms.Where(x => x.FarmId == farmId).ToList().First();
            chicken.FarmId = farm.FarmId;
            List<string> Colors = new List<string>();
            Colors.Add("Black");
            Colors.Add("Red");
            Colors.Add("Brown");
            Colors.Add("Grey");
            Colors.Add("White");
            Random random = new Random();
            int ranColor = random.Next(0, 4);
            chicken.Color = Colors[ranColor];

            int ranSmarts = random.Next(1, 10);
            chicken.Smarts = ranSmarts;

            int ranStrength = random.Next(1, 10);
            chicken.Strength = ranStrength;
            int ranSpeed = random.Next(1, 10);
            chicken.Speed = ranSpeed;
            int ranLuck = random.Next(1, 10);
            chicken.Luck = ranLuck;

            chicken.Age = 1;

            db.Chickens.Add(chicken);
            db.SaveChanges();
        }

        [HttpPost("{farmName}/s={seeds}")]
        public void AddFarm(string FarmName, int seeds)
        {
            Farm farm = new Farm();
            farm.Name = FarmName;
            farm.Seeds = seeds;

            db.Farms.Add(farm);
            db.SaveChanges();
        }

        [HttpGet("Farm")]
        public List<Farm> GetFarms()
        {
            List<Farm> fList = new List<Farm>();
            foreach(Farm f in db.Farms)
            {
                fList.Add(f);
            }
            return fList;
        }

        // DELETE: api/Chickens/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Chicken>> DeleteChicken(int id)
        {
            var chicken = await _context.Chickens.FindAsync(id);
            if (chicken == null)
            {
                return NotFound();
            }

            _context.Chickens.Remove(chicken);
            await _context.SaveChangesAsync();

            return chicken;
        }

        private bool ChickenExists(int id)
        {
            return _context.Chickens.Any(e => e.ChickenId == id);
        }
    }
}
