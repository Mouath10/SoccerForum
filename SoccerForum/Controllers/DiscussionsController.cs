using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerForum.Data;
using SoccerForum.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize] // Restrict access to authenticated users
public class DiscussionController : Controller
{
    private readonly SoccerForumContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DiscussionController(SoccerForumContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Create Discussion
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Discussion discussion)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                discussion.ApplicationUserId = user.Id;
            }

            _context.Add(discussion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(discussion);
    }

    // Edit Discussion - Only Owner Can Edit
    public async Task<IActionResult> Edit(int id)
    {
        var discussion = await _context.Discussions.FindAsync(id);
        if (discussion == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (discussion.ApplicationUserId != user?.Id) // Only allow owner to edit
        {
            return Forbid(); // Return 403 Forbidden
        }

        return View(discussion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Discussion discussion)
    {
        if (id != discussion.DiscussionId)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (discussion.ApplicationUserId != user?.Id)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(discussion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Discussions.Any(d => d.DiscussionId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(discussion);
    }

    // Delete Discussion - Only Owner Can Delete
    public async Task<IActionResult> Delete(int id)
    {
        var discussion = await _context.Discussions.FindAsync(id);
        if (discussion == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (discussion.ApplicationUserId != user?.Id)
        {
            return Forbid();
        }

        return View(discussion);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var discussion = await _context.Discussions.FindAsync(id);
        if (discussion == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (discussion.ApplicationUserId != user?.Id)
        {
            return Forbid();
        }

        _context.Discussions.Remove(discussion);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
