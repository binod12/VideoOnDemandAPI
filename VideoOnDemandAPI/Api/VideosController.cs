using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOnDemandAPI.Entities;

namespace VideoOnDemandAPI.Api
{

  [Authorize]
  public class VideosController : BaseApiController
  {
    private readonly VideoOnDemandDBContext _context;

    public VideosController(VideoOnDemandDBContext context)
    {
      _context = context;
    }

    // GET: api/Videos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Video>>> GetVideo()
    {
      return await _context.Video.ToListAsync();
    }

    // GET: api/Videos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Video>> GetVideo(int id)
    {
      var video = await _context.Video.FindAsync(id);

      if (video == null)
      {
        return NotFound();
      }

      return video;
    }

    // PUT: api/Videos/5
    [Authorize(Roles =Role.Admin)]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVideo(int id, Video video)
    {
      if (id != video.Id)
      {
        return BadRequest();
      }

      _context.Entry(video).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!VideoExists(id))
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

    // POST: api/Videos
    [Authorize(Roles =Role.Admin)]
    [HttpPost]
    public async Task<ActionResult<Video>> PostVideo(Video video)
    {
      _context.Video.Add(video);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetVideo", new { id = video.Id }, video);
    }

    // DELETE: api/Videos/5
    [Authorize(Roles =Role.Admin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Video>> DeleteVideo(int id)
    {
      var video = await _context.Video.FindAsync(id);
      if (video == null)
      {
        return NotFound();
      }

      _context.Video.Remove(video);
      await _context.SaveChangesAsync();

      return video;
    }

    private bool VideoExists(int id)
    {
      return _context.Video.Any(e => e.Id == id);
    }
  }
}
