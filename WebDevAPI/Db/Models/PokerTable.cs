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
        //Things as Ingame, seat palcement? Dealer etc....

        public GetPokerTableDto GetPokerTableDto()
        {
            return new()
            {
                Id = PokerTableId,
                Ante = Ante,
                SmallBlind = SmallBlind,
                BigBlind = BigBlind,
                MaxSeats = MaxSeats,

            };
        }
        /*
        public ICollection<GetPlayerDto> ConvertAllPlayersToDto(ICollection<Player> players)
        {
            ICollection<GetPlayerDto> allPlayers = new List<GetPlayerDto>();
            if (players != null)
            {
                if (players.Count > 0)
                {
                    foreach (Player p in players)
                    {
                        allPlayers.Add(p.GetPlayerDto());
                    }
                }
            }
            
            return allPlayers;
        }

        public bool isFull()
        {
            if (MaxSeats == Players.Count) return true;
            else return false;
        }}*/
    }
}
