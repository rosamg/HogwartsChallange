using AutoMapper;
using Hogwarts.API.Command;
using Hogwarts.API.Handlers;
using Hogwarts.API.Mappers;
using Hogwarts.API.Models;
using Hogwarts.Domain.Entities;
using Hogwarts.Infrastructure;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Hogwarts.UnitTest.Handlers
{
    [TestFixture]
    public class CreateCandidateCommandHandlerTest
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private IMapper _mapper;        

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(c => c.CandidateRepository.Add(It.IsAny<Candidate>())).Returns(GetCandidateFake());

            var myProfile = new CandidateProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);            
        }

        [Test]
        public async Task Handler_Success()
        {
            var handler = new CreateCandidateCommandHandler(_unitOfWork.Object, _mapper);
            var result = await handler.Handle(new CreateCandidateCommand(GetCandidateDtoFake()), new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.AreEqual(1, result.CandidateId);
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
    }
}
