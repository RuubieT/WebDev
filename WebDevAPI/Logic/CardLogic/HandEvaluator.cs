using WebDevAPI.Db.Models;
using static WebDevAPI.Db.Models.Card;

namespace WebDevAPI.Logic.CardLogic
{
    public enum Hand
    {
        Nothing,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FourOfAKind,
        Straight,
        Flush,
        FullHouse,
    }

    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }

    public class HandEvaluator : Card
    {
        private int heartSum;
        private int diamondSum;
        private int clubSum;
        private int spadesSum;
        private Card[] _cards;
        private HandValue _handValue;

        private int firstCard;
        private int secondCard;
        private int thirdCard;
        private int fourthCard;
        private int fifthCard;

        public HandEvaluator(Card[] sortedHand)
        {
            heartSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadesSum = 0;
            _cards = new Card[5];
            Cards = sortedHand;
            _handValue = new HandValue();

            firstCard = (int)_cards[0].MyValue;
            secondCard = (int)_cards[1].MyValue;
            thirdCard = (int)_cards[2].MyValue;
            fourthCard = (int)_cards[3].MyValue;
            fifthCard = (int)_cards[4].MyValue;
        }

        public HandValue HandValue
        {
            get { return _handValue; }
            set { _handValue = value; }
        }

        public Card[] Cards
        {
            get { return _cards; }
            set
            {
                _cards[0] = value[0];
                _cards[1] = value[1];
                _cards[2] = value[2];
                _cards[3] = value[3];
                _cards[4] = value[4];
            }
        }

        public Hand EvaluateHand()
        {
            getNumberOfSuit();
            if (FourOfAKind())
                return Hand.FourOfAKind;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flush())
                return Hand.Flush;
            else if (Straight())
                return Hand.Straight;
            else if (ThreeOfAKind())
                return Hand.ThreeOfAKind;
            else if (TwoPair())
                return Hand.TwoPair;
            else if (OnePair())
                return Hand.OnePair;

            _handValue.HighCard = fifthCard;
            return Hand.Nothing;
        }

        private void getNumberOfSuit()
        {
            foreach(var e in Cards)
            {
                if (e.MySuit == Card.SUIT.HEARTS)
                    heartSum++;
                else if (e.MySuit == Card.SUIT.DIAMONDS)
                    diamondSum++;
                else if (e.MySuit == Card.SUIT.CLUBS)
                    clubSum++;
                else if (e.MySuit == Card.SUIT.SPADES)
                    spadesSum++;
            }
        }

        // #ToDo optimize?
        private bool FourOfAKind()
        {
            if (firstCard == secondCard && firstCard == thirdCard && firstCard == fourthCard)
            {
                _handValue.Total = secondCard * 4;
                _handValue.HighCard = (int)fifthCard;
                return true;
            }
            else if (secondCard == thirdCard && secondCard == fourthCard && secondCard == fifthCard)
            {
                _handValue.Total = secondCard * 4;
                _handValue.HighCard = firstCard;
                return true;
            }

            return false;
        }

        private bool FullHouse()
        {
            if (firstCard == secondCard && firstCard == thirdCard && fourthCard == fifthCard ||
                firstCard == secondCard && thirdCard == fourthCard && thirdCard == fifthCard)
            {
                _handValue.Total = (int)(firstCard) + (int)(secondCard) + (int)(thirdCard) +
                    (int)(fourthCard) + (int)(fifthCard);
                return true;
            }

            return false;
        }

        private bool Flush()
        {
            if (heartSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
            {
                _handValue.Total = (int)fifthCard;
                return true;
            }
            return false;
        }

        private bool Straight()
        {
            if (firstCard + 1 == secondCard &&
                secondCard + 1 == thirdCard &&
                thirdCard + 1 == fourthCard &&
                fourthCard + 1 == fifthCard)
            {
                _handValue.Total = (int)fifthCard;
                return true;
            }

            return false;
        }

        private bool ThreeOfAKind()
        {
            if ((firstCard == secondCard && firstCard == thirdCard) ||
                (secondCard == thirdCard && secondCard == fourthCard))
            {
                _handValue.Total = (int)thirdCard * 3;
                _handValue.HighCard = (int)fifthCard;
                return true;
            }
            else if (thirdCard == fourthCard && fourthCard == fifthCard)
            {
                _handValue.Total = (int)thirdCard * 3;
                _handValue.HighCard = secondCard;
                return true;
            }

            return false;
        }

        private bool TwoPair()
        {
            if(firstCard == secondCard && thirdCard == fourthCard)
            {
                _handValue.Total = (secondCard * 2) + (fourthCard * 2);
                _handValue.HighCard = fifthCard;
                return true;
            }
            else if(firstCard == secondCard && fourthCard == fifthCard)
            {
                _handValue.Total = (secondCard * 2) + (fourthCard * 2);
                _handValue.HighCard = thirdCard;
                return true;
            }
            else if (secondCard == thirdCard && fourthCard == fifthCard)
            {
                _handValue.Total = (secondCard * 2) + (fourthCard * 2);
                _handValue.HighCard = firstCard;
                return true;
            }

            return false;
        }

        private bool OnePair()
        {
            if(firstCard == secondCard)
            {
                _handValue.Total = firstCard * 2;
                _handValue.HighCard = fifthCard;
                return true;
            }
            if (secondCard == thirdCard)
            {
                _handValue.Total = secondCard * 2;
                _handValue.HighCard = fifthCard;
                return true;
            }
            if (thirdCard == fourthCard)
            {
                _handValue.Total = thirdCard * 2;
                _handValue.HighCard = fifthCard;
                return true;
            }
            if (fourthCard == fifthCard)
            {
                _handValue.Total = fourthCard * 2;
                _handValue.HighCard = thirdCard;
                return true;
            }

            return false;
        }

    }
}
