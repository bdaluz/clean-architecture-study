using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDeckRepository
    {
        Task AddAsync(Deck deck);
        Task<List<Deck>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
