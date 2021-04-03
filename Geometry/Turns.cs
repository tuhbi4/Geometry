namespace Geometry
{
    public class Turns
    {
        public int TurnsCount { get; }
        public int CurrentTurnNumber { get; private set; }
        public Turns(int count)
        {
            TurnsCount = count;
            CurrentTurnNumber = 0;
        }
        public Turns() : this(20) { }
        public bool IsTurnsCountOver()
        {
            if (CurrentTurnNumber == TurnsCount)
            {
                return true;
            }
            return false;
        }
        public void IncrementTurnsCount()
        {
            CurrentTurnNumber++;
        }
    }
}