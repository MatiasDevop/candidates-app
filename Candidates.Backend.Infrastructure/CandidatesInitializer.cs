using Candidates.Backend.Domain.Entities;

namespace Candidates.Backend.Infrastructure
{
    public class CandidatesInitializer
    {
        private readonly CandidateDbContext _candidateDbContext;
        public CandidatesInitializer(CandidateDbContext candidateDbContext)
        {
            _candidateDbContext = candidateDbContext;
        }
        public void Seed()
        {
            if (!_candidateDbContext.Candidates.Any())
            {
                var candidates = new List<Candidate>
                {
                    new Candidate()
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

                _candidateDbContext.Candidates.AddRange(candidates);
                _candidateDbContext.SaveChanges();
            }
        }
    }
}
