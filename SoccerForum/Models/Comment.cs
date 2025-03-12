using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        // Foreign Key to Discussion
        public int DiscussionId { get; set; }

        public Discussion? Discussion { get; set; }

        // Foreign Key to ApplicationUser
        public string? ApplicationUserId { get; set; } // Nullable FK

        // Navigation Property
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
