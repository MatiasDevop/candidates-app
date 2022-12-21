using AutoMapper;
using Candidates.Backend.Application.Dtos;
using Candidates.Backend.Application.Exceptions;
using Candidates.Backend.Domain.Entities;
using Candidates.Backend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Candidates.Backend.Application.Candidates
{
    public class CandiateService : ICandidateService
    {
        private readonly CandidateDbContext _context;
        private readonly ILogger<CandiateService> _logger;
        private readonly IMapper _mapper;
        public CandiateService(CandidateDbContext context, ILogger<CandiateService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task DeleteCandidateAsync(Guid candidateId)
        {
            _logger.LogInformation($"Deleting Candidate with Id:{candidateId}");

            var entity = await _context.Candidates.FindAsync(candidateId);

            if (entity == null)
                throw new NotFoundException("Candidate", candidateId);

            //Remove all the experiencies for the candidate
            var experienceToRemove = _context.CandidateExperiences
                .Where(x => x.CandidateExperienceId == candidateId);
            
            _context.RemoveRange(experienceToRemove);
            _context.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Candidate>> GetAllCandidatesAsync()
        {
            _logger.LogInformation($"Get all Candidates...");

            return await _context.Candidates.Include(c => c.Experiences).ToListAsync();
        }

        public async Task<CandidateDto> GetByIdAsync(Guid id)
        {
            _logger.LogInformation($"Getting Candidate by ID{id}");

            var entity = await _context.Candidates.Where(x => x.CandidateId == id)
                .Include(c => c.Experiences)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new NotFoundException("Candidate", id);
            
            return _mapper.Map<CandidateDto>(entity);
        }

        public async Task<CandidateDto> SaveAsync(CandidateDto newCandidate)
        {
            _logger.LogInformation($"Creating a new Candidate {newCandidate.Name}");
            
            var entity = await _context.Candidates
                       .Where(c => c.Name == newCandidate.Name)
                       .FirstOrDefaultAsync();

            if (entity != null)
                throw new AlreadyExistsException("Name:" + entity.Name);

            //Mapping
            var candidate = _mapper.Map<Candidate>(newCandidate);
            
            candidate.CandidateId = new Guid();
            candidate.InsertDate = DateTime.Now;
            candidate.Experiences = new List<CandidateExperience>();

            foreach (var item in newCandidate.Experiences)
            {
                var candidateEx = _mapper.Map<CandidateExperience>(item);
              
                candidateEx.CandidateExperienceId = new Guid();
                candidateEx.InsertDate = DateTime.Now;

                candidate.Experiences.Add(candidateEx);
            }

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<CandidateDto>(candidate);
        }

        public async Task<bool> UpdateCandidateAsync(Guid candidateId, CandidateDto candidate)
        {
            _logger.LogInformation($"Updating a Candidate {candidate.Name}");

            var entity = await _context.Candidates.Include(c => c.Experiences)
                        .Where(c => c.CandidateId == candidateId)
                        .FirstOrDefaultAsync();

            if (entity == null)
                throw new NotFoundException("Candidate:", candidate.Name);

            //Mapping
             _mapper.Map(candidate, entity);
            
            entity.CandidateId = candidateId;
            entity.ModifyDate = DateTime.Now;

            if (candidate.Experiences == null)
                candidate.Experiences = new List<ExperienceDto>();

            //Delete
            var experiences = candidate.Experiences.Select(e => e.CandidateExperienceId).ToList();

            var toDelete = entity.Experiences.Where(ex => !experiences.Contains(ex.CandidateExperienceId)).ToList();
            _context.CandidateExperiences.RemoveRange(toDelete);

            //Add
            var toAdd = candidate.Experiences
                                .Where(ex => ex.CandidateExperienceId == Guid.Empty)
                                .Select(ex => _mapper.Map<CandidateExperience>(ex)).ToList();

            foreach (var item in toAdd)
            {
                item.ModifyDate = DateTime.Now;
                entity.Experiences.Add(item);
            }

            //Update
            var toUpdate = candidate.Experiences
                                .Where(ex => ex.CandidateExperienceId != Guid.Empty)
                                .ToList();

            foreach (var item in toUpdate)
            {
                var candidateExperience = entity.Experiences.FirstOrDefault(ex => ex.CandidateExperienceId == item.CandidateExperienceId);
                if (candidateExperience != null)
                {
                    _mapper.Map(item, candidateExperience);

                    candidateExperience.CandidateId = candidateId;
                    candidateExperience.ModifyDate = DateTime.Now;
                }
            }

            _context.Candidates.Update(entity);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}
