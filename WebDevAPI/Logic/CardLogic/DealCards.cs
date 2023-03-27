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

        public ICollection<PlayerHand> Deal(ICollection<Player> players, DeckOfCards deck)
        {
            var playerHands = new List<PlayerHand>();

            foreach(var p in players)
            { 
                playerHand[0] = deck.getDeck[0];
                deck.getDeck.ToList().RemoveAt(0);
                playerHand[1] = deck.getDeck[0];
                deck.getDeck.ToList().RemoveAt(0);

                Console.WriteLine(deck.getDeck.Length);

                playerHands.Add(new PlayerHand
                {
                    PlayerHandId = new Guid(),
                    Player = p,
                    PlayerId = p.Id,
                    Cards = playerHand,
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
