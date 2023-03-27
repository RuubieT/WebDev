﻿using System.ComponentModel.DataAnnotations;
using WebDevAPI.Db.Dto_s.Player;
using static WebDevAPI.Db.Models.Card;

namespace WebDevAPI.Db.Models
{
    public class Player : User
    {
        public string Username { get; set; }
        public int Chips { get; set; }
        public PlayerHand? PlayerHand { get; set; }


        //Foreign key
        public Guid PokerTableId { get; set; }
        public virtual PokerTable? PokerTable { get; set; }

       
        public Player() 
        {
            Chips = 15000;
        }

        public GetPlayerDto GetPlayerDto()
        {
            return new()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PasswordHash = PasswordHash,
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
