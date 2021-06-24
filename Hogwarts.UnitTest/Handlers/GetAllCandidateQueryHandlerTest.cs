using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hogwarts.API.Handlers;
using Hogwarts.API.Mappers;
using Hogwarts.API.Queries;
using Hogwarts.Domain.Entities;
using Hogwarts.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Hogwarts.UnitTests.Handlers
{
    [TestFixture]
    public class GetAllCandidateQueryHandlerTest
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(c => c.CandidateRepository.GetAllCandidatesAsync().Result).Returns(GetCandidateFake());

            var myProfile = new CandidateProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);
        }

        [Test]
        public async Task Get_All_Handler_Success()
        {
            var handler = new GetAllCandidatesQueryHandler(_unitOfWork.Object, _mapper);
            var result = await handler.Handle(new GetAllCandidatesQuery(), new System.Threading.CancellationToken());

            Assert.NotNull(result);
            Assert.AreEqual(1, result[0].CandidateId);

        }

        private List<Candidate> GetCandidateFake()
        {
            return new List<Candidate>(){ new Candidate()
            {
                CandidateId = 1,
                Lastname = "Test",
                IdentificationNumber = 1111111111,
                Name = "Test",
                Age = 20,
                HouseType = HouseType.Gryffindor
            }};
        }
    }
}


