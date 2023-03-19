using WebDevAPI.Db.Models;

namespace WebDevAPI.Logic.CardLogic
{
    public class DealCards
    {
        private Card[] playerHand;
        private Card[] sortedHand;
        private Card[] bordCards;

        private DeckOfCards deck = new();

        public DealCards()
        {
            playerHand= new Card[2];
            sortedHand = new Card[5];
            bordCards = new Card[5];
        }

        public void Deal()
        {
            deck.SetUpDeck();
        }
    }
}
