using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly FlashcardsDbContext _context;

        public CardRepository(FlashcardsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Card card)
        {
            await _context.Cards.AddAsync(card);
        }

        public Task<List<Card>> GetByDeckIdAsync(Guid deckId)
        {
            return _context.Cards
                .AsNoTracking()
                .Where(x => x.DeckId == deckId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
