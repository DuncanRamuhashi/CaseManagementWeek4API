using System.ComponentModel.DataAnnotations;

namespace CaseManagementWeek4API.DTOs.CourtCaseDTO
{
    // Used to display court case information
    public class CourtCaseDto
    {
        public int Id { get; set; }

        public int OriginalCaseId { get; set; }

        public string? CaseNumber { get; set; }

        public string? Status { get; set; }

        public int CourtId { get; set; }

        public int? JudgeId { get; set; }

        public int? ProsecutorId { get; set; }

        public DateTime? FilingDate { get; set; }

        public DateTime? NextHearingDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public string? Outcome { get; set; }

        public string? Notes { get; set; }

        public bool IsConfidential { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

    // Used when creating a court case
    public class CreateCourtCaseDto
    {
        [Required]
        public int OriginalCaseId { get; set; }

        [Required]
        public int CourtId { get; set; }

        public int? JudgeId { get; set; }

        public int? ProsecutorId { get; set; }

        [StringLength(100)]
        public string? CaseNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Open";

        public DateTime? FilingDate { get; set; }

        public DateTime? NextHearingDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public string? Outcome { get; set; }

        public string? Notes { get; set; }

        public bool IsConfidential { get; set; }
    }

    // Used when updating an existing court case
    public class UpdateCourtCaseDto
    {
        [Required]
        public int CourtId { get; set; }

        public int? JudgeId { get; set; }

        public int? ProsecutorId { get; set; }

        [StringLength(100)]
        public string? CaseNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = null!;

        public DateTime? FilingDate { get; set; }

        public DateTime? NextHearingDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public string? Outcome { get; set; }

        public string? Notes { get; set; }

        public bool IsConfidential { get; set; }
    }

    // Used when assigning a defendant to a court case
    public class AddCourtCaseDefendantDto
    {
        [Required]
        public int CourtCaseId { get; set; }

        [Required]
        public string DefendantId { get; set; } = null!;

        [StringLength(100)]
        public string? Role { get; set; }

        public bool IsPrimary { get; set; }
    }

    // Used when removing a defendant from a court case
    public class RemoveCourtCaseDefendantDto
    {
        [Required]
        public int CourtCaseId { get; set; }

        [Required]
        public string DefendantId { get; set; } = null!;
    }
}
