using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    class InvalidNumException : Exception
    { // if cant convert char to int
        public InvalidNumException(string err_char)
        : base(String.Format("Invalid Char In Board: {0}", err_char))
        {
        }
    }

    class UnsolvableBoardException : Exception
    { // if the board is unsolvable
        public UnsolvableBoardException()
        : base(String.Format("The Given Board Is Unsolvable"))
        {
        }
    }

    class InvalidBoardException : Exception
    { // if the board is not possible by the rules
        public InvalidBoardException()
        : base(String.Format("The Given Board Is Invalid"))
        {
        }
    }
}
