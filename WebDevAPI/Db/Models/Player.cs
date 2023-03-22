﻿using WebDevAPI.Db.Dto_s.Player;
using static WebDevAPI.Db.Models.Card;

namespace WebDevAPI.Db.Models
{
    public class Player : User
    {
        public string Username { get; set; }
        public int Chips { get; set; }
        
        //Foreign key
        public Guid PokerTableId { get; set; }
        public virtual PokerTable? PokerTable { get; set; }
        public virtual PlayerHand? PlayerHand { get; set; }
       
        public Player() 
        {
        }

        public Player(User user, string username, int chips) 
            : base (user.Id, user.FirstName, user.LastName, user.Description, user.Email, user.Password)
        {
            Username = username;
            Chips = chips;
        }

        public GetPlayerDto GetPlayerDto()
        {
            return new()
            {
                Username = Username,
                Chips = Chips
            };
        }

        public GetLeaderBoardDto GetLeaderBoardDto()
        {
            return new()
            {
                Username = Username,
                Chips = Chips
            };
        }
    }
}
