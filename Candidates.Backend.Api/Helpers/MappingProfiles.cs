using AutoMapper;
using Candidates.Backend.Application.Dtos;
using Candidates.Backend.Domain.Entities;

namespace Candidates.Backend.Api.Helpers
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Candidate, CandidateDto>();
            CreateMap<CandidateDto, Candidate>()
                .ForMember(c => c.Experiences, opt => opt.Ignore());

            CreateMap<CandidateExperience, ExperienceDto>();
            CreateMap<ExperienceDto, CandidateExperience>();
        }
    }
}
