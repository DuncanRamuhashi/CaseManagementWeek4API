using System.ComponentModel.DataAnnotations;

namespace CaseManagementWeek4API.DTOs.JudgeDTO
{
    // Used to display judge information
    public class JudgeDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Title { get; set; }
        public int? CourtId { get; set; }
        public string? CourtName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateOnly? AppointmentDate { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CreateJudgeDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public string? Title { get; set; }

        [Required]
        public int CourtId { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateOnly? AppointmentDate { get; set; }

        public bool? IsActive { get; set; } = true;
    }

    public class UpdateJudgeDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public string? Title { get; set; }

        [Required]
        public int CourtId { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateOnly? AppointmentDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
