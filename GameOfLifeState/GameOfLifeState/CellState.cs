
namespace GameOfLifeState
{
    public abstract class CellState
    {
        public abstract CellState Live();
        public abstract CellState Die();
        public abstract CellState Toggle();
        public abstract bool IsAlive();
    }
}
