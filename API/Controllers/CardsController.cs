using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/decks/{deckId}/cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid deckId, [FromBody] CreateCardDto dto)
        {

            dto.DeckId = deckId;

            var result = await _cardService.CreateCardAsync(dto);
            return CreatedAtAction(nameof(GetAllByDeck), new { deckId = deckId, cardId = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByDeck(Guid deckId)
        {
            var result = await _cardService.GetAllCardsByDeckId(deckId);
            return Ok(result);
        }
    }
}
