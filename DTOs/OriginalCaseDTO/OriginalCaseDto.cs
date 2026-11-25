
using System.ComponentModel.DataAnnotations;

namespace CaseManagementWeek4API.DTOs.OriginalCaseDTO
{
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
