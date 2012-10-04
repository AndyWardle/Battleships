namespace Battleships
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Board
    {
        public static readonly char[] ValidCharacters = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        public static readonly int[] ValidNumbers = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private readonly Cell[,] grid;
        private readonly IList<Ship> placedShips;

        public Board()
        {
            this.grid = new Cell[ValidCharacters.Length, ValidNumbers.Length];
            this.placedShips = new List<Ship>();

            for (var i = 0; i < ValidCharacters.Length; i++)
            {
                for (var j = 0; j < ValidNumbers.Length; j++)
                {
                    grid[i, j] = new Cell(ValidCharacters[i], ValidNumbers[j]);
                }
            }
        }

        public void Initialize(Player player, Player computer)
        {
            this.PlaceShip(Ship.CreateDestroyer(player, computer));
            this.PlaceShip(Ship.CreateDestroyer(player, computer));
            this.PlaceShip(Ship.CreateBattleShip(player, computer));

            this.PlaceShip(Ship.CreateDestroyer(computer, player));
            this.PlaceShip(Ship.CreateDestroyer(computer, player));
            this.PlaceShip(Ship.CreateBattleShip(computer, player));
        }

        private void PlaceShip(Ship ship)
        {
            do
            {
                var direction = (Direction)new Random().Next(0, 2);
                var randomHorizontal = new Random().Next(0, ValidCharacters.Length - 1);
                var randomVertical = new Random().Next(0, ValidNumbers.Length - 1);

                if (direction == Direction.Horizontal && randomHorizontal + ship.Size <= ValidCharacters.Length)
                {
                    if (ShipPlaced(ship, () => TryPlaceShipHorizontally(ship, randomVertical, randomHorizontal)))
                    {
                        return;
                    }
                }

                if (direction == Direction.Vertical && randomVertical + ship.Size <= ValidNumbers.Length)
                {
                    if (ShipPlaced(ship, () => TryPlaceShipVertically(ship, randomVertical, randomHorizontal)))
                    {
                        return;
                    }
                }
            } while (true);
        }

        private bool ShipPlaced(Ship ship, Func<IList<Cell>> shipPlacingAction)
        {
            var shipCells = shipPlacingAction();

            if (shipCells != null)
            {
                ship.SetPlace(shipCells);
                this.placedShips.Add(ship);

                return true;
            }

            return false;
        }

        private IList<Cell> TryPlaceShipVertically(Ship ship, int randomVertical, int randomHorizontal)
        {
            var shipCells = new List<Cell>();
            for (var i = randomVertical; i < randomVertical + ship.Size; i++)
            {
                var cell = grid[i, randomHorizontal];
                if (this.placedShips.Any(s => s.Occupies(cell)))
                {
                    return null;
                }

                shipCells.Add(cell);
            }

            return shipCells;
        }

        private IList<Cell> TryPlaceShipHorizontally(Ship ship, int randomVertical, int randomHorizontal)
        {
            var shipCells = new List<Cell>();
            for (var i = randomHorizontal; i < randomHorizontal + ship.Size; i++)
            {
                var cell = grid[randomVertical, i];
                if (this.placedShips.Any(s => s.Occupies(cell)))
                {
                    return null;
                }

                shipCells.Add(cell);
            }

            return shipCells;
        }
    }
}
