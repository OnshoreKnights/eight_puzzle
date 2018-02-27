using System;
using System.Collections.Generic;
using System.Linq;

namespace eight_puzzle
{
    public class Board
    {
        public int[] _board;
        private int _width;
        internal int moves;
        private int hCache = -1;
        internal Board(int tileCount = 9)
        {
            if (tileCount <= 0)
            {
                throw new ArgumentOutOfRangeException("dim", "Parameter must be postive");
            }
            else if (Math.Sqrt(tileCount) % 1 != 0)
            {
                throw new ArgumentException("Parameter must be a perfect square", "dim");
            }
            _width = (int)Math.Round(Math.Sqrt(tileCount));
            var rng = new Random();
            do
            {
                _board = Enumerable.Range(0, tileCount).OrderBy(_ => rng.Next()).ToArray();
            } while (!IsSolveable());
        }

        public bool IsSolveable()
        {
            int inversions = 0;
            for (int i = 0; i < _board.Length; i++)
            {
                for (int j = i + 1; j < _board.Length; j++)
                {
                    if (_board[i] != 0 && _board[j] != 0 && _board[i] > _board[j])
                    {
                        inversions++;
                    }
                }
            }
            return inversions % 2 == 0;
        }

        internal void Move(int move)
        {
            if (!IsLegalMove(move))
            {
                throw new ApplicationException("Illegal move!");
            }
            moves++;
            int zeroIdx = GetEmptySquare();
            int temp = _board[zeroIdx];
            _board[zeroIdx] = _board[move];
            _board[move] = temp;
            hCache = -1;
        }

        internal bool IsSolved()
        {
            int last = -1;
            foreach (int tile in _board)
            {
                if (tile != 0)
                {
                    if (tile < last)
                    {
                        return false;
                    }
                    last = tile;
                }
            }
            return true;
        }

        private bool IsLegalMove(int move)
        {
            return GetLegalMoves().Contains(move);
        }

        internal void Print()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                if (i % _width == 0)
                {
                    if (i != 0)
                    {
                        Console.WriteLine('|');
                    }
                    for (int _ = 0; _ < (_width * 2 + 1); _++)
                    {
                        Console.Write('-');
                    }
                    System.Console.WriteLine();
                }
                Console.Write('|');
                if (_board[i] != 0)
                {
                    Console.Write(_board[i]);
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine('|');
            for (int _ = 0; _ < (_width * 2 + 1); _++)
            {
                Console.Write('-');
            }
            System.Console.WriteLine();
            System.Console.WriteLine("Move #: " + moves);
            System.Console.WriteLine("Legal Moves: { " + String.Join(", ", GetLegalMoves()) + " }");
            System.Console.WriteLine("Heuristic: " + GetManhattanDistance());
            System.Console.WriteLine();
        }

        private int GetEmptySquare()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                if (_board[i] == 0)
                {
                    return i;
                }
            }
            throw new ApplicationException("Invalid board");
        }

        internal IEnumerable<int> GetLegalMoves()
        {
            var moves = new List<int>();
            int emptyIdx = GetEmptySquare();
            // above
            if (emptyIdx - _width >= 0)
            {
                moves.Add(emptyIdx - _width);
            }
            // below
            if (emptyIdx + _width < _board.Length)
            {
                moves.Add(emptyIdx + _width);
            }
            // left
            if ((emptyIdx - 1) / _width == emptyIdx / _width && emptyIdx != 0)
            {
                moves.Add(emptyIdx - 1);
            }
            // right
            if ((emptyIdx + 1) / _width == emptyIdx / _width)
            {
                moves.Add(emptyIdx + 1);
            }
            return moves;
        }

        public Board Copy()
        {
            var board = new Board(this._board.Length);
            this._board.CopyTo(board._board, 0);
            board.moves = this.moves;
            board._width = this._width;
            return board;
        }
        
        public bool IsDuplicate(Board otherBoard)
        {
            if (otherBoard == null || _board == null || otherBoard._board == null || _board.Length != otherBoard._board.Length)
            {
                return false;
            }
            return String.Join("", _board).Equals(String.Join("", otherBoard._board));
        }

        public int GetManhattanDistance()
        {
            if (hCache > 0) return hCache;
            int totalDist = 0;
            for (int i = 0; i < _board.Length; i++)
            {
                int tile = _board[i];
                if (tile != i)
                {
                    totalDist +=
                        // row distance
                        Math.Abs(i / _width - tile / _width) +
                        // column distance
                        Math.Abs(i % _width - tile % _width);
                }
            }
            hCache = totalDist;
            return totalDist;
        }

        public int GetMisplacedTiles()
        {
            int misplaced = 0;
            for (int i = 1; i < _board.Length; i++)
            {
                if (_board[i] != 0)
                {
                    if (_board[i] > i || _board[i] < i - 1)
                    {
                        misplaced++;
                    }
                }
            }
            return misplaced;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //
            
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            return this.IsDuplicate((Board)obj);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return String.Join("", _board).GetHashCode();
        }
    }
}