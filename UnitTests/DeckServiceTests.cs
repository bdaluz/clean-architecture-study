using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class DeckServiceTests
    {
        private readonly Mock<IDeckRepository> _deckRepositoryMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly DeckService _sut;

        public DeckServiceTests()
        {
            _deckRepositoryMock = new Mock<IDeckRepository>();
            _cacheServiceMock = new Mock<ICacheService>();
            _sut = new DeckService(_deckRepositoryMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task Create_Deck_Should_CallRepository_And_InvalidateCache_When_Valid()
        {
            var dto = new CreateDeckDto
            {
                Title = "Test deck Title",
                Description = "Teste deck Description",
            };

            var result = await _sut.CreateDeckAsync(dto);

            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Title.Should().Be(dto.Title);
            _deckRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Deck>()), Times.Once);
            _deckRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);

            _cacheServiceMock.Verify(x => x.RemoveAsync("decks_list"), Times.Once);
        }
    }
}
