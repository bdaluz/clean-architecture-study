using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Card : BaseEntity
    {
        public string FrontText { get; private set; }
        public string BackText { get; private set; }
        public DateTime NextReviewDate { get; private set; }
        public Guid DeckId { get; private set; }
        public Deck Deck { get; set; }

        public Card(string frontText, string backText, Guid deckId)
        {
            if (string.IsNullOrWhiteSpace(frontText)) throw new ArgumentException("Invalid card front text.");
            if (string.IsNullOrWhiteSpace(backText)) throw new ArgumentException("Invalid card back text.");

            FrontText = frontText;
            BackText = backText;
            DeckId = deckId;
            NextReviewDate = DateTime.UtcNow;
        }

        public void UpdateContent(string front, string back)
        {
            FrontText = front;
            BackText = back;
            SetUpdatedDate();
        }

        public void ProcessReview(bool rememberedCorrectly)
        {
            if (rememberedCorrectly)
            {
                NextReviewDate = DateTime.UtcNow.AddDays(1);
            }
            else
            {
                NextReviewDate = DateTime.UtcNow;
            }
            SetUpdatedDate();
        }
    }
}
