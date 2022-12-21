using Candidates.Backend.Application.Dtos;
using Candidates.Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.Backend.TestApi.MockData
{
    public class CandidateMockData
    {
        public static List<Candidate> GetCandidates()
        {
            return new List<Candidate>
            {
                new Candidate
                {
                    CandidateId = new Guid("a63797fa-1c14-438c-8df9-7a07d83091ed"),
                    Name = "Jhon",
                    Surname = "Due",
                    Birthdate = DateTime.Now,
                    Email = "jon@test.com",
                    InsertDate = DateTime.Now,
                    Experiences = new List<CandidateExperience>()
                },
                new Candidate
                {
                    CandidateId = new Guid("3addc985-b9c5-4620-bfe1-16ad88e383c1"),
                    Name = "Pepe",
                    Surname = "many",
                    Birthdate = DateTime.Now,
                    Email = "pepe@test.com",
                    InsertDate = DateTime.Now,
                    Experiences = new List<CandidateExperience>
                    {
                        new CandidateExperience
                        {
                            CandidateExperienceId = new Guid("4a50bc58-cf93-4e0e-8447-5603a6865f61"),
                            CandidateId = new Guid("3addc985-b9c5-4620-bfe1-16ad88e383c1"),
                            Company = "APPLE",
                            Job = "Developer",
                            Description = "full time job",
                            Salary = 4500.0m,
                            BeginDate = DateTime.Now,
                            InsertDate= DateTime.Now
                        }
                    }
                }
            };
        }

        public static List<Candidate> EmptyList()
        {
            return new List<Candidate>();
        }

        public static CandidateDto AddCandidateAndExperience()
        {
            return new CandidateDto
            {
                CandidateId = Guid.Empty,
                Name = "Summer",
                Surname = "Mati",
                Birthdate = DateTime.Now,
                Email = "sumer@tes.com",
                InsertDate = DateTime.Now,
                Experiences = new List<ExperienceDto>
                {
                    new ExperienceDto
                    {
                        CandidateExperienceId = Guid.Empty,
                        CandidateId = Guid.Empty,
                        Company = "APPLE",
                        Job = "Developer",
                        Description = "full time job",
                        Salary = 4500.0m,
                        BeginDate = DateTime.Now,
                        InsertDate= DateTime.Now
                    }
                }
            };
        }

        public static CandidateDto UpdateCandidateAndExperience()
        {
            return new CandidateDto
            {
                CandidateId = new Guid("a63797fa-1c14-438c-8df9-7a07d83091ed"),
                Name = "Summer2",
                Surname = "Mati2",
                Birthdate = DateTime.Now,
                Email = "sume2r@tes.com",
                InsertDate = DateTime.Now,
                Experiences = new List<ExperienceDto>
                {
                    new ExperienceDto
                    {
                        CandidateExperienceId = Guid.Empty,
                        CandidateId = Guid.Empty,
                        Company = "APPLE2",
                        Job = "Developer",
                        Description = "full time job",
                        Salary = 4500.0m,
                        BeginDate = DateTime.Now,
                        InsertDate= DateTime.Now
                    }
                }
            };
        }
    }
}
