using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public Guid DeckId { get; set; }
        public string FrontText { get; set; }
        public string BackText { get; set; }
        public DateTime NextReviewDate { get; set; }
    }
}
