/**
 * Author: Dhruvesh Hemantkumar Patel / 991580791
 * Date started: January 30, 2021
 * Date end: February 11, 2021
 * Date revised and fragmented: February 17, 2021
 * 
 * MainWindow.xaml.cs class contain actual logic behind the GUI of BlackJack
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mw;
        BlackJacks game;
        int flag = 0;
        
        public MainWindow()
        {
            InitializeComponent();
         }

        // click event for start button
        private void Button_Start(object sender, RoutedEventArgs e)
        {

            game = new BlackJacks();
            game.getDealerCards().Clear();
            game.getPlayerCards().Clear();

            TableSetup();
            game.setPlayerTotal(game.getPlayerTotal() - 100);
            playerTotal.Content = "Player Bank : $" + game.getPlayerTotal().ToString();

            game.setTableTotal(game.getTableTotal() + 100);
            tableTotal.Content = "$" + game.getTableTotal().ToString();

            game.Start();
            Repaint();

        }

        // click event for hit button
        private void Button_Hit(object sender, RoutedEventArgs e)
        {
            Boolean hitResult = game.Hit();
            Repaint();
            if(hitResult)
            {
                MessageBox.Show("Busted! Dealer Won");
                game.getDealerCards().Clear();
                game.getPlayerCards().Clear();
                game.setTableTotal(0);
            }
            player.Content = game.calcPlayerSum().ToString();
            
            Repaint();
        }

        // click event for stand button
        private void Button_Stand(object sender, RoutedEventArgs e)
        {
            // Part of Assignment 1
            flag = 1;
            Repaint();
            
            int stand = game.Stand();
           
            Repaint();
            dealer.Content = game.calcDealerSum().ToString();
            if (stand == 0)
            {
                MessageBox.Show("Dealer Won");
                game.setTableTotal(0);
            }
            else if(stand == 2)
            {
                MessageBox.Show("Busted! Player Won");
                game.setPlayerTotal(game.getPlayerTotal() + (2 * game.getTableTotal()));
                game.setTableTotal(0);
            }
            else if (stand == 3)
            {
                MessageBox.Show("Tie! No win");
                game.setPlayerTotal(game.getPlayerTotal() + (game.getTableTotal()));
                game.setTableTotal(0);
            }
            else
            {
                MessageBox.Show("Player Won");
                game.setPlayerTotal(game.getPlayerTotal() + (2 * game.getTableTotal()));
                game.setTableTotal(0);
            }
            game.getDealerCards().Clear();
            game.getPlayerCards().Clear();
            Repaint();

        }

        // method for setting up table view with both dealer and players hands and summation of face value of card
        private void Repaint()
        {
            pane1.Children.Clear();
            pane2.Children.Clear();
            int playerSize = 1;
            int dealerSize = 1;

            if (game.getPlayerCards().Count() > 0 && game.getDealerCards().Count() > 0)
            {
                playerSize = 290 / game.getPlayerCards().Count();
                dealerSize = 290 / game.getDealerCards().Count();
            }

            // Part of Assignment 1

            foreach (Card c in game.getPlayerCards())
            {
                String path = "cards/" + c.getCorrespondingImageName();

                var bitmap = new BitmapImage(new Uri(path, UriKind.Relative));
                var image = new Image();
                image.Source = bitmap;
                image.Width = playerSize;
                pane1.Children.Add(image);
            }// end of foreach loop

            if (flag == 0 && game.getDealerCards().Count > 0)
            {
                var bitmap = new BitmapImage(new Uri("cards/Gray_back.jpg", UriKind.Relative));
                var image = new Image();
                image.Source = bitmap;
                image.Width = dealerSize;
                pane2.Children.Add(image);

                String path = "cards/" + game.getDealerCards()[(game.getDealerCards().Count) - 1].getCorrespondingImageName();
                var bitmap1 = new BitmapImage(new Uri(path, UriKind.Relative));
                var image1 = new Image();
                image1.Source = bitmap1;
                image1.Width = dealerSize;
                pane2.Children.Add(image1);

            }

            else
            {
                foreach (Card c in game.getDealerCards())
                {
                    String path = "cards/" + c.getCorrespondingImageName();
                    //Console.WriteLine(path);
                    var bitmap = new BitmapImage(new Uri(path, UriKind.Relative));
                    var image = new Image();
                    image.Source = bitmap;
                    image.Width = dealerSize;
                    pane2.Children.Add(image);
                } // end of foreach loop
            }
            playerTotal.Content = "Player Bank : $" + game.getPlayerTotal().ToString();


            tableTotal.Content = "$" + game.getTableTotal().ToString();
            dealer.Content = game.calcDealerSum().ToString();
            player.Content = game.calcPlayerSum().ToString();

        }// end of method

        // click event for reset button
        private void Button_Reset(object sender, RoutedEventArgs e)
        {
            flag = 0;
            ResetTable();
        }

        // click event for play again button
        private void Button_Again(object sender, RoutedEventArgs e)
        {
            flag = 0;
            game.getDealerCards().Clear();
            game.getPlayerCards().Clear();

            TableSetup();
            game.setPlayerTotal(game.getPlayerTotal() - 100);
            playerTotal.Content = "Player Bank : $" + game.getPlayerTotal().ToString();

            game.setTableTotal(game.getTableTotal() + 100);
            tableTotal.Content = "$" + game.getTableTotal().ToString();

            game.Start();
            Repaint();
        }

        // method to setup table element's visibility when game starts
        public void TableSetup()
        {
            startImage.Visibility = Visibility.Hidden;

            playAgain.Visibility = Visibility.Visible;
            reset.Visibility = Visibility.Visible;

            tableTotal.Visibility = Visibility.Visible;
            tableImage.Visibility = Visibility.Visible;
            playerTotal.Visibility = Visibility.Visible;
            

            player.Visibility = Visibility.Visible;
            playerlabel.Visibility = Visibility.Visible;
            dealer.Visibility = Visibility.Visible;
            dealerlabel.Visibility = Visibility.Visible;

            playerName.Visibility = Visibility.Visible;
            dealerName.Visibility = Visibility.Visible;

            pane1.Visibility = Visibility.Visible;
            pane2.Visibility = Visibility.Visible;

            stand.Visibility = Visibility.Visible;
            stand.IsEnabled = true;
            hit.Visibility = Visibility.Visible;
            hit.IsEnabled = true;

            start.Visibility = Visibility.Hidden;
            start.IsEnabled = false;
        } // end of method

        // method to setup table element's visibility when game terminate
        public void ResetTable()
        {
            startImage.Visibility = Visibility.Visible;

            playAgain.Visibility = Visibility.Hidden;
            reset.Visibility = Visibility.Hidden;

            tableTotal.Visibility = Visibility.Hidden;
            tableImage.Visibility = Visibility.Hidden;
            playerTotal.Visibility = Visibility.Hidden;
           

            player.Visibility = Visibility.Hidden;
            playerlabel.Visibility = Visibility.Hidden;
            dealer.Visibility = Visibility.Hidden;
            dealerlabel.Visibility = Visibility.Hidden;

            playerName.Visibility = Visibility.Hidden;
            dealerName.Visibility = Visibility.Hidden;

            pane1.Visibility = Visibility.Hidden;
            pane2.Visibility = Visibility.Hidden;

            stand.Visibility = Visibility.Hidden;
            stand.IsEnabled = false;
            hit.Visibility = Visibility.Hidden;
            hit.IsEnabled = false;

            start.Visibility = Visibility.Visible;
            start.IsEnabled = true;
        } // end of method
    }
}
