using Candidates.Backend.Application.Exceptions;
using Candidates.Backend.Domain.Entities;
using Candidates.Backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Candidates.Backend.Application.Experiences
{
    public class ExperienceService : IExperienceService
    {
        private readonly CandidateDbContext _context;
        private readonly ILogger<ExperienceService> _logger;
        public ExperienceService(CandidateDbContext context, ILogger<ExperienceService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteExperiencieAsync(Guid experienceId)
        {
            _logger.LogInformation($"Deleting Candidate Experience with Id:{experienceId}");

            var entity = await _context.CandidateExperiences.FindAsync(experienceId);

            if (entity == null)
                throw new NotFoundException("Candidate Experience :", experienceId);

            _context.CandidateExperiences.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<CandidateExperience> SaveCandidateExperiencieAsync(CandidateExperience newExperience)
        {
            _logger.LogInformation($"Creating a new Candidate Experience {newExperience.Job}");

            var entity = await _context.CandidateExperiences
                       .Where(c => c.Description == newExperience.Description)
                       .FirstOrDefaultAsync();

            if (entity != null)
                throw new AlreadyExistsException("Experince:" + entity.Description);

            newExperience.CandidateExperienceId = new Guid();
            newExperience.InsertDate = DateTime.Now;

            _context.CandidateExperiences.Add(newExperience);
            await _context.SaveChangesAsync();

            return newExperience;
        }

        public async Task<bool> UpdateExperienceAsync(Guid experienceId, CandidateExperience experience)
        {
            _logger.LogInformation($"Updating a Candidate experience {experience.Description}");

            var entity = await _context.CandidateExperiences
                        .Where(c => c.CandidateExperienceId == experienceId)
                        .FirstOrDefaultAsync();

            if (entity == null)
                throw new NotFoundException("Candidate experience: ", experience.Description);

            experience.ModifyDate = DateTime.Now;

            _context.Update(experience);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
