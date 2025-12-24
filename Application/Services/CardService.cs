using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _repository;
        private readonly ICacheService _cacheService;

        public CardService(ICardRepository cardRepository, ICacheService cacheService)
        {
            _repository = cardRepository;
            _cacheService = cacheService;
        }

        public async Task<CardDto> CreateCardAsync(CreateCardDto dto)
        {
            var card = new Card(dto.FrontText, dto.BackText, dto.DeckId);
            var cacheKey = $"cards_deckKey_{dto.DeckId}";

            await _repository.AddAsync(card);
            await _repository.SaveChangesAsync();
            await _cacheService.RemoveAsync(cacheKey);

            return new CardDto
            {
                Id = card.Id,
                FrontText = dto.FrontText,
                BackText = dto.BackText,
                DeckId = dto.DeckId,
                NextReviewDate = card.NextReviewDate
            };
        }

        public async Task<List<CardDto>> GetAllCardsByDeckId(Guid deckId)
        {
            var cacheKey = $"cards_deckKey_{deckId}";
            var cachedCards = await _cacheService.GetAsync<List<CardDto>>(cacheKey);

            if (cachedCards != null) { 
                return cachedCards;
            }

            var cards = await _repository.GetByDeckIdAsync(deckId);

            var cardDtos = cards.Select(c => new CardDto
            {
                Id = c.Id,
                FrontText = c.FrontText,
                BackText = c.BackText,
                DeckId = deckId,
                NextReviewDate = c.NextReviewDate
            }).ToList();

            await _cacheService.SetAsync(cacheKey, cardDtos);

            return cardDtos;
        }
    }
}
