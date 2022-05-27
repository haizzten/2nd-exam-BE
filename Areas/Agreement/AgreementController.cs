using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyExam;
using MyExam.Models;

namespace MyExam.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Policy")]
    [ApiController]
    public class AgreementController : ControllerBase
    {
        private readonly MyDbContext context;

        public AgreementController(MyDbContext _context)
        {
            this.context = _context;
            SeedData();
        }
        private void SeedData()
        {
            if (context.Agreements.Any())
            {
                return;
            }
            var records = SeedHelper.SeedData<Agreement>("SeedingData.json");
            context.AddRange(records);
            context.SaveChanges();
        }
        // GET: api/Agreement
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Agreement>>> GetAgreements(int p = 1)
        {
            if (context.Agreements == null)
            {
                return NotFound();
            }
            return await context.Agreements.ToListAsync();
        }

        // GET: api/Agreement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agreement>> GetAgreement(string id)
        {
            if (context.Agreements == null)
            {
                return NotFound();
            }
            var agreement = await context.Agreements.FindAsync(id);

            if (agreement == null)
            {
                return NotFound();
            }

            return agreement;
        }

        // PUT: api/Agreement/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgreement(string id, Agreement agreement)
        {
            if (id != agreement.ID)
            {
                return BadRequest();
            }

            context.Entry(agreement).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgreementExists(id))
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

        // POST: api/Agreement
        [HttpPost]
        public async Task<ActionResult<Agreement>> PostAgreement(Agreement agreement)
        {
            if (context.Agreements == null)
            {
                return Problem("Entity set 'MyDbContext.Agreements'  is null.");
            }
            context.Agreements.Add(agreement);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AgreementExists(agreement.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAgreement", new { id = agreement.ID }, agreement);
        }

        // DELETE: api/Agreement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgreement(string id)
        {
            if (context.Agreements == null)
            {
                return NotFound();
            }
            var agreement = await context.Agreements.FindAsync(id);
            if (agreement == null)
            {
                return NotFound();
            }

            context.Agreements.Remove(agreement);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgreementExists(string id)
        {
            return (context.Agreements?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
