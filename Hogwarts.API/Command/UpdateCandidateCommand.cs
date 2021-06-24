using Hogwarts.API.Models;
using MediatR;

namespace Hogwarts.API.Command
{
    public class UpdateCandidateCommand : IRequest<bool>
    {
        public CandidateDto Candidate { get; set; }
        public int Id { get; set; }

        public UpdateCandidateCommand(int id, CandidateDto candidateDto)
        {
            Id = id;
            Candidate = candidateDto;            
        }
    }
}
