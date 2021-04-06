/**
 * Author: Dhruvesh Hemantkumar Patel / 991580791
 * 
 * Card.cs class create individual card with their value and face.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Card
    {
        // declaration of variables
        public int Rank; // 1,2,..,10,11,12,13 (where 11 means J, 12 means Q, 13 means K)
        public String Suit; // Hearts, Diamond, Spreads, Clubs

        // Parameterized constructor
        public Card(int rank, String suit)
        {
            Rank = rank;
            Suit = suit;
        } 

        // return the name of card
        public String getFullName()
        {
            // Ace of Hearts"
            String name ="";
           
            switch(Rank)
            {
                case 11:
                    name = "Jack";
                    break;

                case 12:
                    name = "Queen";
                    break;

                case 13:
                    name = "King";
                    break;

                case 1:
                    name = "Ace";
                    break;

                default: name = Rank.ToString();
                    break;
            }
            return name + "_of_" + Suit;
        } // end of method

        // return the equivalent image name
        public String getCorrespondingImageName()
        {
            //Quiz 2
            // ace_of_hearts.jpg for example
            int temp = Rank;
            String s = Suit.Substring(0, 1);
            return temp.ToString()+s+".jpg";
        } // end of method
    }// end of class
}
