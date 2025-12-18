using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Deck : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        private readonly List<Card> _cards = new();
        public IReadOnlyCollection<Card> Cards => _cards.AsReadOnly();


        public Deck(string title, string description)
        {
            ValidateAndSetTitle(title);
            Description = description;
        }

        public void UpdateInfo(string title, string description)
        {
            ValidateAndSetTitle(title);
            Description = description;
            SetUpdatedDate();
        }

        public void AddCard(string frontText, string backText)
        {
            var card = new Card(frontText, backText, this.Id);
            _cards.Add(card);
            SetUpdatedDate();
        }

        private void ValidateAndSetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("O título do deck não pode ser vazio.");

            Title = title;
        }
    }
}
