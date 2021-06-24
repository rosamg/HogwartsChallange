using AutoMapper;
using Hogwarts.API.Models;
using Hogwarts.API.Queries;
using Hogwarts.Infrastructure;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hogwarts.API.Handlers
{
    public class GetAllCandidatesQueryHandler : IRequestHandler<GetAllCandidatesQuery, List<CandidateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCandidatesQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CandidateDto>> Handle(GetAllCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _unitOfWork.CandidateRepository.GetAllCandidatesAsync();

            return candidates == null ? null : _mapper.Map<List<CandidateDto>>(candidates);
        }
    }
}
