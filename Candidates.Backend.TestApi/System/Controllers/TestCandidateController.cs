using AutoMapper;
using Candidates.Backend.Api.Controllers;
using Candidates.Backend.Api.Helpers;
using Candidates.Backend.Application.Candidates;
using Candidates.Backend.Application.Dtos;
using Candidates.Backend.TestApi.MockData;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Candidates.Backend.TestApi.System.Controllers
{
    public class TestCandidateController
    {
        private readonly Mock<ICandidateService> _candidateService;
        private readonly IMapper _mapper;
        public TestCandidateController()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _candidateService = new Mock<ICandidateService>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess()
        {
            //Arrange
            var listCandidates = _mapper.Map<List<CandidateDto>>(CandidateMockData.GetCandidates());
            _candidateService.Setup(x => x.GetAllCandidatesAsync()).ReturnsAsync(listCandidates);

            var sut = new CandidateController(_candidateService.Object);

            //Act
            var result = (OkObjectResult)await sut.GetAllCandidatesAsync();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnNoContentStatus()
        {
            //Arrange
            var listCandidates = _mapper.Map<List<CandidateDto>>(CandidateMockData.EmptyList());
            _candidateService.Setup(x => x.GetAllCandidatesAsync()).ReturnsAsync(listCandidates);
            var sut = new CandidateController(_candidateService.Object);

            //Act
            var result = (NoContentResult) await sut.GetAllCandidatesAsync();

            //Assert
            result.StatusCode.Should().Be(204);
            _candidateService.Verify(x => x.GetAllCandidatesAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task SaveCandidateAndExperienceAsync_ShouldSaveAsyncOnce()
        {
            //Arrange
            var candidate = CandidateMockData.AddCandidateAndExperience();
            var sut = new CandidateController(_candidateService.Object);

            //Act
            var result = await sut.CreateCandidateAsync(candidate);

            //Assert
            _candidateService.Verify(x => x.SaveAsync(candidate), Times.Exactly(1));
        }

        [Fact]
        public async Task UpdateCandidateAndExperienceAsync_ShouldUpdateSaveAsyncOnce()
        {
            //Arrange
            var toUpdatecandidate = CandidateMockData.UpdateCandidateAndExperience();
            var sut = new CandidateController(_candidateService.Object);
            var id = new Guid("a63797fa-1c14-438c-8df9-7a07d83091ed");

            //Act
            var result = await sut.UpdateCandidateAsync(id, toUpdatecandidate);

            //Assert
            _candidateService.Verify(x => x.UpdateCandidateAsync(id, toUpdatecandidate), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnSuccess()
        {
            //Arrange
            var sut = new CandidateController(_candidateService.Object);
            var candidateId = new Guid("a63797fa-1c14-438c-8df9-7a07d83090ed");

            //Act
            var result = await sut.GetCandidateByIdAsync(candidateId);

            //Assert
            _candidateService.Verify(x => x.GetByIdAsync(candidateId), Times.Exactly(1));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNotContentResult()
        {
            //Arrange
            var listCandidates = _mapper.Map<List<CandidateDto>>(CandidateMockData.EmptyList());
            _candidateService.Setup(x => x.GetAllCandidatesAsync()).ReturnsAsync(listCandidates);
            var sut = new CandidateController(_candidateService.Object);
            var candidateId = new Guid("a63797fa-1c14-438c-8df9-7a07d83090ed");

            //Act
            var result = (NoContentResult)await sut.GetCandidateByIdAsync(candidateId);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteCandidateByIdAsync_ShouldReturnSuccess()
        {
            //Arrange
            var listCandidates = _mapper.Map<List<CandidateDto>>(CandidateMockData.EmptyList());
            _candidateService.Setup(x => x.GetAllCandidatesAsync()).ReturnsAsync(listCandidates);
            var sut = new CandidateController(_candidateService.Object);
            var candidateId = new Guid("a63797fa-1c14-438c-8df9-7a07d83091ed");

            //Act
            var result = (OkResult)await sut.DeleteCandidateAsync(candidateId);

            //Assert
            result.StatusCode.Should().Be(200);
        }
    }
}
