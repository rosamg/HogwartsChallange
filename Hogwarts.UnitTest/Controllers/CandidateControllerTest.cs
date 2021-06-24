using Hogwarts.API.Command;
using Hogwarts.API.Controllers;
using Hogwarts.API.Models;
using Hogwarts.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hogwarts.UnitTest.Controllers
{
    [TestFixture]
    public class CandidateControllerTest
    {
        private Mock<IMediator> _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = new Mock<IMediator>();
        }


        [Test]
        public async Task Get_Candidate_Success()
        {
            _mediator.Setup(x => x.Send(It.IsAny<GetAllCandidatesQuery>(), new System.Threading.CancellationToken()))
                .ReturnsAsync(GetCandidateFake());

            var candidateController = new CandidateController(_mediator.Object);

            //Action
            var result = await candidateController.Get();

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }       

        [Test]
        public async Task Create_Candidate_Success()
        {
            _mediator.Setup(x => x.Send(new CreateCandidateCommand(GetCandidateFake().FirstOrDefault()), new System.Threading.CancellationToken()))
                .ReturnsAsync(GetCandidateFake().FirstOrDefault());

            var candidateController = new CandidateController(_mediator.Object);

            //Action
            var result = await candidateController.Post(GetCandidateFake().FirstOrDefault());

            //Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task Update_Candidate_Success()
        {
            _mediator.Setup(x => x.Send(It.IsAny<UpdateCandidateCommand>(), new System.Threading.CancellationToken()))
              .ReturnsAsync(true);

            var candidateController = new CandidateController(_mediator.Object);

            //Action
            var result = await candidateController.Put(1, GetCandidateFake().FirstOrDefault());

            //Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);

        }
        private List<CandidateDto> GetCandidateFake()
        {
            return new List<CandidateDto>(){ new CandidateDto()
            {
                CandidateId = 1,
                Lastname = "Test",
                IdentificationNumber = 1111111111,
                Name = "Test",
                Age = 20,
                HouseType = "Gryffindor"
            }};
        }

    }
}