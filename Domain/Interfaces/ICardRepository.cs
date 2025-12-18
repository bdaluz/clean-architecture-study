using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICardRepository
    {
        Task AddAsync(Card card);
        Task<List<Card>> GetByDeckIdAsync(Guid deckId);
        Task SaveChangesAsync();
    }
}
