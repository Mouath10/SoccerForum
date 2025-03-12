using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SoccerForum.Models
{
    public class Discussion
    {
        [Key]
        public int DiscussionId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string Content { get; set; } = string.Empty;

        // Optional Image Filename
        public string? ImageFilename { get; set; }

        // Property for file upload, not mapped in EF
        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        // Default to UTC for consistency across servers
        public DateTime CreateDate { get; set; }

        // Navigation property for comments
        public ICollection<Comment> Comments { get; set; }

        // Constructor to initialize collections & default values
        public Discussion()
        {
            CreateDate = DateTime.UtcNow;
            Comments = new List<Comment>();
        }
    }
}
