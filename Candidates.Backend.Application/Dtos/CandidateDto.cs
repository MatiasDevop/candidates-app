using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Candidates.Backend.Application.Dtos
{
    /// <summary>
    /// This class is use for CandidateDto Model
    /// </summary>
    public class CandidateDto
    {
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid CandidateId { get; set; }

        /// <summary>
        /// Candidate's Name
        /// </summary>
        [Column(TypeName = "varchar(50)")]
        [Required]
        [MinLength(15, ErrorMessage = "Name must be 15 characters")]
        [MaxLength(50, ErrorMessage = "Name cannot be over 50 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Candidate surname
        /// </summary>
        [Column(TypeName = "varchar(150)")]
        [Required]
        [MinLength(15, ErrorMessage = "Surename must be 15 characters")]
        [MaxLength(50, ErrorMessage = "Surname cannot be over 50 characters")]
        public string Surname { get; set; }

        /// <summary>
        /// Candidate Birthdate
        /// </summary>
        public DateTime Birthdate { get; set; }

        [Column(TypeName = "varchar(250)")]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public DateTime InsertDate { get; set; }
        public ICollection<ExperienceDto> Experiences { get; set; }
    }
}
