
using System.Collections.Generic;

namespace GameOfLifeVisitor
{
    public class LifeVisitor
    {
        public void Visit(Cell cell, GameOfLifeGame game, List<LifeCommand> cmdList)
        {
            if (cell.State is AliveState)
            {
                VisitAliveCell(cell, game, cmdList);
            }
            else
            {
                VisitDeadCell(cell, game, cmdList);
            }
        }

        public void VisitAliveCell(Cell cell, GameOfLifeGame game, List<LifeCommand> cmdList)
        {
            var neighbors = NumberOfNeighborsByMe(cell, game.Board);
            if (neighbors < 2)
            {
                cmdList.Add(new DieCommand(cell)); // underpopulation, dies
            }

            if (neighbors > 3)
            {
                cmdList.Add(new DieCommand(cell)); // overpopulation, dies
            }
        }

        public void VisitDeadCell(Cell cell, GameOfLifeGame game, List<LifeCommand> cmdList)
        {
            var neighbors = NumberOfNeighborsByMe(cell, game.Board);
            if (!cell.IsAlive() && neighbors == 3)
            {
                cmdList.Add(new LiveCommand(cell)); // a cell is born!
            }

            if (neighbors >= 2 && neighbors <= 3 && cell.IsAlive())
            {
                cmdList.Add(new LiveCommand(cell)); // else same as given
            }
        }

        private static int NumberOfNeighborsByMe(Cell cell, Cell[,] board)
        {
            var neighbors = 0;
            var row = cell.Row;
            var column = cell.Column;
            if (ValidRowIndex(row) && ValidColumnIndex(column + 1))
            {
                neighbors += board[row, column + 1].IsAlive() ? 1 : 0;
            }

            if (ValidRowIndex(row) && ValidColumnIndex(column - 1))
            {
                neighbors += board[row, column - 1].IsAlive() ? 1 : 0;
            }

            if (ValidRowIndex(row + 1) && ValidColumnIndex(column))
            {
                neighbors += board[row + 1, column].IsAlive() ? 1 : 0;
            }

            if (ValidRowIndex(row - 1) && ValidColumnIndex(column))
            {
                neighbors += board[row - 1, column].IsAlive() ? 1 : 0;
            }

            if (ValidRowIndex(row + 1) && ValidColumnIndex(column + 1))
            {
                neighbors += board[row + 1, column + 1].IsAlive() ? 1 : 0;
            }

            if (ValidRowIndex(row - 1) && ValidColumnIndex(column - 1))
            {
                neighbors += board[row - 1, column - 1].IsAlive() ? 1 : 0;
            }

            if (ValidRowIndex(row + 1) && ValidColumnIndex(column - 1))
            {
                neighbors += board[row + 1, column - 1].IsAlive() ? 1 : 0;
            }

            if (ValidRowIndex(row - 1) && ValidColumnIndex(column + 1))
            {
                neighbors += board[row - 1, column + 1].IsAlive() ? 1 : 0;
            }

            return neighbors;
        }

        private static bool ValidRowIndex(int index)
        {
            return ((index >= 0) && (index < GameOfLifeGame.MaxRow));
        }

        private static bool ValidColumnIndex(int index)
        {
            return ((index >= 0) && (index < GameOfLifeGame.MaxColumn));
        }
    }
}
