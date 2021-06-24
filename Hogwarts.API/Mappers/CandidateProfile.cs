using AutoMapper;
using Hogwarts.API.Models;
using Hogwarts.Domain.Entities;

namespace Hogwarts.API.Mappers
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateDto>();
        }        
    }
}
