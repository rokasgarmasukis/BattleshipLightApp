using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary.Models;
using BattleshipLibrary;

namespace BattleshipLight
{
    public class Methods
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship Lite");
            Console.WriteLine("Created by Rokas Garmasukis");
            Console.WriteLine();
        }

        public static PlayerModel CreatePlayer(string playerTitle)
        {
            PlayerModel output = new PlayerModel();

            Console.WriteLine($"Player information for {playerTitle}.");

            // ask user for their name
            output.UsersName = AskForUsersName();

            // load up the shot grid
            GameLogic.InitializeGrid(output);
            DisplayShotGrid(output);

            // ask user for their 5 ship placements
            PlaceShips(output);

            // clear 
            Console.Clear();

            return output;

        }

        private static string AskForUsersName()
        {
            Console.Write("What is your name: ");
            string output = Console.ReadLine();
            return output;
        }

        private static void PlaceShips(PlayerModel model)
        {
            do
            {
                Console.Write($"Where do you want to place your ship number { model.ShipLocations.Count + 1 }: ");
                string location = Console.ReadLine();

                bool isValidInput = GameLogic.CheckGridInput(location);

                if (isValidInput == false)
                {
                    Console.WriteLine($"{location} does not exist. Please try again.");
                    continue;
                }

                bool isValidLocation = GameLogic.PlaceShip(model, location);

                if (isValidLocation == false)
                {
                    Console.WriteLine($"{location} was not a valid location. Please try again.");
                }

            } while (model.ShipLocations.Count < 5);
        }

        public static void DisplayShotGrid(PlayerModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            Console.WriteLine();
            Console.WriteLine("  1 2 3 4 5");
            Console.Write("A");

            foreach (var gridSpot in activePlayer.ShotGrid)
            {
                if (gridSpot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridSpot.SpotLetter;
                    Console.Write(currentRow);
                }

                if (gridSpot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($"  ");
                }
                else if (gridSpot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X");
                }
                else if (gridSpot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O");
                }
                else
                {
                    Console.Write(" ?");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        public static void RecordPlayerShot(PlayerModel activePlayer, PlayerModel opponent)
        {
            bool isValidShot = false;
            string row = "";
            int column = 0;

            do
            {
                string shot = AskForShot(activePlayer);
                (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);
                isValidShot = GameLogic.ValidateShot(activePlayer, row, column);

                if (isValidShot == false)
                {
                    Console.WriteLine("Invalid shot location. Please try again.");
                }

            } while (isValidShot == false);

            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
        }

        private static string AskForShot(PlayerModel activePlayer)
        {
            Console.Write($"{activePlayer.UsersName}, please enter your shot selection: ");
            string output = Console.ReadLine();

            return output;
        }

        public static void IdentifyWinner(PlayerModel winner)
        {
            Console.WriteLine($"Congratulations to {winner.UsersName} for winning!");
            Console.WriteLine($"{winner.UsersName} took {GameLogic.GetShotCount(winner)} shots");
        }
    }


}
