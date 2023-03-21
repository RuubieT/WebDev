﻿using WebDevAPI.Db.Dto_s.Player;

namespace WebDevAPI.Db.Dto_s.PokerTable
{
    public class GetPokerTableDto
    {
        public Guid Id { get; set; }
        public int Ante { get; set; }
        public int SmallBlind { get; set; }
        public int BigBlind { get; set; }
        public int MaxSeats { get; set; }
    }
}
