
using System.Collections.Generic;

namespace GameOfLifeVisitor
{
    public abstract class CellState
    {
        public abstract CellState Live();
        public abstract CellState Die();
        public abstract CellState Toggle();
        public abstract bool IsAlive();
        public abstract void Accept(LifeVisitor visitor, Cell cell, GameOfLifeGame game, List<LifeCommand> cmdList);
    }
}
