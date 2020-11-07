
namespace GameOfLifeState
{
    public class DeadState : CellState
    {
        public override CellState Live()
        {
            return new AliveState();
        }

        public override CellState Die()
        {
            return this;
        }

        public override CellState Toggle()
        {
            return new AliveState();
        }

        public override bool IsAlive()
        {
            return false;
        }
    }
}
