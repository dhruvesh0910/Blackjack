/**
 * Author: Dhruvesh Hemantkumar Patel / 991580791
 * 
 * Deck.cs class generate 52 cards deck that needed to play BlackJack game.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Deck
    {
        // declaration of array of list object
        List<Card> cards = new List<Card>();
        // declaration and initialization of arrays and variables
        String[] suit = {"Diamonds","Hearts","Clubs","Spreads"};
        int[] value = {1,2,3,4,5,6,7,8,9,10,11,12,13};
        int index = 0;
        
        // Non-parameterize constructor
        public Deck()
        {
            //initialize deck
            InitDeck();
        }

        // Getter and setter method for card collection
        public List<Card> getCards()
        {
            return cards;
        }// end of method

        public void setCards(List<Card> cards)
        {
            this.cards = cards;
        }// end of method
       
        public void InitDeck()
        {
            // Quiz 2: Add the other cards
            // make 52 Card objects and put them into the cards arraylist
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    cards.Add(new Card(value[j], suit[i]));
                } // end of inner for loop
            }// end of outer for loop
            cards = shuffle(cards);
        } // end of method

        // shuffle the deck
        public List<Card> shuffle(List<Card> cards)
        {
            // Part of Assignment #1
            
            Random rand = new Random();
            for(int i = 0; i<1000; i++) { 
                int index1 = rand.Next(0, 52);
                int index2 = rand.Next(0, 52);
                Console.WriteLine();
                Card c = cards[index1];
                cards[index1] = cards[index2];
                cards[index2] = c;
            }// end of for loop
            return cards;
        }// end of method

        // print deck
        public void printDeck()
        {
            // show all the cards in the deck
            foreach(Card card in cards )
            {
                Console.WriteLine(card.getFullName());
            }// end of foreach loop
        }// end of method

        // distribute individual card to player and dealers
        public Card Deal()
        {
            // return the card at the top of the deck and remove it from the array list
            //int index = cards.First();
            if (cards.Count == 0)
            {
                InitDeck();
            }
            Card c = cards[index];
            cards.RemoveAt(index);
            
            return c;
        } // end of method
    }// end of class
}
