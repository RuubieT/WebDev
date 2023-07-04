using Moq;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories.Contract;
using WebDevAPI.Logic.CardLogic;

namespace WebDevApiTests
{
    public class DeckTests
    {
        public DeckOfCards deck;
        public Queue<Card> deckQueue;

        [SetUp]
        public void Setup()
        {
            deck = new DeckOfCards();
            deck.SetUpDeck();
            deckQueue = deck.getDeck;
            deckQueue = new Queue<Card>();
        }

        [Test]
        public void Deck_Is_Not_Null()
        {
            Assert.IsNotNull(deck.getDeck);

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