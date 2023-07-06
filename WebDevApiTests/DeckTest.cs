using Moq;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic.CardLogic;

namespace WebDevApiTests
{
    public class DeckTest
    {
        private DeckOfCards deck;
        private Queue<Card> deckQueue;

        [SetUp]
        public void Setup()
        {
            deck = new DeckOfCards();
            deck.SetUpDeck();
            deckQueue = deck.getDeck;
            deckQueue = new Queue<Card>();
        }

        [Test]
        public void Deck_Is_Not_Empty()
        {
            Assert.AreNotEqual(0, deck.getDeck.Count);

        }

        [Test]
        public void SetupDeck_Is_Shuffled()
        {
            deck.SetUpDeck();
            Assert.That(deck.getDeck, Is.Not.EqualTo(deckQueue));
        }

        [Test]
        public void EmptyDeck_Clears_Deck()
        {
            deck.EmptyDeck();
            Assert.IsTrue(deck.getDeck.Count == 0);
        }
    }
}