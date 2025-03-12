using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerForum.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string ImageFilename { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        // Foreign Key to ApplicationUser
        public string? ApplicationUserId { get; set; } // Nullable FK

        // Navigation Property
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
