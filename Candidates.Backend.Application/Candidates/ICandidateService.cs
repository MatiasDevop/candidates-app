using Candidates.Backend.Application.Dtos;
using Candidates.Backend.Domain.Entities;

namespace Candidates.Backend.Application.Candidates
{
    public interface ICandidateService
    {
        Task<List<Candidate>> GetAllCandidatesAsync();
        Task<CandidateDto> SaveAsync(CandidateDto newCandidate);
        Task<CandidateDto> GetByIdAsync(Guid id);
        Task DeleteCandidateAsync(Guid id);
        Task<bool> UpdateCandidateAsync(Guid id, CandidateDto candidate);
    }
}
