using System;

namespace Battleships
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Player
    {
        protected readonly IList<Cell> previousGuesses;
        protected readonly IList<Ship> ships;

        protected Player()
        {
            this.previousGuesses = new List<Cell>();
            this.ships = new List<Ship>();
        }

        public string Name { get; protected set; }

        public void AddShip(Ship ship)
        {
            this.ships.Add(ship);
        }

        public bool Dead()
        {
            return ships.All(x => x.IsSunk());
        }

        public virtual bool CheckForHit(Cell guessedCell)
        {
            var ship = this.ships.FirstOrDefault(x => x.IsHit(guessedCell));

            if (ship == null)
            {
                Console.WriteLine(this.Name + ": YOU HAVE MISSED!!");
                return false;
            }
            
            if (ship.IsSunk())
            {
                Console.WriteLine(this.Name + ": YOU HAVE SUNK MY: " + ship.ShipType);
                return true;
            }

            Console.WriteLine(this.Name + "YOU HAVE SCORED A HIT!!");
            return true;
        }

        public abstract Cell TakeShot();
    }
}
