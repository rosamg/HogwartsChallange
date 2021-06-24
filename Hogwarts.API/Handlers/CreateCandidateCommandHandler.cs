using AutoMapper;
using Hogwarts.API.Command;
using Hogwarts.API.Models;
using Hogwarts.Domain.Entities;
using Hogwarts.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hogwarts.API.Handlers
{
    public class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, CandidateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCandidateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<CandidateDto> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = _unitOfWork.CandidateRepository.Add(new Candidate()
            {
                Name = request.Candidate.Name,
                Lastname = request.Candidate.Lastname,
                Age = request.Candidate.Age,
                HouseType = Enum.Parse<HouseType>(request.Candidate.HouseType, true),
                CandidateId = request.Candidate.CandidateId,
                IdentificationNumber = request.Candidate.IdentificationNumber
            });

            _unitOfWork.Commit();

            return Task.FromResult(_mapper.Map<CandidateDto>(candidate));
        }
    }
}
