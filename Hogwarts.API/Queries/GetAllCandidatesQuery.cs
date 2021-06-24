using Hogwarts.API.Models;
using MediatR;
using System.Collections.Generic;

namespace Hogwarts.API.Queries
{
    public class GetAllCandidatesQuery : IRequest<List<CandidateDto>>
    {
    }
}
