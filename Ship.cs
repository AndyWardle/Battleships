namespace Battleships
{
    using System;
    using System.Collections.Generic;

    public class Ship
    {
        private IList<Cell> cells;
        private readonly IList<Cell> hitCells; 

        private Ship(ShipType type, Player owner)
        {
            this.ShipType = type;
            this.Owner = owner;
            this.hitCells = new List<Cell>();
        }

        public static Ship CreateDestroyer(Player owner)
        {
            var ship = new Ship(ShipType.Destroyer, owner);
            owner.AddShip(ship);

            return ship;
        }

        public static Ship CreateBattleShip(Player owner)
        {
            var ship = new Ship(ShipType.Battleship, owner);
            owner.AddShip(ship);

            return ship;
        }

        public int Size
        {
            get { return (int) ShipType; }
        }

        public ShipType ShipType { get; private set; }

        public Player Owner { get; private set;  }

        public bool Occupies(Cell cell)
        {
            return this.cells.Contains(cell);
        }

        public void SetPlace(IList<Cell> cells)
        {
            this.cells = cells;
        }

        public bool IsHit(Cell shot)
        {
            if (cells == null)
            {
                throw new InvalidOperationException("Ship has not yet been placed on the board.");
            }

            if (cells.Contains(shot))
            {
                this.hitCells.Add(shot);

                return true;
            }

            return false;
        }

        public bool IsPlaced()
        {
            return this.cells == null;
        }

        public bool IsSunk()
        {
            return new HashSet<Cell>(hitCells).SetEquals(cells);
        }
    }
}
