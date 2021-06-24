using Moq;
using NUnit.Framework;
using Hogwarts.API.Command;
using Hogwarts.API.Handlers;
using Hogwarts.API.Models;
using Hogwarts.Infrastructure;
using Hogwarts.Domain.Entities;
using System.Threading.Tasks;

namespace Hogwarts.UnitTests.Handlers
{
    [TestFixture]
    public class UpdateCandidateCommandHandlerTest
    {
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(c => c.CandidateRepository.GetCandidateAsync(It.IsAny<int>()).Result).Returns(GetCandidateFake());
        }

        [Test]
        public async Task Update_handler_Success()
        {

            var handler = new UpdateCandidateCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(new UpdateCandidateCommand(1,GetCandidateDtoFake()), new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.IsTrue(result);
        }

        private CandidateDto GetCandidateDtoFake()
        {
            return new CandidateDto()
            {
                CandidateId = 1,
                Lastname = "Test",
                IdentificationNumber = 1111111111,
                Name = "Test",
                Age = 20,
                HouseType = "Gryffindor"
            };
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
