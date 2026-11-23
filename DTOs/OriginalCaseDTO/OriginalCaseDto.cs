using System.ComponentModel.DataAnnotations;

namespace CaseManagementWeek4API.DTOs.OriginalCaseDTO
{
    // Used to display original case information
    public class OriginalCaseDto
    {
        public int Id { get; set; }
        public string? CaseNumber { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? CaseType { get; set; }
        public string? Priority { get; set; }
        public string? ReportedBy { get; set; }
    }

    // Used when registering a new original case
    public class CreateOriginalCaseDto
    {
        [Required]
        [StringLength(100)]
        public string? CaseNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public string? CaseType { get; set; }

        public string? Priority { get; set; }

        public string? ReportedBy { get; set; }
    }

    // Used when updating an existing original case
    public class UpdateOriginalCaseDto
    {
        [Required]
        [StringLength(100)]
        public string? CaseNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public string? CaseType { get; set; }

        public string? Priority { get; set; }

        public string? ReportedBy { get; set; }
    }
}
