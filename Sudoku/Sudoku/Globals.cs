using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class cell
    {
        public int value //value at cell
        { get; set; }
        public int[] possibilities; //possible values for cell
        public bool given //true if the cell is given in the original board
        { get; set; }
        public cell(int size, int value, bool given)
        { // init the cell
            this.value = value;
            possibilities = new int[size];
            this.given = given;
        }
    }
    public static class Globals
    {
        public static int rows; //board index is 0-size
        public static int cols; //board index is 0-size
        public static cell[,] board; //sudoku board

        public static void InitGlobals(int size)
        { //init size*size board
            rows = size;
            cols = size;
            board = new cell[rows, cols]; //init sudoku board for size*size
        }
    }
}
