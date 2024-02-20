using AutoMapper;
using Candidates.Backend.Api.Helpers;
using Candidates.Backend.Application.Candidates;
using Candidates.Backend.Application.Dtos;
using Candidates.Backend.Infrastructure;
using Candidates.Backend.TestApi.MockData;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Candidates.Backend.TestApi.System.Services
{
    public class TestCandidateService
    {
        private readonly CandidateDbContext _dbContext;
        private readonly ILogger<CandiateService> _logger;
        private readonly IMapper _mapper;
        public TestCandidateService()
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

            // Setup Inmemory 
            var options = new DbContextOptionsBuilder<CandidateDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new CandidateDbContext(options);

            _dbContext.Database.EnsureCreated();
            _logger = Mock.Of<ILogger<CandiateService>>();
        }

        [Fact]
        public async Task GetAllAsync_ReturnCandidateGetCollection()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();

            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            var result = await sut.GetAllCandidatesAsync();

            //Assert
            result.Should().HaveCount(CandidateMockData.GetCandidates().Count);
        }

        [Fact]
        public async Task GetCandidateByIdAsync_ReturnSuccess()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();
            var idCandidate = new Guid("a63797fa-1c14-438c-8df9-7a07d83091ed");

            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            var result = await sut.GetByIdAsync(idCandidate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(CandidateMockData.GetCandidates()[0].CandidateId, result.CandidateId);
            Assert.True(CandidateMockData.GetCandidates()[0].CandidateId == result.CandidateId);
        }

        [Fact]
        public async Task GetCandidateByWrongIdAsync_ReturnException()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();
            var idCandidate = new Guid("a63797fa-1c14-438c-8df9-7a07d83092ed");

            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            Func<Task> result = async () => {
                await sut.GetByIdAsync(idCandidate);
            };

            //Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task DeleteCandidateByIdAsync_ReturnSuccess()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();
            var idCandidate = new Guid("a63797fa-1c14-438c-8df9-7a07d83091ed");

            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            Func<Task> result = async () => {
                await sut.DeleteCandidateAsync(idCandidate);
            };

            //Assert
            await result.Should().NotThrowAsync();
        }

        [Fact]
        public async Task DeleteCandidateByWrongIdAsync_ReturnException()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();
            var idCandidate = new Guid("a63797fa-1c14-438c-8df9-7a07d83092ed");

            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            Func<Task> result = async () => {
                await sut.DeleteCandidateAsync(idCandidate);
            };

            //Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task CreateANewCandidateAndExperiencesAsync_ReturnSucces()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();

            var newCandidate = CandidateMockData.AddCandidateAndExperience();

            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            var result = await sut.SaveAsync(newCandidate);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeSameAs(typeof(CandidateDto));
            int expectedRecord = CandidateMockData.GetCandidates().Count + 1;
            _dbContext.Candidates.Count().Should().Be(expectedRecord);
        }

        [Fact]
        public async Task CreateANewCandidateAndExperiencesAsync_WithSameNameReturnException()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();
            var newCandidate = CandidateMockData.AddCandidateAndExperience();
            newCandidate.Name = "Jhon";
            newCandidate.Surname = "Due";
            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            Func<Task> result = async () => {
                await sut.SaveAsync(newCandidate);
            };

            //Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task UpdateCandidateAndExperiencesAsync_WithWrongIdReturnException()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();
            var newCandidate = CandidateMockData.UpdateCandidateAndExperience();
            newCandidate.CandidateId = new Guid("a63797fa-1c14-438c-8df9-7a07d83666ed");
            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            Func<Task> result = async () => {
                await sut.UpdateCandidateAsync(newCandidate.CandidateId, newCandidate);
            };

            //Assert
            await result.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task UpdateCandidateAndExperiencesAsync_ReturnSuccess()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();
            var candidate = CandidateMockData.UpdateCandidateAndExperience();
            candidate.CandidateId = new Guid("3addc985-b9c5-4620-bfe1-16ad88e383c1");
            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            var result = await sut.UpdateCandidateAsync(candidate.CandidateId, candidate);

            //Assert
            result.Should().BeTrue();
            int expectedRecord = CandidateMockData.GetCandidates().Count;
            _dbContext.Candidates.Count().Should().Be(expectedRecord);
        }

        [Fact]
        public async Task UpdateCandidateAndRemoveExperiencesAsync_ExperienceReturnSuccess()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();
            var candidate = CandidateMockData.UpdateCandidateAndExperience();
            candidate.CandidateId = new Guid("3addc985-b9c5-4620-bfe1-16ad88e383c1");
            candidate.Experiences = new List<ExperienceDto>();
            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            var result = await sut.UpdateCandidateAsync(candidate.CandidateId, candidate);

            //Assert
            result.Should().BeTrue();
            int expectedRecord = CandidateMockData.GetCandidates().Count;
            _dbContext.Candidates.Count().Should().Be(expectedRecord);
        }

        [Fact]
        public async Task UpdateCandidateAndExperiencesAsync_WithSpecificExperience_ReturnSuccess()
        {
            //Arrange
            _dbContext.Candidates.AddRange(CandidateMockData.GetCandidates());
            _dbContext.SaveChanges();
            var candidate = CandidateMockData.UpdateCandidateAndExperience();
            candidate.CandidateId = new Guid("3addc985-b9c5-4620-bfe1-16ad88e383c1");
            candidate.Experiences = new List<ExperienceDto>
                    {
                        new ExperienceDto{
                            CandidateExperienceId = new Guid("4a50bc58-cf93-4e0e-8447-5603a6865f61"),
                            CandidateId = new Guid("3addc985-b9c5-4620-bfe1-16ad88e383c1"),
                            Company = "NEW BRAND",
                            Job = "UX DELOPER",
                            Description = "updating experience",
                            Salary = 4500.0m,
                            BeginDate = DateTime.Now,
                            InsertDate= DateTime.Now
                        }
                    };
            var experiencesRecored = 1;
            var sut = new CandiateService(_dbContext, _logger, _mapper);

            //Act
            var result = await sut.UpdateCandidateAsync(candidate.CandidateId, candidate);
            
            //Assert
            result.Should().BeTrue();
            int expectedRecord = CandidateMockData.GetCandidates().Count;
            _dbContext.Candidates.Where(c => c.CandidateId == candidate.CandidateId)
                .First().Experiences.Count().Should().Be(experiencesRecored);
            _dbContext.Candidates.Count().Should().Be(expectedRecord);
        }
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
