using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateCardDto
    {
        public Guid DeckId { get; set; }
        public string FrontText { get; set; }
        public string BackText { get; set; }
    }
}
