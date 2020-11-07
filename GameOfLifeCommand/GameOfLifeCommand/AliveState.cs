
namespace GameOfLifeCommand
{
    public class AliveState : CellState
    {
        static protected AliveState Instance { set; get; }

        private AliveState()
        {
        }

        static public AliveState Create()
        {
            return Instance ?? (Instance = new AliveState());
        }

        public override CellState Live()
        {
            return this;
        }

        public override CellState Die()
        {
            return DeadState.Create();
        }

        public override CellState Toggle()
        {
            return DeadState.Create();
        }

        public override bool IsAlive()
        {
            return true;
        }
    }
}
