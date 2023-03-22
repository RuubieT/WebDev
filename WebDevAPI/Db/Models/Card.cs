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
            TWO = 2, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT,
            NINE, TEN, JACK, QUEEN, KING, ACE
        }

        public Guid CardId { get; set; }
        public SUIT MySuit { get; set; }
        public VALUE MyValue { get; set; }

        //fk
        public virtual PlayerHand? PlayerHand { get; set; }
        public virtual PokerTable? PokerTable { get; set; }

        public GetCardDto GetCardDto()
        {
            return new()
            {
                Id = CardId,
                SUIT = MySuit,
                VALUE = MyValue
            };
        }
    }

 
}
