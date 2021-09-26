namespace Geometry
{
    public class Player
    {
        public string Name { get; }
        public string Filler { get; }
        public int FilledCells { get; set; }
        public int AttemptsToRoll { get; private set; }
        public bool GameOver { get; set; }

        public Player(string name, string filler, int attemptsToRoll)
        {
            Name = name;
            Filler = filler;
            AttemptsToRoll = attemptsToRoll;
        }

        public void DecrementCountOfAttemptsToRoll()
        {
            AttemptsToRoll--;
        }
    }
}