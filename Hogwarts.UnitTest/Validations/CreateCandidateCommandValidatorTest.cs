

using FluentValidation.TestHelper;
using Hogwarts.API.Command;
using Hogwarts.API.Models;
using Hogwarts.API.Validations;
using Hogwarts.Domain.Entities;
using Hogwarts.Domain.Repositories;
using Moq;
using NUnit.Framework;

namespace Hogwarts.UnitTest.Validations
{
    [TestFixture]
    public class CreateCandidateCommandValidatorTest
    {
        private CreateCandidateCommandValidator _validator;
        private Mock<ICandidateRepository> _candidateRepository;

        [SetUp]
        public void SetUp()
        {
            _candidateRepository = new Mock<ICandidateRepository>();
            _candidateRepository.Setup(x => x.FindByIdentifacationNumber(111111111).Result).Returns(GetCandidateFake());
            _validator = new CreateCandidateCommandValidator(_candidateRepository.Object);
        }

        [Test]
        public void Should_have_error_when_identifier_not_unique()
        {
            var model = new CreateCandidateCommand(new CandidateDto() { IdentificationNumber = 111111111 });
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.Candidate.IdentificationNumber);
        }

        [Test]
        public void Should_not_have_error_when_identifier_not_unique()
        {
            var model = new CreateCandidateCommand(new CandidateDto() { IdentificationNumber = 111111112 });
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(c => c.Candidate.IdentificationNumber);
        }

        [Test]
        public void Should_have_error_when_identifier_is_null()
        {
            var model = new CreateCandidateCommand(new CandidateDto());
            var result = _validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(c => c.Candidate.IdentificationNumber);
        }

        [Test]
        public void Should_not_have_error_when_identifier_is_specified()
        {
            var model = new CreateCandidateCommand(new CandidateDto() { IdentificationNumber = 1111111112 });
            var result = _validator.TestValidate(model);

            result.ShouldNotHaveValidationErrorFor(c => c.Candidate.IdentificationNumber);
        }

        private Candidate GetCandidateFake()
        {
            return new Candidate()
            {
                CandidateId = 1,
                Lastname = "Test",
                IdentificationNumber = 1111111111,
                Name = "Test",
                Age = 20,
                HouseType = HouseType.Gryffindor
            };
        }
    }
}
