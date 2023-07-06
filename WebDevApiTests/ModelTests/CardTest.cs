using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;

namespace WebDevApiTests.ModelTests
{
    public class CardTest
    {
        public Card Card;

        [SetUp]
        public void Setup()
        {
            Card = new Card{CardId = Guid.NewGuid(), InHand = false,MySuit = Card.SUIT.CLUBS, MyValue = Card.VALUE.EIGHT};
        }

        [Test]
        public void CreateCard()
        {
            Assert.IsNotNull(Card);
            Assert.That(Card.InHand, Is.EqualTo(false));
            Assert.That(Card.MySuit, Is.EqualTo(Card.SUIT.CLUBS));
        }

        [Test]
        public void CardGetCardDto()
        {
            var dto = Card.GetCardDto();
            Assert.IsNotNull(dto);
            Assert.That(dto.SUIT, Is.EqualTo(Card.MySuit));
        }
    }
}
