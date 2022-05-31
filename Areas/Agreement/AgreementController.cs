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

namespace MyExam.Agreement
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
        public class toReturn
        {
            public string ac { get; set; }
        }

        private void SeedData()
        {
            if (context.Agreements.Any())
            {
                return;
            }
            var records = SeedHelper.SeedData<AgreementModel>("SeedingData.json");
            context.AddRange(records);
            context.SaveChanges();
        }

        [HttpPost("TestPost")]
        public async Task<ActionResult> TestPost([FromBody] List<FilterModel> filterModelList)
        {

            return Ok();
        }



        // GET: api/Agreement
        [HttpGet()]
        public async Task<ActionResult> GetAgreements(int? start, int? end)
        {
            var _start = start ?? 0;
            var _end = end ?? 50;

            var blockSize = _end - _start;
            var queryResultSize = 0;
            var lastIndex = 0;

            if (context.Agreements == null)
            {
                return NotFound();
            }
            var query = context.Agreements as IQueryable<AgreementModel>;
            query = query.OrderBy(a => a.Status);
            query = query.Skip(_start);
            query = query.Take(_end - _start + 1);

            var queryResult = await query.ToListAsync();

            queryResultSize = query.Count();

            if (queryResultSize <= blockSize)
            {
                lastIndex = queryResultSize + _start;
            }
            else
            {
                queryResult.RemoveAt(queryResultSize - 1);
            }

            var agreementDTO = new AgreementDTO()
            {
                agreementList = queryResult,
                lastIndex = lastIndex
            };
            var result = new ObjectResult(agreementDTO);

            return result;

        }

        // GET: api/Agreement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgreementModel>> GetAgreement(string id)
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
        public async Task<IActionResult> PutAgreement(string id, AgreementModel agreement)
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
        public async Task<ActionResult<AgreementModel>> PostAgreement(AgreementModel agreement)
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

        [HttpPost("SortFilter")]
        public async Task<ActionResult> SortFilter([FromBody] List<FilterModel> filterModelList, [FromQuery] int? start, [FromQuery] int? end)
        {
            start ??= 0;
            end ??= 50;

            var blockSize = end - start;
            var queryResultSize = 0;
            var lastIndex = 0;

            if (context.Agreements == null)
            {
                return NotFound();
            }
            var query = (IQueryable<AgreementModel>)context.Agreements;
            // IQueryable<AgreementModel> query = (IQueryable<AgreementModel>)context.Agreements;
            filterModelList.ForEach(filterModel =>
            {
                switch (filterModel.columnName)
                {
                    case "id":
                        query = query.Where(f => f.ID.Contains(filterModel.filterValue));
                        break;

                    case "status":
                        query = query.Where(f => f.Status.Contains(filterModel.filterValue));
                        break;

                    case "quoteNumber":
                        query = query.Where(f => f.QuoteNumber.Contains(filterModel.filterValue));
                        break;

                    case "agreementName":
                        query = query.Where(f => f.AgreementName.Contains(filterModel.filterValue));
                        break;

                    case "agreementType":
                        query = query.Where(f => f.AgreementType.Contains(filterModel.filterValue));
                        break;

                    case "distributorName":
                        query = query.Where(f => f.DistributorName.Contains(filterModel.filterValue));
                        break;

                    case "effectiveDate":

                        query = query.Where(f => f.EffectiveDate.Date.Equals(DateTime.Parse(filterModel.filterValue).Date));
                        break;

                    case "expirationDate":
                        query = query.Where(f => f.ExpirationDate.Date.Equals(DateTime.Parse(filterModel.filterValue).Date));
                        break;

                    case "createdDate":
                        query = query.Where(f => f.CreatedDate.Date.Equals(DateTime.Parse(filterModel.filterValue).Date));
                        break;

                    case "daysUntilExpiration":
                        query = query.Where(f => f.DaysUntilExpiration.Equals(Int32.Parse(filterModel.filterValue)));
                        break;

                }
            });
            query = query.OrderBy(a => a.EffectiveDate);
            query = query.Skip(start.Value);
            query = query.Take(end.Value - start.Value + 1);


            var queryResult = await query.ToListAsync();


            var testTakeDate = context.Agreements.First();

            if (testTakeDate.EffectiveDate.Date.Equals(queryResult[0].EffectiveDate.Date)) { System.Console.WriteLine("true"); }

            queryResultSize = query.Count();

            if (queryResultSize <= blockSize)
            {
                lastIndex = queryResultSize + start.Value;
            }
            else
            {
                queryResult.RemoveAt(queryResultSize - 1);
            }

            var agreementDTO = new AgreementDTO()
            {
                agreementList = queryResult,
                lastIndex = lastIndex
            };
            var result = new ObjectResult(agreementDTO);

            return result;
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

        private void SwitchCase(string filterType)
        {
            switch (filterType)
            {
                case "date":
                    //Contains
                    break;
                case "text":
                    //Equal
                    break;
            }

        }
    }

}
