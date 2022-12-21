
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Candidates.Backend.Domain.Entities
{
    public class Candidate
    {
        public Guid CandidateId { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string Email { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; } = null;

        [JsonIgnore]
        public virtual ICollection<CandidateExperience> Experiences { get; set; }
    }
}
