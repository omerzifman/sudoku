using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class logic
    {
        private static bool isInRow(int[,] board, int number, int row)
        { // returns true if num is in a given row in board, else false.
            for (int i = 0; i < Globals.rows; i++)
            {
                if (board[row,i] == number)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isInCol(int[,] board, int number, int col)
        { // returns true if num is in a given col in board, else false.
            for (int i = 0; i < Globals.cols; i++)
            {
                if (board[i,col] == number)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isInBox(int[,] board, int number, int row, int col)
        { // returns true if num is in a given box in board, else false.
            //the box is the one the [row,col] is in
            int BoxSize = (int)Math.Sqrt(Globals.rows); //box row/col size
            int BoxRow = row - row % BoxSize; //calc box starting index - row
            int BoxCol = col - col % BoxSize;//calc box starting index - col

            for (int i = BoxRow; i < BoxRow + BoxSize; i++)
            {
                for (int j = BoxCol; j < BoxCol + BoxSize; j++)
                {
                    if (board[i,j] == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool isValidPlacement(int[,] board, int number, int row, int col)
        { //returns true if a num can be placed in a [row,col]
            return !isInRow(board, number, row) &&
                !isInCol(board, number, col) &&
                !isInBox(board, number, row, col);
        }

        private static bool solveBoard(int[,] board)
        { // solves the board and changes the given board. returns true if succeeded, else false
            for (int row = 0; row < Globals.rows; row++)
            {
                for (int col = 0; col < Globals.cols; col++)
                {
                    if (board[row,col] == 0)
                    {
                        for (int numberToTry = 1; numberToTry <= Globals.rows; numberToTry++)
                        {
                            if (isValidPlacement(board, numberToTry, row, col))
                            {
                                board[row,col] = numberToTry;

                                if (solveBoard(board))
                                {
                                    return true;
                                }
                                else
                                {
                                    board[row,col] = 0;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool solveBoard2(int[,] board, int row = 0, int col = 0)
        { // solves the board and changes the given board. returns true if succeeded, else false
            if (row == Globals.rows)
                return true;
            else if (col == Globals.cols)
                return solveBoard2(board,row+1,0);
            else if(board[row, col] != 0)
                return solveBoard2(board, row , col+1);
            for(int number = 1; number <= Globals.rows; number++)
            {
                if(isValidPlacement(board, number, row, col))
                {
                    board[row, col] = number;
                    if (solveBoard2(board, row, col + 1))
                        return true;
                    board[row, col] = 0;
                }
            }
            return false;
        }

        public static int[,] solve()
        { //returns the solved board. can throw UnsolvableBoardException
            if (solveBoard2(Globals.board))
                return Globals.board;
            else
            {
                UnsolvableBoardException unsolvableBoard = new UnsolvableBoardException();
                throw unsolvableBoard;
            }
        }
    }
}
