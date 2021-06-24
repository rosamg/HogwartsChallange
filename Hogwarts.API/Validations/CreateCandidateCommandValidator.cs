using FluentValidation;
using Hogwarts.API.Command;
using Hogwarts.Domain.Entities;
using Hogwarts.Domain.Repositories;
using System;
using System.Linq;

namespace Hogwarts.API.Validations
{
    public class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand>
    {
        private readonly ICandidateRepository _candidateRespository;


        public CreateCandidateCommandValidator(ICandidateRepository candidateRespository)
        {
            _candidateRespository = candidateRespository;

            RuleFor(x => x.Candidate.Name).MaximumLength(20).Must(IsLetter).WithMessage("Name must be only letters");
            RuleFor(x => x.Candidate.Lastname).MaximumLength(20).Must(IsLetter).WithMessage("Lastname must be only letters");
            RuleFor(x => x.Candidate.IdentificationNumber).NotEmpty();
            RuleFor(x => x.Candidate.IdentificationNumber.ToString()).MaximumLength(10).WithMessage("Identification Number must be only numbers");
            RuleFor(x => x.Candidate.IdentificationNumber).Must(IsUniqueIdentifier).WithMessage("Identifier must be unique");
            RuleFor(x => x.Candidate.Age.ToString()).MaximumLength(2).WithMessage("Age must be maximun 2 digits"); ;
            RuleFor(x => x.Candidate.HouseType).IsEnumName(typeof(HouseType), caseSensitive: false).WithMessage("House type must be " + string.Join(",", Enum.GetNames(typeof(HouseType))));           
        }

        private bool IsLetter(string value) {
            value = string.IsNullOrEmpty(value) ? string.Empty : string.Join("", value.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            return value.All(Char.IsLetter);
        }

        private bool IsUniqueIdentifier(int IdentificationNumber)
        {
            var candidate = _candidateRespository.FindByIdentifacationNumber(IdentificationNumber);
            candidate.Wait();

            return candidate.Result == null;
        }

    }
}
