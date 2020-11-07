
using System.Collections.Generic;

namespace GameOfLifeVisitor
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

        public override void Accept(LifeVisitor visitor, Cell cell, GameOfLifeGame game, List<LifeCommand> cmdList)
        {
            visitor.VisitAliveCell(cell, game, cmdList);
        }
    }
}
