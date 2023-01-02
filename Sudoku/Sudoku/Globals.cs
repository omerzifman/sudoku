using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public static class Globals
    {
        public static int rows; //board index is 0-size
        public static int cols; //board index is 0-size
        public static int[,] board; //sudoku board

        public static void InitGlobals(int size)
        { //init size*size board
            rows = size;
            cols = size;
            board = new int[rows, cols]; //init sudoku board for size*size
        }
    }
}
