using System;
using System.Collections.Generic;
using System.Linq;

namespace eight_puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            board.Print();
            var rng = new Random();
            for (int i = 0; i < 50; i++)
            {
                board.Move(board.GetLegalMoves().OrderBy(_ => rng.Next()).First());
                board.Print();
            }
            Console.ReadKey();
        }
    }
}
