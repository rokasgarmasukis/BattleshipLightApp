using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    public static class GameLogic
    {
        public static void InitializeGrid(PlayerModel model)
        {
            List<string> letters = new List<string>
            {
                "A", "B", "C", "D", "E",
            };

            List<int> numbers = new List<int>
            {
                1, 2, 3, 4, 5,
            };

            foreach (string letter in letters)
            {
                foreach (int number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }
            }
        }

        private static void AddGridSpot(PlayerModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty,
            };

            model.ShotGrid.Add(spot);
        }

        public static bool PlaceShip(PlayerModel model, string location)
        {
            throw new NotImplementedException();
        }
    }
}
