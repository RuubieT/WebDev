using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDevAPI.Db.Models;

namespace WebDevApiTests.ModelTests
{
    public class PokertableTest
    {
        public PokerTable PokerTable;

        [SetUp]
        public void Setup()
        {
            PokerTable = new PokerTable
            {
                Ante = 0,
                BigBlind = 0,
                MaxSeats = 0,
                PokerTableId = Guid.NewGuid(),
                SmallBlind = 0
            };
        }

        [Test]
        public void CreatePokertable()
        {
            Assert.IsNotNull(PokerTable);
            Assert.That(PokerTable.Ante, Is.EqualTo(0));
            Assert.That(PokerTable.BigBlind, Is.EqualTo(0));
            Assert.That(PokerTable.SmallBlind, Is.EqualTo(0));
        }

        [Test]
        public void PokertableGetPokertableDto()
        {
            var dto = PokerTable.GetPokerTableDto();
            Assert.IsNotNull(dto);
            Assert.That(dto.Ante, Is.EqualTo(PokerTable.Ante));
            Assert.That(dto.SmallBlind, Is.EqualTo(PokerTable.SmallBlind));
        }
    }
}
