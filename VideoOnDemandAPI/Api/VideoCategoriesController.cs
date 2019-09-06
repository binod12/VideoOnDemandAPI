using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoOnDemandAPI.DataContext;

namespace VideoOnDemandAPI.Api
{
  /// <summary>
  /// VideoCategory API
  /// </summary>
  [Authorize]
  public class VideoCategoriesController : BaseApiController
  {
    private readonly VideoOnDemandDBContext _context;

    public VideoCategoriesController(VideoOnDemandDBContext context)
    {
      _context = context;
    }

    /// <summary>
    /// GET: api/VideoCategories - Get All Video Categories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoCategory>>> GetVideoCategory()
    {
      return await _context.VideoCategory.ToListAsync();
    }

    /// <summary>
    /// GET: api/VideoCategories/5 - Get Video Categories by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// PUT: api/VideoCategories/5 - Update Video Category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="videoCategory"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize(Roles=Role.Admin)]
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

    /// <summary>
    /// POST: api/VideoCategories - Create a new VideoCategory
    /// </summary>
    /// <param name="videoCategory"></param>
    /// <returns></returns>
    [Authorize(Roles = Role.Admin)]
    [HttpPost]
    public async Task<ActionResult<VideoCategory>> PostVideoCategory(VideoCategory videoCategory)
    {
      _context.VideoCategory.Add(videoCategory);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetVideoCategory", new { id = videoCategory.Id }, videoCategory);
    }

    /// <summary>
    /// DELETE: api/VideoCategories/5
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = Role.Admin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<VideoCategory>> DeleteVideoCategory(int id)
    {
      var videoCategory = await _context.VideoCategory.FindAsync(id);
      if (videoCategory == null)
      {
        return NotFound();
      }
      
      //If a video exists for the current category, throw error
      if (VideoExists(id))
      {
        return BadRequest(new { message = "This video category cannot be deleted, because a video of this category exists" });
      }
      _context.VideoCategory.Remove(videoCategory);
      await _context.SaveChangesAsync();

      return videoCategory;
    }

    #region Private helper methods
    private bool VideoCategoryExists(int id)
    {
      return _context.VideoCategory.Any(e => e.Id == id);
    }

    private bool VideoExists(int id)
    {
      return _context.Video.Any(e => e.VideoCategoryId == id);
    } 
    #endregion
  }
}
