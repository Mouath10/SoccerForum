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

        // Optional Image Filename, no [Required] attribute
        public string ImageFilename { get; set; } = string.Empty;

        // Property for file upload, not mapped in EF
        [NotMapped]
        [Display(Name = "Photograph")]
        public IFormFile? ImageFile { get; set; } // nullable!!!


        // Ensure a default value for CreateDate
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public ICollection<Comment> Comments { get; set; }
    }
}
