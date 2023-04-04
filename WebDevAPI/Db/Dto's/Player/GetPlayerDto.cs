using WebDevAPI.Db.Dto_s.PokerTable;
using WebDevAPI.Db.Dto_s.User;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Db.Dto_s.Player
{
    public class GetPlayerDto: GetUserDto
    {
        public string Username { get; set; }
        public int Chips { get; set; }
        public Guid? PokerTableId { get; set; }
    }
}
