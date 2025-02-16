using Microsoft.EntityFrameworkCore;
using SoccerForum.Models;

namespace SoccerForum.Data
{
    public class SoccerForumContext : DbContext
    {
        public SoccerForumContext(DbContextOptions<SoccerForumContext> options)
            : base(options)
        {
        }

        // DbSet for Discussion model
        public DbSet<SoccerForum.Models.Discussion> Discussions { get; set; } = default!;

        // DbSet for Comment model
        public DbSet<SoccerForum.Models.Comment> Comments { get; set; } = default!;
    }
}
