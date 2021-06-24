using Hogwarts.API.Command;
using Hogwarts.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hogwarts.API.Handlers
{
    public class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCandidateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.CandidateRepository.GetCandidateAsync(request.CandidateId);

            if (candidate == null)
            {
                return false;
            }

            _unitOfWork.CandidateRepository.Delete(candidate);

            _unitOfWork.Commit();
            return true;
        }
    }
}
