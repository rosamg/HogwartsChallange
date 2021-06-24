using FluentValidation;
using Hogwarts.API.Command;
using Hogwarts.Domain.Entities;
using Hogwarts.Domain.Repositories;

namespace Hogwarts.API.Validations
{
    public class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand>
    {
        private readonly ICandidateRepository _candidateRespository;


        public CreateCandidateCommandValidator(ICandidateRepository candidateRespository)
        {
            _candidateRespository = candidateRespository;

            RuleFor(x => x.Candidate.Name).MaximumLength(20).Matches(@"^[a-zA-Z]+$").WithMessage("Name must be only letters and less than 20 characters");
            RuleFor(x => x.Candidate.Lastname).MaximumLength(20).Matches(@"^[a-zA-Z]+$").WithMessage("Lastname must be only letters and less than 20 characters");
            RuleFor(x => x.Candidate.IdentificationNumber).NotEmpty();
            RuleFor(x => x.Candidate.IdentificationNumber.ToString()).MaximumLength(10).WithMessage("Identification Number must be only numbers and less than 10 characters");
            RuleFor(x => x.Candidate.IdentificationNumber).Must(IsUniqueIdentifier).WithMessage("Identifier must be unique");
            RuleFor(x => x.Candidate.Age.ToString()).MaximumLength(2).WithMessage("Age must be maximun 2 digits");
            RuleFor(x => x.Candidate.HouseType).IsEnumName(typeof(HouseType), caseSensitive: false).WithMessage("House type must be Gryffindor, Hufflepuff, Ravenclaw or Slytherin ");           
        }

        private bool IsUniqueIdentifier(int IdentificationNumber)
        {
            var candidate = _candidateRespository.FindByIdentifacationNumber(IdentificationNumber);
            candidate.Wait();

            return candidate.Result == null;
        }

    }
}
