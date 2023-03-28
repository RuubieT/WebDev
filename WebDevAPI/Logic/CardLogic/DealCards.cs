using NuGet.Packaging;
using WebDevAPI.Db.Models;

namespace WebDevAPI.Logic.CardLogic
{
    public class DealCards
    {
        private Card[] playerHand;
        private Card[] sortedHand;
        private Card[] bordCards;

        public DealCards()
        {
            playerHand= new Card[2];
            sortedHand = new Card[5];
            bordCards = new Card[5];
        }

        public ICollection<PlayerHand> Deal(ICollection<Player> players, Queue<Card> deck)
        {
            var playerHands = new List<PlayerHand>();

            foreach(var p in players)
            {
                var firstCard = deck.Dequeue();
                var secondCard = deck.Dequeue();

                playerHands.Add(new PlayerHand
                {
                    PlayerId = p.Id,
                    FirstCard = firstCard,
                    FirstCardId = firstCard.CardId,
                    SecondCard = secondCard,
                    SecondCardId = secondCard.CardId
                });  
            }
            return playerHands;
            //GetHand();
            //SortCards();
            //EvaluateHands();
        }

     /*   public void GetHand()
        {
            for(int i =0; i< 2; i++)
            {
                playerHand[i] = deck.getDeck[i];
            }

        }
     */
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
