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

        public static bool PlayerStillActive(PlayerModel player)
        {
            bool isActive = false;

            foreach (var ship in player.ShipLocations)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    isActive = true;
                }
            }

            return isActive;
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

        public static int GetShotCount(PlayerModel player)
        {
            int shotCount = 0;

            foreach (var shot in player.ShotGrid)
            {
                if (shot.Status != GridSpotStatus.Empty)
                {
                    shotCount += 1;
                }
            }

            return shotCount;
        }

        public static bool CheckGridInput(string location)
        {
            if (location.Length != 2)
            {
                return false;
                //throw new ArgumentException("This location does not exist.", "shot");
            }

            char[] coordinates = location.ToCharArray();
            bool isValidLocation = int.TryParse(coordinates[1].ToString(), out _);
            return isValidLocation;
        }

        public static bool PlaceShip(PlayerModel model, string location)
        {
            bool output = false;

            (string row, int column) = SplitShotIntoRowAndColumn(location);

            bool isValidLocation = ValidateGridLocation(model, row, column);
            bool isSpotOpen = ValidateShipLocation(model, row, column);

            if (isValidLocation && isSpotOpen)
            {
                model.ShipLocations.Add(new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship
                });

                output = true;
            }

            return output;
        }

        private static bool ValidateShipLocation(PlayerModel model, string row, int column)
        {
            bool isValidLocation = true;

            foreach (var ship in model.ShipLocations)
            {
                if (ship.SpotLetter == row && ship.SpotNumber == column)
                {
                    isValidLocation = false;
                }
            }

            return isValidLocation;
        }

        private static bool ValidateGridLocation(PlayerModel model, string row, int column)
        {
            bool isValidLocation = false;

            foreach (var spot in model.ShotGrid)
            {
                if (spot.SpotLetter == row && spot.SpotNumber == column)
                {
                    isValidLocation = true;
                }
            }

            return isValidLocation;
        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            string row;
            int column;

            char[] shotArray = shot.ToArray();

            row = shotArray[0].ToString();
            column = int.Parse(shotArray[1].ToString());

            return (row.ToUpper(), column);
        }

        public static bool ValidateShot(PlayerModel player, string row, int column)
        {
            bool isValidShot = false;

            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row && gridSpot.SpotNumber == column)
                {
                    if (gridSpot.Status == GridSpotStatus.Empty)
                    {
                        isValidShot = true;
                    }

                }
            }

            return isValidShot;
        }

        public static bool IdentifyShotResult(PlayerModel opponent, string row, int column)
        {
            bool isAHit = false;

            foreach (var ship in opponent.ShipLocations)
            {
                if (ship.SpotLetter == row && ship.SpotNumber == column)
                {
                    isAHit = true;
                    ship.Status = GridSpotStatus.Sunk;
                }
            }

            return isAHit;
        }

        public static void MarkShotResult(PlayerModel player, string row, int column, bool isAHit)
        {
            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row && gridSpot.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        gridSpot.Status = GridSpotStatus.Hit;
                        Console.WriteLine("It was a hit!");
                    }
                    else
                    {
                        gridSpot.Status = GridSpotStatus.Miss;
                        Console.WriteLine("It was a miss!");
                    }
                }
            }
        }
    }
}
