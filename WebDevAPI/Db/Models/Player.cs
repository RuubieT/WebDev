namespace WebDevAPI.Db.Models
{
    public class Player : User
    {
        public string Username { get; set; }
        public int Chips { get; set; }
        public int Seat { get; set; }
        
        //Foreign key
        public int PokerTableId { get; set; }
        public PokerTable PokerTable { get; set; }
       
    }
}
