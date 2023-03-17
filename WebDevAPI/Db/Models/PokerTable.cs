namespace WebDevAPI.Db.Models
{
    public class PokerTable
    {
        public Guid Id { get; set; }
        public int Ante { get; set; }
        public int Dealer { get; set; }
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public int MaxSeats { get; set; }
        public List<Player> Players { get; set; }

    }
}
