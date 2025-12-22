using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _repository;
        private readonly ICacheService _cacheService;

        private const string CacheKeyDecks = "decks_list";

        public DeckService(IDeckRepository repository, ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<DeckDto> CreateDeckAsync(CreateDeckDto dto)
        {
            var deck = new Deck(dto.Title, dto.Description);

            await _repository.AddAsync(deck);
            await _repository.SaveChangesAsync();

            await _cacheService.RemoveAsync(CacheKeyDecks);

            return new DeckDto
            {
                Id = deck.Id,
                Title = deck.Title,
                Description = deck.Description
            };
        }

        public async Task<List<DeckDto>> GetAllDecksAsync()
        {
            var cachedDecks = await _cacheService.GetAsync<List<DeckDto>>(CacheKeyDecks);

            if (cachedDecks != null)
            {
                return cachedDecks;
            }

            var decks = await _repository.GetAllAsync();

            var deckDtos = decks.Select(d => new DeckDto
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description
            }).ToList();

            await _cacheService.SetAsync(CacheKeyDecks, deckDtos);

            return deckDtos;
        }
    }
}
