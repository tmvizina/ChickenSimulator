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
        [HttpPost]
        public async Task<ActionResult<Chicken>> PostChicken(Chicken chicken)
        {
            _context.Chickens.Add(chicken);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChicken", new { id = chicken.ChickenId }, chicken);
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
