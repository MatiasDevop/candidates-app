using AutoFixture;
using Candidates.Backend.Api.Controllers;
using Candidates.Backend.Application.Candidates;
using Candidates.Backend.Application.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Candidate.Backend.Api.Tests.V1.Controllers
{
    public class CandidateControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<ICandidateService> _candidateServiceMock;
        private readonly CandidateController _sut;

        public CandidateControllerTests()
        {
                _fixture = new Fixture();
            _candidateServiceMock = _fixture.Freeze<Mock<ICandidateService>>();
            _sut = new CandidateController(_candidateServiceMock.Object);// creates the implementation in-memory
        }

        [Fact]
        public async Task GetAllCandidates_ShouldReturnOkResponse_WhenDataFound()
        {
            //Arrange
            var candidatesMock = _fixture.Create<List<CandidateDto>>();
            _candidateServiceMock.Setup(x => x.GetAllCandidatesAsync()).ReturnsAsync(candidatesMock);

            //Act
            var result = await _sut.GetAllCandidatesAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value
                .Should().NotBeNull()
                .And.BeOfType(candidatesMock.GetType());
            _candidateServiceMock.Verify(x => x.GetAllCandidatesAsync(), Times.Once);
        }
    }
}