using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class logic
    {
        private static bool isInRow(cell[,] board, int number, int row)
        { // returns true if num is in a given row in board, else false.
            for (int i = 0; i < Globals.rows; i++)
            {
                if (board[row,i].value == number)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isInCol(cell[,] board, int number, int col)
        { // returns true if num is in a given col in board, else false.
            for (int i = 0; i < Globals.cols; i++)
            {
                if (board[i,col].value == number)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool isInBox(cell[,] board, int number, int row, int col)
        { // returns true if num is in a given box in board, else false.
            //the box is the one the [row,col] is in
            int BoxSize = (int)Math.Sqrt(Globals.rows); //box row/col size
            int BoxRow = row - row % BoxSize; //calc box starting index - row
            int BoxCol = col - col % BoxSize;//calc box starting index - col

            for (int i = BoxRow; i < BoxRow + BoxSize; i++)
            {
                for (int j = BoxCol; j < BoxCol + BoxSize; j++)
                {
                    if (board[i,j].value == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool isValidPlacement(cell[,] board, int number, int row, int col)
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
                            if (true)//isValidPlacement(board, numberToTry, row, col))
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
                if(true)//isValidPlacement(board, number, row, col))
                {
                    board[row, col] = number;
                    if (solveBoard2(board, row, col + 1))
                        return true;
                    board[row, col] = 0;
                }
            }
            return false;
        }
        private static cell[,] build_possibles(cell[,] board)
        {
            for (int row = 0; row < Globals.rows; row++)
            {
                for (int col = 0; col < Globals.cols; col++)
                {
                    if (!board[row, col].given)
                    {
                        for (int number = 1; number <= Globals.rows; number++)
                        {
                            if (isValidPlacement(board, number, row, col))
                            {
                                board[row, col].possibilities[number - 1] = number;
                            }
                            else
                                board[row, col].possibilities[number - 1] = 0;
                        }
                    }
                }
            }
            return board;
        }
        private static bool solveBoard3(cell[,] board, int row = 0, int col = 0)
        { // solves the board and changes the given board. returns true if succeeded, else false
            if (row == Globals.rows)
                return true;
            else if (col == Globals.cols)
                return solveBoard3(board, row + 1, 0);
            else if (board[row, col].given)
                return solveBoard3(board, row, col + 1);
            for (int index = 0; index < Globals.rows; index++)
            {
                int number = board[row, col].possibilities[index];
                if (number != 0)
                {
                    if (isValidPlacement(board, number, row, col))
                    {
                        board[row, col].value = number;
                        if (solveBoard3(board, row, col + 1))
                            return true;
                    }
                    board[row, col].value = 0;
                }
            }
            return false;
        }

        public static string solve()
        {
            if (solveBoard3(build_possibles(Globals.board)))
            {
                string boardStr = "";
                for (int row = 0; row < Globals.rows; row++)
                {
                    for (int col = 0; col < Globals.cols; col++)
                    {
                        boardStr = boardStr + (char)(Globals.board[row, col].value + '0');
                    }
                }
                return boardStr;
            }

            else
            {
                UnsolvableBoardException unsolvableBoard = new UnsolvableBoardException();
                throw unsolvableBoard;
            }
        }


        public static bool solveBitWise(cell[,] arr, int[] rows, int[] cols, int[,] grid, int i, int j)
        { // solves the board and changes the given board. returns true if succeeded, else false
            if (j == Globals.cols) 
                return true; //if reached end of board

            else if (i == Globals.rows)
                return solveBitWise(arr, rows, cols, grid, 0, j + 1); //if reached end of column

            if (arr[i, j].value != 0)
                return solveBitWise(arr, rows, cols, grid, i + 1, j); //if the cell isnt empty(the cell is given in the original board)

            for (int option = 0; option < Globals.rows; option++)
            {
                if(arr[i, j].possibilities[option] != 0) { 
                    int digit = 1 << (option); //curront num mask
                    if (!((rows[i] & digit) != 0 || //if num is leagal in current row
                       (cols[j] & digit) != 0 || //if num is leagal in current row
                       ((grid[i / Globals.sqrtSize, j / Globals.sqrtSize] & digit) != 0))) //if num is leagal in current box
                    {
                        arr[i, j].value = option+1; //insert the num to the board
                        rows[i] |= digit; //make num invalid in current row
                        cols[j] |= digit; //make num invalid in current column
                        grid[i / Globals.sqrtSize, j / Globals.sqrtSize] |= digit; //make num invalid in current box
                        if (solveBitWise(arr, rows, cols, grid, i + 1, j))
                            return true; //continue solving the board
                        // if the num in the current cell doesnt lead to a solved board
                        rows[i] &= ~digit; //make num valid in current row
                        cols[j] &= ~digit; //make num valid in current column
                        grid[i / Globals.sqrtSize, j / Globals.sqrtSize] &= ~digit; //make num valid in current box
                        arr[i, j].value = 0;
                    }
                }
            }
            return false; // if all nums doesnt result in a solved board - the current board isnt solvable
        }

        public static string callSolve()
        { // creates arr helpers for the solving func, returns the solved board as a string or UnsolvableBoardException
            int[] rows = new int[Globals.rows];
            int[] cols = new int[Globals.cols];
            int[,] grid = new int[Globals.sqrtSize, Globals.sqrtSize];

            for (int i = 0; i < Globals.rows; i++)
            {
                for (int j = 0; j < Globals.cols; j++)
                {
                    int digit = Globals.board[i, j].value;
                    if (digit != 0)
                    {
                        int value = 1 << (Globals.board[i,j].value - 1); //curront num mask
                        rows[i] |= value; //make num invalid in current row
                        cols[j] |= value; //make num invalid in current column
                        grid[i / Globals.sqrtSize, j / Globals.sqrtSize] |= value; //make num invalid in current box
                    }
                }
            }
            Globals.board = build_possibles(Globals.board);
            if (solveBitWise(Globals.board, rows, cols, grid, 0, 0))
            { //return the solved board as a string
                string boardStr = "";
                for (int row = 0; row < Globals.rows; row++)
                {
                    for (int col = 0; col < Globals.cols; col++)
                    {
                        boardStr = boardStr + (char)(Globals.board[row, col].value + '0');
                    }
                }
                return boardStr;
            }
            else
            { //couldent solve board - throw UnsolvableBoardException
                UnsolvableBoardException unsolvableBoard = new UnsolvableBoardException();
                throw unsolvableBoard;
            }
        }
    }
}
