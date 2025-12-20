using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _repository;
        private readonly IDistributedCache _cache;

        private const string CacheKeyDecks = "decks_list";

        public DeckService(IDeckRepository repository, IDistributedCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<DeckDto> CreateDeckAsync(CreateDeckDto dto)
        {
            var deck = new Deck(dto.Title, dto.Description);

            await _repository.AddAsync(deck);
            await _repository.SaveChangesAsync();

            await _cache.RemoveAsync(CacheKeyDecks);

            return new DeckDto
            {
                Id = deck.Id,
                Title = deck.Title,
                Description = deck.Description
            };
        }

        public async Task<List<DeckDto>> GetAllDecksAsync()
        {
            var cachedData = await _cache.GetStringAsync(CacheKeyDecks);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<List<DeckDto>>(cachedData);
            }

            var decks = await _repository.GetAllAsync();

            var deckDtos = decks.Select(d => new DeckDto
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description
            }).ToList();

            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

            await _cache.SetStringAsync(CacheKeyDecks, JsonSerializer.Serialize(deckDtos), options);

            return deckDtos;
        }
    }
}
