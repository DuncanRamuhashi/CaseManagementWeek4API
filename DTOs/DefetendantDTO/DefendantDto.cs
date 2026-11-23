using System.ComponentModel.DataAnnotations;

namespace CaseManagementWeek4API.DTOs.DefetendantDTO
{
    // Used to display defendant information
    public class DefendantDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Idnumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public bool? IsInCustody { get; set; }
    }

    // Used when registering a new defendant
    public class CreateDefendantDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public string? Idnumber { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Nationality { get; set; }

        public bool? IsInCustody { get; set; }
    }

    // Used when updating an existing defendant
    public class UpdateDefendantDto
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string? Name { get; set; }

        public string? Idnumber { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Nationality { get; set; }

        public bool? IsInCustody { get; set; }
    }
}
