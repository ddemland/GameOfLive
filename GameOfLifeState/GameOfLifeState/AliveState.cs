
namespace GameOfLifeState
{
    public class AliveState : CellState
    {
        public override CellState Live()
        {
            return this;
        }

        public override CellState Die()
        {
            return new DeadState();
        }

        public override CellState Toggle()
        {
            return new DeadState();
        }

        public override bool IsAlive()
        {
            return true;
        }
    }
}
