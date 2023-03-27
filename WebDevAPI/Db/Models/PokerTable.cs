using WebDevAPI.Db.Dto_s.Card;
using WebDevAPI.Db.Dto_s.Player;
using WebDevAPI.Db.Dto_s.PokerTable;

namespace WebDevAPI.Db.Models
{
    public class PokerTable
    {
        public Guid PokerTableId { get; set; }
        public int Ante { get; set; }
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public int MaxSeats { get; set; }
        public virtual ICollection<Player>? Players { get; set; }
        public virtual ICollection<Card>? Cards { get; set; }

        public GetPokerTableDto GetPokerTableDto()
        {
            return new()
            {
                Id = PokerTableId,
                Ante = Ante,
                SmallBlind = SmallBlind,
                BigBlind = BigBlind,
                MaxSeats = MaxSeats,
                Players = ConvertAllPlayersToDto(Players),
                Cards = ConvertAllCardsToDto(Cards)
            };
        }

        public ICollection<GetPlayerDto> ConvertAllPlayersToDto(ICollection<Player> players)
        {
            ICollection<GetPlayerDto> allPlayers = new List<GetPlayerDto>();
            if (players.Count > 0)
            {
                foreach (Player p in players)
                {
                    allPlayers.Add(p.GetPlayerDto());
                }
            }

            return allPlayers;
        }

        public ICollection<GetCardDto> ConvertAllCardsToDto(ICollection<Card> cards)
        {
            ICollection<GetCardDto> allCards = new List<GetCardDto>();
            if (cards.Count > 0)
            {
                foreach (Card c in cards)
                {
                    allCards.Add(c.GetCardDto());
                }
            }

            return allCards;
        }

        public bool isFull()
        {
            if (MaxSeats == Players.Count) return true;
            else return false;
        }
    }
}
