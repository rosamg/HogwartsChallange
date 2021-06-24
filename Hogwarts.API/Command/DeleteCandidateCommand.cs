using MediatR;

namespace Hogwarts.API.Command
{
    public class DeleteCandidateCommand : IRequest<bool>
    {
        public int CandidateId { get; set; }
        public DeleteCandidateCommand(int candidateId)
        {
            CandidateId = candidateId;
        }
    }
}
