using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COMP2001_Final.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgesController : ControllerBase
    {
        public COMP2001_EAldosariContext _get;

        public BadgesController(COMP2001_EAldosariContext get)
        {
            _get = get;
        }


        [HttpGet]
        [Route("get")]
        public JsonResult GetAllBadges()
        {
            return new JsonResult(_get.Badges.ToArray());
        }

        [HttpGet("get/{id}")]
        public ActionResult GetBadge(int id)
        {
            var badge = _get.Badges.Find(id);
            if (badge == null)
            {
                return NotFound();
            }
            return Ok(badge);
        }
            [HttpPost("create")]
        public async Task<ActionResult<Badge>> PostBadge(Badge badge)
        {
            _get.Badges.Add(badge);
            await _get.SaveChangesAsync();

            return CreatedAtAction(
                "GetBadge",
                new { id = badge.BadgeId },
                badge);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> PutBadge(int id, [FromBody] Badge badge)
        {
            if (id != badge.BadgeId)
            {
                return BadRequest();
            }
            _get.Entry(badge).State = EntityState.Modified;
            try
            {
                await _get.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_get.Badges.Any(p => p.BadgeId == id))
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

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Badge>> DeletBadge(int id)
        {
            var badge = await _get.Badges.FindAsync(id);
            if (badge == null)
            {
                return NotFound();
            }
            _get.Badges.Remove(badge);
            await _get.SaveChangesAsync();
            return badge;
        }
    }
}
