using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public static class Globals
    {
        public const int rows = 8; //9, arr index is 0-8
        public const int cols = 8; //9, arr index is 0-8
        public static int[,] board = new int[rows, cols];
    }
}
