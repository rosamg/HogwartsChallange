using Hogwarts.Domain.Entities;

namespace Hogwarts.API.Models
{
    public class CandidateDto
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int IdentificationNumber { get; set; }
        public int Age { get; set; }
        public string HouseType { get; set; }
    }
}
