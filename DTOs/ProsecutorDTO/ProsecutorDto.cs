using System.ComponentModel.DataAnnotations;

namespace CaseManagementWeek4API.DTOs.ProsecutorDTO
{
    // Used to display prosecutor information
    public class ProsecutorDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Rank { get; set; }
        public string? Office { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool? IsActive { get; set; }
    }

    // Used when registering a new prosecutor
    public class CreateProsecutorDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public string? Rank { get; set; }

        public string? Office { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool? IsActive { get; set; } = true;
    }

    // Used when updating an existing prosecutor
    public class UpdateProsecutorDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public string? Rank { get; set; }

        public string? Office { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool? IsActive { get; set; }
    }
}
