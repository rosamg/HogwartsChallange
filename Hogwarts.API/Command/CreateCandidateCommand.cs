using Hogwarts.API.Models;
using MediatR;

namespace Hogwarts.API.Command
{
    public class CreateCandidateCommand : IRequest<CandidateDto>
    {
        public CandidateDto Candidate { get; set; }

        public CreateCandidateCommand(CandidateDto candidateDto)
        {
            Candidate = candidateDto;
        }
    }
}
