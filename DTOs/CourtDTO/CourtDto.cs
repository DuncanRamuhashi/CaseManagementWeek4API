using System.ComponentModel.DataAnnotations;

namespace CaseManagementWeek4API.DTOs.CourtDTO
{
    // Used to display court information
    public class CourtDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Location { get; set; }

        public string? CourtCode { get; set; }

        public string? CourtLevel { get; set; }

        public string? Province { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    // Used when creating a new court
    public class CreateCourtDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [StringLength(200)]
        public string? Location { get; set; }

        [StringLength(50)]
        public string? CourtCode { get; set; }

        [StringLength(100)]
        public string? CourtLevel { get; set; }

        [StringLength(100)]
        public string? Province { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;
    }

    // Used when updating an existing court
    public class UpdateCourtDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [StringLength(200)]
        public string? Location { get; set; }

        [StringLength(50)]
        public string? CourtCode { get; set; }

        [StringLength(100)]
        public string? CourtLevel { get; set; }

        [StringLength(100)]
        public string? Province { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; }
    }
}
