using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PopcornReady.Core.ApiServices;
using PopcornReady.Core.Data;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Services;
using System;
using System.Threading.Tasks;
using TestSupport.EfHelpers;
using Xunit;

namespace PopcornReady.Tests.Services
{
    public class TvShowsServiceTests : IDisposable
    {
        private readonly Mock<ITvShowsApiService> _mockTvShowsApiService = new();
        private readonly Mock<ILogger<TvShowsService>> _mockLogger = new();
        private readonly DataContext _context = new(SqliteInMemory.CreateOptions<DataContext>());
        private readonly TvShowsService _sut;

        public TvShowsServiceTests()
        {
            _context.Database.EnsureCreated();
            _sut = new TvShowsService(_context, _mockTvShowsApiService.Object, _mockLogger.Object);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task FindAsync_CallsApiService_When_TvShowDoesNotExistInDatabaseAsync()
        {
            //Arrange
            var tvShow = new TvShow { Name = "batman" };
            _mockTvShowsApiService.Setup(x => x.GetTvSeriesAsync(tvShow.Name)).ReturnsAsync(tvShow);

            //Act
            var result = await _sut.FindAsync("batman");

            //Assert
            _mockTvShowsApiService.Verify(x => x.GetTvSeriesAsync(tvShow.Name), Times.Once);
            result.Should().NotBeNull();
            result.Name.Should().Be(tvShow.Name);
        }
    }
}
