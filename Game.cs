using System;
using System.Linq;

namespace Battleships
{
    public class Game
    {
        private Board board;
        private bool gameOver;

        public void Start()
        {
            board = new Board();
            var player = new HumanPlayer();
            var computer = new ComputerPlayer();

            board.Initialize(player, computer);

            do
            {
                gameOver = TakeMove(player, computer);

                if (!gameOver)
                {
                    gameOver = TakeMove(computer, player);
                }
                else
                {
                    Console.WriteLine("CONGRATULATIONS YOU WON!!!");
                    Console.ReadLine();
                    return;
                }
            } while (!gameOver);

            Console.WriteLine("COMPUTER HAS WON!!!");
            Console.ReadLine();
        }

        private bool TakeMove(Player hero, Player enemy)
        {
            bool wasHit;

            do
            {
                var guessedCell = hero.TakeShot();
                wasHit = enemy.CheckForHit(guessedCell);

                if (wasHit && enemy.Dead())
                {
                    return true;
                }
            } while (wasHit);

            return false;
        }
    }
}
