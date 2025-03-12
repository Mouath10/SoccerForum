using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerForum.Data;
using SoccerForum.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerForum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly SoccerForumContext _context;

        public CommentsController(SoccerForumContext context)
        {
            _context = context;
        }

        // GET: Comments/Create
        public IActionResult Create(int discussionId)
        {
            // Ensure the discussion exists
            var discussion = _context.Discussions.Find(discussionId);
            if (discussion == null)
            {
                return NotFound(); // Return 404 if discussion not found
            }

            // Create a new comment linked to the provided DiscussionId
            var comment = new Comment
            {
                DiscussionId = discussionId
            };

            return View(comment);
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,DiscussionId")] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return View(comment); // Return with validation errors if invalid
            }

            // Ensure the associated discussion exists
            var discussion = await _context.Discussions.FindAsync(comment.DiscussionId);
            if (discussion == null)
            {
                return NotFound(); // Return 404 if discussion not found
            }

            comment.CreateDate = DateTime.UtcNow; // Set creation date in UTC

            // Add the comment to the database
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            // Redirect to the discussion details page
            return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
        }
    }
}