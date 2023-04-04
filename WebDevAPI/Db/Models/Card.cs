using Microsoft.EntityFrameworkCore;
using WebDevAPI.Db.Dto_s.Card;

namespace WebDevAPI.Db.Models
{
    public class Card
    {
        public enum SUIT
        {
            HEARTS,
            SPADES,
            DIAMONDS,
            CLUBS
        }

        public enum VALUE
        {
            TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT,
            NINE, TEN, JACK, QUEEN, KING, ACE
        }

        public Guid CardId { get; set; }
        public SUIT MySuit { get; set; }
        public VALUE MyValue { get; set; }
        public bool InHand { get; set; }

        public GetCardDto GetCardDto()
        {
            return new()
            {
                SUIT = MySuit,
                VALUE = MyValue
            };
        }
    }

 
}
