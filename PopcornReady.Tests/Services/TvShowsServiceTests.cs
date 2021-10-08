using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PopcornReady.Core.ApiServices;
using PopcornReady.Core.Data;
using PopcornReady.Core.Data.Entities;
using PopcornReady.Core.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestSupport.EfHelpers;
using Xunit;

namespace PopcornReady.Tests.Services
{
    public class TvShowsServiceTests : IDisposable
    {
        private readonly DataContext _context = new(SqliteInMemory.CreateOptions<DataContext>());
        private readonly Mock<ILogger<TvShowsService>> _mockLogger = new();
        private readonly Mock<ITvShowsApiService> _mockTvShowsApiService = new();
        private readonly TvShowsService _sut;

        public TvShowsServiceTests()
        {
            _context.Database.EnsureCreated();
            _sut = new TvShowsService(_context, _mockTvShowsApiService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task AddAsync_AddsTvShowToDatabase_If_ItDoesntExist()
        {
            var tvShow = CreateTvShow();
            int userId = 1;

            await _sut.AddAsync(tvShow, userId);

            _context.TvShows.Count().Should().Be(1);
        }

        [Fact]
        public async Task AddAsync_DoesNotAddUserTvShow_If_ItIsAlreadyTracked()
        {
            var tvShow = CreateTvShow();
            var user = new AppUser();
            _context.AppUsers.Add(user);
            _context.TvShows.Add(tvShow);
            _context.SaveChanges();
            _context.UserTvShows.Add(new UserTvShow { TvShowId = tvShow.Id, UserId = user.Id });
            _context.SaveChanges();

            await _sut.AddAsync(tvShow, user.Id);

            _context.UserTvShows.Count().Should().Be(1);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async Task FindAsync_CallsApiService_When_TvShowDoesNotExistInDatabaseAsync()
        {
            //Arrange
            var tvShow = CreateTvShow();
            _mockTvShowsApiService.Setup(x => x.GetTvSeriesAsync(tvShow.Name)).ReturnsAsync(tvShow);

            //Act
            var result = await _sut.FindAsync(tvShow.Name);

            //Assert
            _mockTvShowsApiService.Verify(x => x.GetTvSeriesAsync(tvShow.Name), Times.Once);
            result.Should().NotBeNull();
            result.Name.Should().Be(tvShow.Name);
        }

        private TvShow CreateTvShow()
        {
            return new TvShow { Name = "batman" };
        }
    }
}
