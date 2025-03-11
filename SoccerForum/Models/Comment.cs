using System;
using System.ComponentModel.DataAnnotations;

namespace SoccerForum.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string? Content { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int DiscussionId { get; set; }
        public Discussion? Discussion { get; set; }
    }
}
