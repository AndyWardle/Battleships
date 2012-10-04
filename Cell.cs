namespace Battleships
{
    public struct Cell
    {
        public Cell(char horizontal, int vertical) : this()
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        public char Horizontal { get; private set; }

        public int Vertical { get; private set; }

        public bool Equals(Cell other)
        {
            return Horizontal == other.Horizontal && Vertical == other.Vertical;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Horizontal.GetHashCode() * 397) ^ Vertical;
            }
        }

        public static bool operator ==(Cell c1, Cell c2)
        {
            return c1.Horizontal == c2.Horizontal && c1.Vertical == c2.Vertical;
        }

        public static bool operator !=(Cell c1, Cell c2)
        {
            return !(c1 == c2);
        }

        public override string ToString()
        {
            return Horizontal + Vertical.ToString();
        }
    }
}
