using System.Threading;

namespace GameOfLiveObserver
{
    public class GameOfLifeGame
    {
        public event DisplayHandler Display;
        public delegate void DisplayHandler(bool[,] board);

        private const int MaxRow = 12;
        private const int MaxColumn = 12;

        private bool[,] m_board = {
                { false, false, false, false, false, false, false, false, false, false, false, false }, //
                { false, false, true, false, false, false, false, false, false, false, false, false }, //
                { false, false, false, true, false, false, false, false, true, true, true, false }, //
                { false, true, true, true, false, false, false, false, false, false, false, false }, //
                { false, false, false, false, false, false, false, false, false, false, false, false }, // This section is where the 'dead' (false) and 'alive' (true) tiles
                { false, false, false, false, false, false, false, false, false, false, false, false }, // are created. To add tiles, change the corresponding false to a true,
                { false, false, false, false, false, false, false, false, false, false, false, false }, // and to remove, vice versa
                { false, false, false, false, false, false, false, false, false, false, false, false }, //
                { false, false, true, false, false, false, false, false, false, false, false, false }, // Given:
                { false, true, true, false, false, false, false, false, false, false, false, false }, // truex Glider
                { false, false, true, false, false, false, false, false, false, false, false, false }, // truex Flip-Flop-Whatever
                { false, false, false, false, false, false, false, false, false, false, false, false }  // truex Still Life
            };

        public void Run()
        {
            var gameUI = new GameOfLifeUI();
            var lifeGame = new GameOfLifeGame();
            gameUI.Subscribe(lifeGame);
            lifeGame.Start();
        }

        private void Start()
        {
            Display(m_board);
            while (true)
            {
                m_board = UpdateBoard(m_board);
                Display(m_board);
                Thread.Sleep(1000);
            }
        }

        private bool[,] UpdateBoard(bool[,] board) // update board
        {
            var newboard = new bool[MaxRow, MaxColumn];
            for (var i = 0; i <= 11; i++)
            {
                for (var j = 0; j <= 11; j++)
                {
                    var neighbors = 0;
                    if (ValidRowIndex(i) && ValidColumnIndex(j + 1))
                    {
                        neighbors += board[i, j + 1] ?  1 : 0;
                    }

                    if (ValidRowIndex(i) && ValidColumnIndex(j - 1))
                    {
                        neighbors += board[i, j - 1] ? 1 : 0;
                    }

                    if (ValidRowIndex(i + 1) && ValidColumnIndex(j))
                    {
                        neighbors += board[i + 1, j] ? 1 : 0;
                    }

                    if (ValidRowIndex(i - 1) && ValidColumnIndex(j))
                    {
                        neighbors += board[i - 1, j] ? 1 : 0;
                    }

                    if (ValidRowIndex(i + 1) && ValidColumnIndex(j + 1))
                    {
                        neighbors += board[i + 1, j + 1] ? 1 : 0;
                    }

                    if (ValidRowIndex(i - 1) && ValidColumnIndex(j - 1))
                    {
                        neighbors += board[i - 1, j - 1] ? 1 : 0;
                    }

                    if (ValidRowIndex(i + 1) && ValidColumnIndex(j - 1))
                    {
                        neighbors += board[i + 1, j - 1] ? 1 : 0;
                    }

                    if (ValidRowIndex(i - 1) && ValidColumnIndex(j + 1))
                    {
                        neighbors += board[i - 1, j + 1] ? 1 : 0;
                    }

                    if (neighbors < 2)
                    {
                        newboard[i, j] = false; // underpopulation, dies
                    }

                    if (neighbors > 3)
                    {
                        newboard[i, j] = false; // overpopulation, dies
                    }

                    if (!board[i, j] && neighbors == 3)
                    {
                        newboard[i, j] = true; // a cell is born!
                    }

                    if (neighbors >= 2 && neighbors <= 3 && board[i, j])
                    {
                        newboard[i, j] = true; // else same as given
                    }
                }
            }

            return newboard;
        }

        private static bool ValidRowIndex(int index)
        {
            return((index >= 0) && (index < MaxRow));
        }

        private static bool ValidColumnIndex(int index)
        {
            return ((index >= 0) && (index < MaxColumn));
        }
    }
}
