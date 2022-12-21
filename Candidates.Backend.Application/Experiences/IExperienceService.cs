using Candidates.Backend.Domain.Entities;

namespace Candidates.Backend.Application.Experiences
{
    public interface IExperienceService
    {
        Task<CandidateExperience> SaveCandidateExperiencieAsync(CandidateExperience newExperience);
        Task DeleteExperiencieAsync(Guid id);
        Task<bool> UpdateExperienceAsync(Guid id, CandidateExperience newExperience);

    }
}
