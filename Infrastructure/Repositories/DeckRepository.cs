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
    public class DeckRepository : IDeckRepository
    {
        private readonly FlashcardsDbContext _context;

        public DeckRepository(FlashcardsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Deck deck)
        {
            await _context.Decks.AddAsync(deck);
        }

        public async Task<List<Deck>> GetAllAsync()
        {
            return await _context.Decks.AsNoTracking().ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
