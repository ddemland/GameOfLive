
using System.Collections.Generic;

namespace GameOfLifeVisitor
{
    public class Cell
    {
        public int Row { private set; get; }
        public int Column { private set; get; }
        public CellState State { set; get; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void Live()
        {
            State = State.Live();
        }

        public void Die()
        {
            State = State.Die();
        }

        public void NumOfNeighbors(GameOfLifeGame game)
        {
            State.Toggle();
        }

        public void Toggle()
        {
        }

        public bool IsAlive()
        {
            return (State.IsAlive());
        }

        public void Accept(LifeVisitor visitor, Cell cell, GameOfLifeGame game, List<LifeCommand> cmdList)
        {
            State.Accept(visitor, cell, game, cmdList);
        }
    }
}
