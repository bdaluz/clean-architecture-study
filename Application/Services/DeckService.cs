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
    public class DeckService : IDeckService
    {
        private readonly IDeckRepository _repository;

        public DeckService(IDeckRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeckDto> CreateDeckAsync(CreateDeckDto dto)
        {
            var deck = new Deck(dto.Title, dto.Description);

            await _repository.AddAsync(deck);
            await _repository.SaveChangesAsync();

            return new DeckDto
            {
                Id = deck.Id,
                Title = deck.Title,
                Description = deck.Description
            };
        }

        public async Task<List<DeckDto>> GetAllDecksAsync()
        {
            var decks = await _repository.GetAllAsync();

            return decks.Select(d => new DeckDto
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description
            }).ToList();
        }
    }
}
