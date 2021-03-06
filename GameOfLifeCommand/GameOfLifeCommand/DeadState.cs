﻿
namespace GameOfLifeCommand
{
    public class DeadState : CellState
    {
        static protected DeadState Instance { set; get;}

        private DeadState()
        {
        }

        static public DeadState Create()
        {
            return Instance ?? (Instance = new DeadState());
        }

        public override CellState Live()
        {
            return AliveState.Create();
        }

        public override CellState Die()
        {
            return this;
        }

        public override CellState Toggle()
        {
            return AliveState.Create();
        }

        public override bool IsAlive()
        {
            return false;
        }
    }
}
