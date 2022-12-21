using Candidates.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.Backend.Application.Dtos
{
    public class ExperienceDto
    {
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid CandidateExperienceId { get; set; }
        public Guid CandidateId { get; set; }

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
