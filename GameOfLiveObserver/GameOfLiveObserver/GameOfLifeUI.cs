using System;

namespace GameOfLiveObserver
{
    public class GameOfLifeUI
    {
        public void Subscribe(GameOfLifeGame m)
        {
            m.Display += DrawBoard;
        }

        public void DrawBoard(bool[,] board) // draw board
        {
            Console.Clear();
            for (var i = 0; i <= 11; i++)
            {
                for (var j = 0; j <= 11; j++)
                {
                    //Console.SetCursorPosition(j, i);
                    Console.Write((board[i, j]) ? "██" : "||");
                }

                Console.WriteLine();
            }
        }
    }
}
