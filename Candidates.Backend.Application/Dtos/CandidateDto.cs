using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Candidates.Backend.Application.Dtos
{
    public class CandidateDto
    {
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid CandidateId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; }
        public DateTime InsertDate { get; set; }
        public ICollection<ExperienceDto> Experiences { get; set; }
    }
}
