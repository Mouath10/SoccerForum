using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerForum.Data;
using SoccerForum.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly SoccerForumContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(SoccerForumContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var discussions = await _context.Discussions
                    .OrderByDescending(d => d.CreateDate)
                    .Select(d => new DiscussionViewModel
                    {
                        DiscussionId = d.DiscussionId,
                        Title = d.Title,
                        ImageFilename = d.ImageFilename,
                        CreateDate = d.CreateDate,
                        CommentCount = d.Comments.Count()
                    })
                    .ToListAsync();

                return View(discussions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching discussions.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        // GET: Home/GetDiscussion/5
        public async Task<IActionResult> GetDiscussion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var discussion = await _context.Discussions
                    .Include(d => d.Comments.OrderByDescending(c => c.CreateDate))
                    .FirstOrDefaultAsync(m => m.DiscussionId == id);

                if (discussion == null)
                {
                    return NotFound();
                }

                return View(discussion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching discussion with ID {id}.");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
