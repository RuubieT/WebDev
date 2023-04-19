using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using WebDevAPI.Db.Dto_s.Player;
using static WebDevAPI.Db.Models.Card;

namespace WebDevAPI.Db.Models
{
    public class Player : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [AllowNull]
        public string AuthCode { get; set; }
        public int Chips { get; set; }
        



        //Foreign key
        public Guid? PokerTableId { get; set; }
        public virtual PokerTable PokerTable { get; set; }
        public virtual PlayerHand PlayerHand { get; set; }

        public GetPlayerDto GetPlayerDto()
        {
            return new()
            {
                Id = Guid.Parse(Id),
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = UserName,
                Chips = Chips,
                PokerTableId = PokerTableId,

            };
        }

        public GetLeaderBoardDto GetLeaderBoardDto()
        {
            return new()
            {
                Username = UserName,
                Chips = Chips
            };
        }
    }
}
