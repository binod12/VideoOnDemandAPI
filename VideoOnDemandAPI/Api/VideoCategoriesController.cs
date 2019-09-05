using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoOnDemandAPI;
using VideoOnDemandAPI.Models;

namespace VideoOnDemandAPI.Api
{
    public class VideoCategoriesController : BaseApiController
    {
        private readonly VideoOnDemandDBContext _context;

        public VideoCategoriesController(VideoOnDemandDBContext context)
        {
            _context = context;
        }

        // GET: api/VideoCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoCategory>>> GetVideoCategory()
        {
            return await _context.VideoCategory.ToListAsync();
        }

        // GET: api/VideoCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoCategory>> GetVideoCategory(int id)
        {
            var videoCategory = await _context.VideoCategory.FindAsync(id);

            if (videoCategory == null)
            {
                return NotFound();
            }

            return videoCategory;
        }

        // PUT: api/VideoCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideoCategory(int id, VideoCategory videoCategory)
        {
            if (id != videoCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(videoCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoCategoryExists(id))
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

        // POST: api/VideoCategories
        [HttpPost]
        public async Task<ActionResult<VideoCategory>> PostVideoCategory(VideoCategory videoCategory)
        {
            _context.VideoCategory.Add(videoCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVideoCategory", new { id = videoCategory.Id }, videoCategory);
        }

        // DELETE: api/VideoCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VideoCategory>> DeleteVideoCategory(int id)
        {
            var videoCategory = await _context.VideoCategory.FindAsync(id);
            if (videoCategory == null)
            {
                return NotFound();
            }

            _context.VideoCategory.Remove(videoCategory);
            await _context.SaveChangesAsync();

            return videoCategory;
        }

        private bool VideoCategoryExists(int id)
        {
            return _context.VideoCategory.Any(e => e.Id == id);
        }
    }
}
