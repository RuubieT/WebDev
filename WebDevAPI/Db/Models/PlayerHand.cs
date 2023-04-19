using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebDevAPI.Db.Dto_s.PlayerHand;

namespace WebDevAPI.Db.Models
{
    public class PlayerHand
    {
        [Key] public Guid PlayerHandId { get; set; }


        public Guid? FirstCardId { get; set; }
        public Card FirstCard { get; set; }
        public Guid? SecondCardId { get; set; }
        public Card SecondCard { get; set; }


        public string PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public GetPlayerHandDto GetPlayerHandDto()
        {
            return new()
            {
                FirstCard = FirstCard.GetCardDto(),
                SecondCard = SecondCard.GetCardDto(),
            };
        }
    }
}
