namespace Hogwarts.Domain.Entities
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int IdentificationNumber { get; set; }
        public int Age { get; set; }
        public HouseType HouseType { get; set; }
    }
}
