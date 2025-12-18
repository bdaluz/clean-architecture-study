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
        public CardService(ICardRepository cardRepository) { 
            _repository = cardRepository;        
        }

        public async Task<CardDto> CreateCardAsync(CreateCardDto dto)
        {
            var card = new Card(dto.FrontText, dto.BackText, dto.DeckId);

            await _repository.AddAsync(card);
            await _repository.SaveChangesAsync();

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
            var cards = await _repository.GetByDeckIdAsync(deckId);

            return cards.Select(c => new CardDto
            {
                Id = c.Id,
                FrontText = c.FrontText,
                BackText = c.BackText,
                DeckId = deckId,
                NextReviewDate = c.NextReviewDate
            }).ToList();
        }
    }
}
