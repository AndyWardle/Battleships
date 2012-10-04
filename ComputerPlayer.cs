namespace Battleships
{
    using System;
    using System.Collections.Generic;

    public class ComputerPlayer : Player
    {
        public IList<Cell> UnGuessedCells;
        private Cell? previousChosenCellWithoutSinkingShip;

        public ComputerPlayer()
        {
            // checking what happens nwith git
            this.Name = "Computer";
            this.UnGuessedCells = new List<Cell>((Board.ValidCharacters.Length * Board.ValidNumbers.Length) / 2);

            for (int i = 0; i < Board.ValidCharacters.Length; i++)
            {
                for (int j = 0; j < Board.ValidNumbers.Length; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        this.UnGuessedCells.Add(new Cell(Board.ValidCharacters[i], Board.ValidNumbers[j]));
                    }
                }
            }
        }

        public override Cell TakeShot()
        {
            do
            {
                var guessedCell = previousChosenCellWithoutSinkingShip.HasValue
                                       ? SelectCellNearPreviousChoice()
                                       : SelectRandomCell();

                if (!this.previousGuesses.Contains(guessedCell))
                {
                    this.previousGuesses.Add(guessedCell);

                    Console.WriteLine("Computer has Guessed: "+ guessedCell);

                    return guessedCell;
                }
            } while (true);
        }

        // this could be improved by holding a collection of previous hit cells instead of 1
        // and then figuring out the direction the ship is facing to improve on the selection of the next cell
        private Cell SelectCellNearPreviousChoice()
        {
            Cell selectedCell;

            var horizontalPosition = Array.IndexOf(Board.ValidCharacters, this.previousChosenCellWithoutSinkingShip.Value.Horizontal);
            var verticalPosition = this.previousChosenCellWithoutSinkingShip.Value.Vertical;

            if (horizontalPosition > 0)
            {
                selectedCell = new Cell(Board.ValidCharacters[horizontalPosition - 1], verticalPosition);

                if (!this.previousGuesses.Contains(selectedCell))
                {
                    return selectedCell;
                }
            }
            if (horizontalPosition < Board.ValidCharacters.Length)
            {
                selectedCell = new Cell(Board.ValidCharacters[horizontalPosition + 1], verticalPosition);

                if (!this.previousGuesses.Contains(selectedCell))
                {
                    return selectedCell;
                }
            }
            if (verticalPosition > 0)
            {
                selectedCell = new Cell(Board.ValidCharacters[horizontalPosition], verticalPosition - 1);

                if (!this.previousGuesses.Contains(selectedCell))
                {
                    return selectedCell;
                }
            }
            if (verticalPosition < Board.ValidNumbers.Length)
            {
                selectedCell = new Cell(Board.ValidCharacters[horizontalPosition], verticalPosition + 1);

                if (!this.previousGuesses.Contains(selectedCell))
                {
                    return selectedCell;
                }
            }

            return new Cell('A', 0);
        }

        private Cell SelectRandomCell()
        {
            var index = new Random().Next(this.UnGuessedCells.Count);
            var guessedCell = this.UnGuessedCells[index];
            this.UnGuessedCells.RemoveAt(index);

            return guessedCell;
        }

        public override bool CheckForHit(Cell guessedCell)
        {
            var wasHit base.CheckForHit(guessedCell);
        }
    }
}
