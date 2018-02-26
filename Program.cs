using System;
using System.Collections.Generic;
using System.Linq;

namespace eight_puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            IAgent agent = new SimpleAgent();
            var board = new Board();
            board.Print();
            var rng = new Random();
            while (!board.IsSolved())
            {
                board.Move(agent.GetMove(board));
                if (board.moves % 100001 == 0)
                {
                    System.Console.WriteLine(board.moves);
                    board.Print();
                }
            }
            System.Console.WriteLine("====SOLVED====");
            board.Print();
            Console.ReadKey();
        }
    }
}
