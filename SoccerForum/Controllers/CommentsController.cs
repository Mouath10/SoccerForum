using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoccerForum.Data;
using SoccerForum.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize] // Restrict access to authenticated users
public class CommentController : Controller
{
    private readonly SoccerForumContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CommentController(SoccerForumContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Create Comment
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Comment comment)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                comment.ApplicationUserId = user.Id;
            }

            _context.Add(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Discussion", new { id = comment.DiscussionId });
        }
        return View(comment);
    }

    // Delete Comment - Only Owner Can Delete
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (comment.ApplicationUserId != user?.Id)
        {
            return Forbid();
        }

        return View(comment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (comment.ApplicationUserId != user?.Id)
        {
            return Forbid();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", "Discussion", new { id = comment.DiscussionId });
    }
}
