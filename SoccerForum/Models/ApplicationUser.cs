using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SoccerForum.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ImageFilename { get; set; } = "default-profile.png"; // Default profile image

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // Not stored in the DB
    }
}
