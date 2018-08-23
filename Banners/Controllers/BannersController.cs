using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Banners.Models;
using Banners.Extensions;

namespace Banners.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly BannerContext _context;

        public BannersController(BannerContext context)
        {
            _context = context;
        }

        // GET: api/Banners
        [HttpGet]
        public IEnumerable<Banner> GetBanners()
        {
            return _context.Banners;
        }

        // GET: api/Banners/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBanner([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var banner = await _context.Banners.FindAsync(id);

            if (banner == null)
            {
                return NotFound();
            }

            return Ok(banner);
        }

        // PUT: api/Banners/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBanner([FromRoute] int id, [FromBody] Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != banner.Id)
            {
                return BadRequest();
            }

            //validate html
            if (!banner.HasValidHtml()) return BadRequest("Invalid HTML in banner");

            //assign modified date
            banner.Modified = DateTime.Now;

            _context.Entry(banner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(id))
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

        // POST: api/Banners
        [HttpPost]
        public async Task<IActionResult> PostBanner([FromBody] Banner banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //validate html
            if (!banner.HasValidHtml()) return BadRequest("Invalid HTML in banner");

            //set Created date
            banner.Created = DateTime.Now;

            _context.Banners.Add(banner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBanner", new { id = banner.Id }, banner);
        }

        // DELETE: api/Banners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanner([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();

            return Ok(banner);
        }

        private bool BannerExists(int id)
        {
            return _context.Banners.Any(e => e.Id == id);
        }
    }
}