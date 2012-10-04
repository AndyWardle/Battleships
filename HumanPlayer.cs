namespace Battleships
{
    using System;
    using System.Linq;

    public class HumanPlayer : Player
    {
        public HumanPlayer()
        {
            this.Name = "Player";
        }

        public override Cell TakeShot()
        {
            while (true)
            {
                Console.WriteLine("Your turn. Please enter your guess in the format A5 (valid chars are [{0} - {1}]) and valid numbers are [{2} - {3}]", Board.ValidCharacters.First(), Board.ValidCharacters.Last(), Board.ValidNumbers.First(), Board.ValidNumbers.Last());
                var playerInput = Console.ReadLine();

                if (playerInput.Length == 2)
                {
                    playerInput = playerInput.ToUpper();
                    char horizontalGuess;
                    int verticalGuess;
                    if (Char.TryParse(playerInput[0].ToString(), out horizontalGuess) && int.TryParse(playerInput[1].ToString(), out verticalGuess))
                    {
                        if (Board.ValidCharacters.Contains(horizontalGuess) && Board.ValidNumbers.Contains(verticalGuess))
                        {
                            if (previousGuesses.Any(x => x.Horizontal == horizontalGuess && x.Vertical == verticalGuess))
                            {
                                Console.WriteLine("You have already guess there before, please try again.");
                            }
                            else
                            {
                                var cell = new Cell(horizontalGuess, verticalGuess);
                                this.previousGuesses.Add(cell);
                                return cell;
                            }
                        }
                    }
                }
            }
        }
    }
}
