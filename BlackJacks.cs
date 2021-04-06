/**
 * Author: Dhruvesh Hemantkumar Patel / 991580791
 * 
 * BlackJacks.cs class implement the actual logic for blackjack game.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class BlackJacks
    {
        // declaring and initializing the variable and Deck object
        Deck deck = new Deck();
        int playerTotal = 1000;
        int tableTotal = 0, flag = 0,pSum,dSum;

        // Getter and Setter method for playerTotal variable
        public int getPlayerTotal()
        {
            return playerTotal;
        } // end of method

        public void setPlayerTotal(int playerTotal)
        {
            this.playerTotal = playerTotal;
        } // end of method

        // Getter and Setter method for tableTotal variable
        public int getTableTotal()
        {
            return tableTotal;
        }// end of method

        public void setTableTotal(int tableTotal)
        {
            this.tableTotal = tableTotal;
        } // end of method

        // declaring and initializing hands array for both player and dealer
        List<Card> dealerCards = new List<Card>();
        List<Card> playerCards = new List<Card>();

        // Getter method for player's hand
        public List<Card> getPlayerCards()
        {
            return playerCards;
        } // end of method

        // Getter method for dealer's hand
        public List<Card> getDealerCards()
        {
            return dealerCards;
        } // end of method

        // distribute the card to dealer
        public void DealToDealer()
        {
            Card c = deck.Deal();
            dealerCards.Add(c);
        } // end of method

        // distribute the card to player
        public void DealToPlayer()
        {
            Card c = deck.Deal();
            playerCards.Add(c);
        } // end of method

        // method to calculate the sum of dealer's hand
        public int calcDealerSum()
        {
            // Part of Assignment 1
            dSum = 0;
            if (flag == 0 && dealerCards.Count > 0)
            {
                dSum += getCardValue(dealerCards[dealerCards.Count - 1],"Dealer");
            }
            else
            {
                foreach (Card card in dealerCards)
                {
                    dSum += getCardValue(card,"Dealer");
                } // end of foreach loop
            }
            return dSum;
        } // end of method

        // method to calculate the sum of player's hand
        public int calcPlayerSum()
        {
            // Part of Assignment 1
            pSum = 0;
            foreach (Card card in playerCards)
            {
                pSum += getCardValue(card,"Player");
            } // end of foreach loop
        
            return pSum;
        } // end of method

        // start the game
        public void Start()
        {
            flag = 0;
            Flush();

            DealToDealer();
            DealToPlayer();
            DealToDealer();
            DealToPlayer();
        } // end of method

        // flush or clears ths hands of both dealer and player
        public void Flush()
        {
            playerCards.Clear();
            dealerCards.Clear();
        } // end of method

        // implement the logic when player or dealer decide to hit and 
        // return true if player loose else return false
        public Boolean Hit()
        {
            DealToPlayer();
            if (calcPlayerSum() > 21)
            {
                flag = 0;
                return true;   
            }
            return false;
        } // end of method

        // implement the logic when player or dealer decide to stand
        // return 0 if dealer win and 1 if player win, 2 if Dealer bursted and 3 if Tie
        public int Stand() 
        {
            flag = 1;
            // remember the casino's Rule for dealer : Hit utill sum is less than 16
            while (calcDealerSum() < 16)
            {
                DealToDealer();
                
                if (calcDealerSum() > 21)
                {
                    return 2; // Player won (bc Dealer was busted)
                }
            } // end of while loop
            // If we reach this point, it means dealer sum is now
            // bigger than 16 but less than 22

            if(calcDealerSum() == calcPlayerSum())
            {
                return 3; // game tie
            }
            else if(calcDealerSum() > calcPlayerSum() && calcDealerSum() < 22)
            {
                return 0; // delaer won
            }
            else
            {
                return 1; // Player won   
            }
        } // end of method

        // return the corresponding the card value based on the rules of BlackJack
        public int getCardValue(Card c, String s)
        {
            // return blackjack value of c (from 1 to 10)
            if (c.Rank > 10)
                return 10;
            else
            {
                if(c.Rank == 1)
                {
                    if(pSum < 11 && s.Equals("Player"))
                    {
                        return 11;
                    }
                    else if (dSum < 11 && s.Equals("Dealer"))
                    {
                        return 11;
                    }
                    else
                    {
                        return 1;
                    }
                }
                return c.Rank;
            }
        }// end of the method
    } // end of the class
}
