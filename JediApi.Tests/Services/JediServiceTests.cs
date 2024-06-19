using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Moq;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAll()
        {

            List<Jedi> expectedJedis = new List<Jedi>()
            {
                new Jedi 
                { 
                    Id = 1,
                    Name = "Luke Skywalker", 
                    Strength = 9, 
                    Version = 1 
                },
                new Jedi 
                { 
                    Id = 2,
                    Name = "Mestre Yoda",
                    Strength = 10,
                    Version = 1
                }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedJedis);

            var result = await _service.GetAllAsync();

            Assert.Equal(expectedJedis, result);
        }

        [Fact]
        public async Task GetById_Success()
        {
            Jedi expectedJedi = new Jedi()
            {
                Id = 2,
                Name = "Mestre Yoda",
                Strength = 10,
                Version = 1
            };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(expectedJedi.Id)).ReturnsAsync(expectedJedi);
            var result = await _service.GetByIdAsync(expectedJedi.Id);

            Assert.Equal(expectedJedi, result);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            var idJedi = 1;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(idJedi));

            var result = await _service.GetByIdAsync(idJedi);

            Assert.Null(result);
        }

    }

}