using WebDevAPI.Db.Models;
using static WebDevAPI.Db.Models.Card;

namespace WebDevAPI.Db.Dto_s.Card
{
    public class GetCardDto
    {
        public Guid Id { get; set; }
        public SUIT SUIT { get; set; }
        public VALUE VALUE { get; set; }
    }
}
