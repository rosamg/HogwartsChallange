using Hogwarts.API.Command;
using Hogwarts.Domain.Entities;
using Hogwarts.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hogwarts.API.Handlers
{
    public class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public UpdateCandidateCommandHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;            
        }
        public async Task<bool> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.CandidateRepository.GetCandidateAsync(request.Id);

            if (candidate == null)
                return false;

            candidate.Name = request.Candidate.Name;
            candidate.Lastname = request.Candidate.Lastname;
            candidate.Age = request.Candidate.Age;
            candidate.HouseType = Enum.Parse<HouseType>(request.Candidate.HouseType, true);
            candidate.IdentificationNumber = request.Candidate.IdentificationNumber;            

            _unitOfWork.CandidateRepository.Update(candidate);            

            _unitOfWork.Commit();

            return true;
        }
    }
}
