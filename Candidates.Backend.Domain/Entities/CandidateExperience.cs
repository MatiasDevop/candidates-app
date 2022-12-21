using System.ComponentModel.DataAnnotations.Schema;

namespace Candidates.Backend.Domain.Entities
{
    public class CandidateExperience
    {
        public Guid CandidateExperienceId { get; set; }
        public Guid CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Company { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Job { get; set; }

        [Column(TypeName = "varchar(400)")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Salary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; } = null;
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; } = null;
    }
}
