using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public static class Globals
    {
        public const int rows = 9; //board index is 0-8
        public const int cols = 9; //board index is 0-8
        public static int[,] board = new int[rows, cols];
    }
}
