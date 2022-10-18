using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kanopi_page.Models;

namespace Kanopi_page.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KanopiPagesController : ControllerBase
    {
        private readonly Kanopi_appContext _context;

        public KanopiPagesController(Kanopi_appContext context)
        {
            _context = context;
        }

        // GET: api/KanopiPages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KanopiPage>>> GetKanopiPages()
        {
            return await _context.KanopiPages.ToListAsync();
        }

        // GET: api/KanopiPages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KanopiPage>> GetKanopiPage(int id)
        {
            var kanopiPage = await _context.KanopiPages.FindAsync(id);

            if (kanopiPage == null)
            {
                return NotFound();
            }

            return kanopiPage;
        }

        // PUT: api/KanopiPages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKanopiPage(int id, KanopiPage kanopiPage)
        {
            if (id != kanopiPage.Id)
            {
                return BadRequest();
            }

            _context.Entry(kanopiPage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KanopiPageExists(id))
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

        // POST: api/KanopiPages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KanopiPage>> PostKanopiPage(KanopiPage kanopiPage)
        {
            _context.KanopiPages.Add(kanopiPage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKanopiPage", new { id = kanopiPage.Id }, kanopiPage);
        }

        // DELETE: api/KanopiPages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKanopiPage(int id)
        {
            var kanopiPage = await _context.KanopiPages.FindAsync(id);
            if (kanopiPage == null)
            {
                return NotFound();
            }

            _context.KanopiPages.Remove(kanopiPage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KanopiPageExists(int id)
        {
            return _context.KanopiPages.Any(e => e.Id == id);
        }
    }
}
