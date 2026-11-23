using System.ComponentModel.DataAnnotations;

namespace CaseManagementWeek4API.DTOs.ChargeDTO
{
    // Used to display charge information
    public class ChargeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int CourtCaseId { get; set; }

        public string? ChargeCode { get; set; }

        public string? Statute { get; set; }

        public string? Description { get; set; }

        public string? Plea { get; set; }

        public string? Severity { get; set; }
    }

    // Used when creating a new charge
    public class CreateChargeDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        public int CourtCaseId { get; set; }

        [StringLength(50)]
        public string? ChargeCode { get; set; }

        [StringLength(200)]
        public string? Statute { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Plea { get; set; }

        [StringLength(30)]
        public string? Severity { get; set; }
    }

    // Used when updating an existing charge
    public class UpdateChargeDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string? ChargeCode { get; set; }

        [StringLength(200)]
        public string? Statute { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Plea { get; set; }

        [StringLength(30)]
        public string? Severity { get; set; }
    }
}
