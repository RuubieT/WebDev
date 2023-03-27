using WebDevAPI.Db.Models;
using static WebDevAPI.Db.Models.Card;

namespace WebDevAPI.Logic.CardLogic
{
    public class DeckOfCards
    {
        const int NUM_OF_CARDS = 52;
        private Queue<Card> deck;
        private Card[] shuffleDeck;

        public DeckOfCards()
        {
            deck = new Queue<Card>();
            shuffleDeck = new Card[NUM_OF_CARDS];
        }

        public Queue<Card> getDeck { get { return deck; } }

        public void SetUpDeck()
        {
            int i = 0;
            foreach (SUIT s in Enum.GetValues(typeof(SUIT)))
            {
                foreach (VALUE v in Enum.GetValues(typeof(VALUE)))
                {
                    shuffleDeck[i] = new Card { MySuit = s, MyValue = v };
                    i++;
                }
            }

            ShuffleCards();
        }

        public void ShuffleCards()
        {
            Random rand = new Random();
            Card temp;

            for (int shuffleTimes = 0; shuffleTimes < 1000; shuffleTimes++)
            {
                for (int i = 0; i < NUM_OF_CARDS; i++)
                {
                    int secondCardIndex = rand.Next(13);
                    temp = shuffleDeck[i];
                    shuffleDeck[i] = shuffleDeck[secondCardIndex];
                    shuffleDeck[secondCardIndex] = temp;
                }
            }

            foreach (var card in shuffleDeck)
            {
                deck.Enqueue(card);
            }
        }

    }
}
