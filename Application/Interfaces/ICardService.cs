using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICardService
    {
        Task<CardDto> CreateCardAsync(CreateCardDto card);
        Task<List<CardDto>> GetAllCardsByDeckId(Guid deckId);
    }
}
