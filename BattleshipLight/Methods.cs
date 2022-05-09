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
                Console.WriteLine($"Where do you want to place your ship number { model.ShipLocations.Count + 1 } ");
                string location = Console.ReadLine();

                bool isValidLocation = GameLogic.PlaceShip(model, location);

                if (isValidLocation == false)
                {
                    Console.WriteLine($"{location} was not a valid location. Please try again.");
                }

            } while (model.ShipLocations.Count < 5);
        }
    }


}
