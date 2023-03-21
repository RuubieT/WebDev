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
            GetHand();
            SortCards();;
            EvaluateHands();
        }

        public void GetHand()
        {
            for(int i =0; i< 2; i++)
            {
                playerHand[i] = deck.getDeck[i];
            }

        }

        public void SortCards()
        {
            var queryPlayer = from hand in playerHand
                              orderby hand.MyValue
                              select hand;

            var index = 0;
            foreach(var element in queryPlayer.ToList())
            {
                sortedHand[index] = element;
                index++;
            }
        }

        public void EvaluateHands()
        {
            HandEvaluator playerHandEvaluator = new HandEvaluator(sortedHand);

            Hand playerHand = playerHandEvaluator.EvaluateHand();
            Console.WriteLine(playerHand);
        }
    }
}
