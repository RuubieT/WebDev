using WebDevAPI.Db.Dto_s.Card;

namespace WebDevAPI.Db.Dto_s.PlayerHand
{
    public class GetPlayerHandDto
    {
        public GetCardDto FirstCard { get; set; }
        public GetCardDto SecondCard { get; set; }
    }
}
