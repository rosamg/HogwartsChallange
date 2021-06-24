using Hogwarts.API.Command;
using Hogwarts.API.Handlers;
using Hogwarts.Domain.Entities;
using Hogwarts.Infrastructure;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Hogwarts.UnitTests.Handlers
{
    [TestFixture]
    public class DeleteCandidateCommandHandlerTest
    {
        private Mock<IUnitOfWork> _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();            
            _unitOfWork.Setup(c => c.CandidateRepository.Delete(It.IsAny<Candidate>()));
        }

        [Test]
        public async Task Delete_Handler_Task_Success()
        {
            _unitOfWork.Setup(c => c.CandidateRepository.GetCandidateAsync(It.IsAny<int>()).Result).Returns(GetCandidateFake());
            var handler = new DeleteCandidateCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(new DeleteCandidateCommand(1), new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task Delete_Handler_Task_Not_Found()
        {
            _unitOfWork.Setup(c => c.CandidateRepository.GetCandidateAsync(It.IsAny<int>()).Result);

            var handler = new DeleteCandidateCommandHandler(_unitOfWork.Object);
            var result = await handler.Handle(new DeleteCandidateCommand(2), new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.IsFalse(result);
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
