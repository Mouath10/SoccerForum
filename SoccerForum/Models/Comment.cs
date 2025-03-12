using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerForum.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500, ErrorMessage = "Comment content cannot exceed 500 characters.")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; }

        [ForeignKey("Discussion")]
        public int DiscussionId { get; set; }
        public Discussion? Discussion { get; set; }

        // Constructor to initialize CreateDate
        public Comment()
        {
            CreateDate = DateTime.UtcNow; // Use UTC for consistency in timestamps
        }
    }
}
