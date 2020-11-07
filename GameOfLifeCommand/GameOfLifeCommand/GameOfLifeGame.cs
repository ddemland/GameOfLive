using System.Collections.Generic;
using System.Threading;

namespace GameOfLifeCommand
{
    public class GameOfLifeGame
    {
        public event DisplayHandler Display;
        public delegate void DisplayHandler(Cell[,] board);

        private const int MaxRow = 12;
        private const int MaxColumn = 12;

        private Cell[,] m_board;

        public GameOfLifeGame()
        {
        }

        public GameOfLifeGame(Cell[,] board)
        {
            m_board = board;
        }

        public void Run()
        {
            m_board = CreateBoard();
            var gameUI = new GameOfLifeUI();
            var lifeGame = new GameOfLifeGame(m_board);
            gameUI.Subscribe(lifeGame);
            lifeGame.Start();
        }

        private void Start()
        {
            Display(m_board);
            while (true)
            {
                var cellList = UpdateBoard(m_board);
                Display(m_board);

                foreach (var cell in cellList)
                {
                    cell.Execute();
                }

                Thread.Sleep(1000);
            }
        }

        private IEnumerable<LifeCommand> UpdateBoard(Cell[,] board) // update board
        {
            var lifeCommandList = new List<LifeCommand>();
            for (var i = 0; i <= 11; i++)
            {
                for (var j = 0; j <= 11; j++)
                {
                    var neighbors = 0;
                    if (ValidRowIndex(i) && ValidColumnIndex(j + 1))
                    {
                        neighbors += board[i, j + 1].IsAlive() ?  1 : 0;
                    }

                    if (ValidRowIndex(i) && ValidColumnIndex(j - 1))
                    {
                        neighbors += board[i, j - 1].IsAlive() ? 1 : 0;
                    }

                    if (ValidRowIndex(i + 1) && ValidColumnIndex(j))
                    {
                        neighbors += board[i + 1, j].IsAlive() ? 1 : 0;
                    }

                    if (ValidRowIndex(i - 1) && ValidColumnIndex(j))
                    {
                        neighbors += board[i - 1, j].IsAlive() ? 1 : 0;
                    }

                    if (ValidRowIndex(i + 1) && ValidColumnIndex(j + 1))
                    {
                        neighbors += board[i + 1, j + 1].IsAlive() ? 1 : 0;
                    }

                    if (ValidRowIndex(i - 1) && ValidColumnIndex(j - 1))
                    {
                        neighbors += board[i - 1, j - 1].IsAlive() ? 1 : 0;
                    }

                    if (ValidRowIndex(i + 1) && ValidColumnIndex(j - 1))
                    {
                        neighbors += board[i + 1, j - 1].IsAlive() ? 1 : 0;
                    }

                    if (ValidRowIndex(i - 1) && ValidColumnIndex(j + 1))
                    {
                        neighbors += board[i - 1, j + 1].IsAlive() ? 1 : 0;
                    }

                    if (neighbors < 2)
                    {
                        if (board[i, j].IsAlive())
                        {
                            lifeCommandList.Add(new DieCommand(board[i, j])); // underpopulation, dies
                        }
                    }

                    if (neighbors > 3)
                    {
                        if (board[i, j].IsAlive())
                        {
                            lifeCommandList.Add(new DieCommand(board[i, j])); // overpopulation, dies
                        }
                    }

                    if (!board[i, j].IsAlive() && neighbors == 3)
                    {
                        lifeCommandList.Add(new LiveCommand(board[i, j])); // a cell is born!
                    }

                    if (neighbors >= 2 && neighbors <= 3 && board[i, j].IsAlive())
                    {
                        lifeCommandList.Add(new LiveCommand(board[i, j])); // else same as given
                    }
                }
            }

            return lifeCommandList;
        }

        private static bool ValidRowIndex(int index)
        {
            return((index >= 0) && (index < MaxRow));
        }

        private static bool ValidColumnIndex(int index)
        {
            return ((index >= 0) && (index < MaxColumn));
        }

        private Cell[,] CreateBoard()
        {
            var board = new Cell[MaxRow, MaxColumn];

            board[0, 0] = CreateDeadCell(0, 0);
            board[0, 1] = CreateDeadCell(0, 1);
            board[0, 2] = CreateDeadCell(0, 2);
            board[0, 3] = CreateDeadCell(0, 3);
            board[0, 4] = CreateDeadCell(0, 4);
            board[0, 5] = CreateDeadCell(0, 5);
            board[0, 6] = CreateDeadCell(0, 6);
            board[0, 7] = CreateDeadCell(0, 7);
            board[0, 8] = CreateDeadCell(0, 8);
            board[0, 9] = CreateDeadCell(0, 9);
            board[0, 10] = CreateDeadCell(0, 1);
            board[0, 11] = CreateDeadCell(0, 1);

            board[1, 0] = CreateDeadCell(1, 0);
            board[1, 1] = CreateDeadCell(1, 1);
            board[1, 2] = CreateAliveCell(1, 2);
            board[1, 3] = CreateDeadCell(1, 3);
            board[1, 4] = CreateDeadCell(1, 4);
            board[1, 5] = CreateDeadCell(1, 5);
            board[1, 6] = CreateDeadCell(1, 6);
            board[1, 7] = CreateDeadCell(1, 7);
            board[1, 8] = CreateDeadCell(1, 8);
            board[1, 9] = CreateDeadCell(1, 9);
            board[1, 10] = CreateDeadCell(1, 1);
            board[1, 11] = CreateDeadCell(1, 1);

            board[2, 0] = CreateDeadCell(2, 0);
            board[2, 1] = CreateDeadCell(2, 1);
            board[2, 2] = CreateDeadCell(2, 2);
            board[2, 3] = CreateAliveCell(2, 3);
            board[2, 4] = CreateDeadCell(2, 4);
            board[2, 5] = CreateDeadCell(2, 5);
            board[2, 6] = CreateDeadCell(2, 6);
            board[2, 7] = CreateDeadCell(2, 7);
            board[2, 8] = CreateAliveCell(2, 8);
            board[2, 9] = CreateAliveCell(2, 9);
            board[2, 10] = CreateAliveCell(2, 1);
            board[2, 11] = CreateDeadCell(2, 1);

            board[3, 0] = CreateDeadCell(3, 0);
            board[3, 1] = CreateAliveCell(3, 1);
            board[3, 2] = CreateAliveCell(3, 2);
            board[3, 3] = CreateAliveCell(3, 3);
            board[3, 4] = CreateDeadCell(3, 4);
            board[3, 5] = CreateDeadCell(3, 5);
            board[3, 6] = CreateDeadCell(3, 6);
            board[3, 7] = CreateDeadCell(3, 7);
            board[3, 8] = CreateDeadCell(3, 8);
            board[3, 9] = CreateDeadCell(3, 9);
            board[3, 10] = CreateDeadCell(3, 1);
            board[3, 11] = CreateDeadCell(3, 1);

            board[4, 0] = CreateDeadCell(4, 0);
            board[4, 1] = CreateDeadCell(4, 1);
            board[4, 2] = CreateDeadCell(4, 2);
            board[4, 3] = CreateDeadCell(4, 3);
            board[4, 4] = CreateDeadCell(4, 4);
            board[4, 5] = CreateDeadCell(4, 5);
            board[4, 6] = CreateDeadCell(4, 6);
            board[4, 7] = CreateDeadCell(4, 7);
            board[4, 8] = CreateDeadCell(4, 8);
            board[4, 9] = CreateDeadCell(4, 9);
            board[4, 10] = CreateDeadCell(4, 1);
            board[4, 11] = CreateDeadCell(4, 1);

            board[5, 0] = CreateDeadCell(5, 0);
            board[5, 1] = CreateDeadCell(5, 1);
            board[5, 2] = CreateDeadCell(5, 2);
            board[5, 3] = CreateDeadCell(5, 3);
            board[5, 4] = CreateDeadCell(5, 4);
            board[5, 5] = CreateDeadCell(5, 5);
            board[5, 6] = CreateDeadCell(5, 6);
            board[5, 7] = CreateDeadCell(5, 7);
            board[5, 8] = CreateDeadCell(5, 8);
            board[5, 9] = CreateDeadCell(5, 9);
            board[5, 10] = CreateDeadCell(5, 1);
            board[5, 11] = CreateDeadCell(5, 1);

            board[6, 0] = CreateDeadCell(6, 0);
            board[6, 1] = CreateDeadCell(6, 1);
            board[6, 2] = CreateDeadCell(6, 2);
            board[6, 3] = CreateDeadCell(6, 3);
            board[6, 4] = CreateDeadCell(6, 4);
            board[6, 5] = CreateDeadCell(6, 5);
            board[6, 6] = CreateDeadCell(6, 6);
            board[6, 7] = CreateDeadCell(6, 7);
            board[6, 8] = CreateDeadCell(6, 8);
            board[6, 9] = CreateDeadCell(6, 9);
            board[6, 10] = CreateDeadCell(6, 1);
            board[6, 11] = CreateDeadCell(6, 1);

            board[7, 0] = CreateDeadCell(7, 0);
            board[7, 1] = CreateDeadCell(7, 1);
            board[7, 2] = CreateDeadCell(7, 2);
            board[7, 3] = CreateDeadCell(7, 3);
            board[7, 4] = CreateDeadCell(7, 4);
            board[7, 5] = CreateDeadCell(7, 5);
            board[7, 6] = CreateDeadCell(7, 6);
            board[7, 7] = CreateDeadCell(7, 7);
            board[7, 8] = CreateDeadCell(7, 8);
            board[7, 9] = CreateDeadCell(7, 9);
            board[7, 10] = CreateDeadCell(7, 1);
            board[7, 11] = CreateDeadCell(7, 1);

            board[8, 0] = CreateDeadCell(8, 0);
            board[8, 1] = CreateDeadCell(8, 1);
            board[8, 2] = CreateAliveCell(8, 2);
            board[8, 3] = CreateDeadCell(8, 3);
            board[8, 4] = CreateDeadCell(8, 4);
            board[8, 5] = CreateDeadCell(8, 5);
            board[8, 6] = CreateDeadCell(8, 6);
            board[8, 7] = CreateDeadCell(8, 7);
            board[8, 8] = CreateDeadCell(8, 8);
            board[8, 9] = CreateDeadCell(8, 9);
            board[8, 10] = CreateDeadCell(8, 1);
            board[8, 11] = CreateDeadCell(8, 1);

            board[9, 0] = CreateDeadCell(9, 0);
            board[9, 1] = CreateAliveCell(9, 1);
            board[9, 2] = CreateAliveCell(9, 2);
            board[9, 3] = CreateDeadCell(9, 3);
            board[9, 4] = CreateDeadCell(9, 4);
            board[9, 5] = CreateDeadCell(9, 5);
            board[9, 6] = CreateDeadCell(9, 6);
            board[9, 7] = CreateDeadCell(9, 7);
            board[9, 8] = CreateDeadCell(9, 8);
            board[9, 9] = CreateDeadCell(9, 9);
            board[9, 10] = CreateDeadCell(9, 1);
            board[9, 11] = CreateDeadCell(9, 1);

            board[10, 0] = CreateDeadCell(10, 0);
            board[10, 1] = CreateDeadCell(10, 1);
            board[10, 2] = CreateAliveCell(10, 2);
            board[10, 3] = CreateDeadCell(10, 3);
            board[10, 4] = CreateDeadCell(10, 4);
            board[10, 5] = CreateDeadCell(10, 5);
            board[10, 6] = CreateDeadCell(10, 6);
            board[10, 7] = CreateDeadCell(10, 7);
            board[10, 8] = CreateDeadCell(10, 8);
            board[10, 9] = CreateDeadCell(10, 9);
            board[10, 10] = CreateDeadCell(10, 1);
            board[10, 11] = CreateDeadCell(10, 1);

            board[11, 0] = CreateDeadCell(11, 0);
            board[11, 1] = CreateDeadCell(11, 1);
            board[11, 2] = CreateDeadCell(11, 2);
            board[11, 3] = CreateDeadCell(11, 3);
            board[11, 4] = CreateDeadCell(11, 4);
            board[11, 5] = CreateDeadCell(11, 5);
            board[11, 6] = CreateDeadCell(11, 6);
            board[11, 7] = CreateDeadCell(11, 7);
            board[11, 8] = CreateDeadCell(11, 8);
            board[11, 9] = CreateDeadCell(11, 9);
            board[11, 10] = CreateDeadCell(11, 1);
            board[11, 11] = CreateDeadCell(11, 1);

            return board;
        }

        private static Cell CreateDeadCell(int row, int column)
        {
            return new Cell(row, column) { State = DeadState.Create() };
        }

        private static Cell CreateAliveCell(int row, int column)
        {
            return new Cell(row, column) { State = AliveState.Create() };
        }
    }
}
