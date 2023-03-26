using WebDevAPI.Db.Dto_s.Card;
using WebDevAPI.Db.Dto_s.Player;

namespace WebDevAPI.Db.Dto_s.PokerTable
{
    public class GetPokerTableDto
    {
        public Guid Id { get; set; }
        public int Ante { get; set; }
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public int MaxSeats { get; set; }
        public ICollection<GetPlayerDto>? Players { get; set; }
        public ICollection<GetCardDto>? Cards { get; set; }
    }
}
