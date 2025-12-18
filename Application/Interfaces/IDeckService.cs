using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDeckService
    {
        Task<DeckDto> CreateDeckAsync(CreateDeckDto dto);
        Task<List<DeckDto>> GetAllDecksAsync();
    }
}
