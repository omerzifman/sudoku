using System;
using System.IO;

namespace Sudoku
{
    public class input_output_file : Input_Output_Interface
    {
        public void getData() // can throw InvalidNumException, InvalidBoardException
        { //inserts the board from sudokuBoard.txt to globals.board
            Console.WriteLine("reading board...");
            string strBoard = File.ReadAllText(@"C:\Users\User\Desktop\יג\אומגה\sudoku\Omer's Sudoku\sudoku\Sudoku\Sudoku\sudokuBoard.txt"); //read board from text file sudokuBoard.txt
            int index = 0; //index for current char
            for (int row = 0; row < Globals.rows; row++)
            {
                for (int col = 0; col < Globals.cols; col++)
                {
                    if (strBoard.Length <= index) // if user board is smaller then 9*9
                    {
                        InvalidBoardException invalidBoard = new InvalidBoardException();
                        throw invalidBoard;
                    }
                    if (strBoard[index] - '0' >= 0 && strBoard[index] - '0' <= 9) //if char is a num
                    {
                        Globals.board[row, col] = strBoard[index] - '0'; //insert the board the current num
                        index += 1;
                    }
                    else // if char isnt a num
                    {
                        InvalidNumException invalidNum = new InvalidNumException(Char.ToString(strBoard[index]));
                        throw invalidNum;
                    }
                }
            }
            if (strBoard.Length > index) // if user board is bigger then 9*9
            {
                InvalidBoardException invalidBoard = new InvalidBoardException();
                throw invalidBoard;
            }
        }

        public void printData()
        { //prints globals.board
            Console.WriteLine("___ ___ ___ ___ ___ ___ ___ ___ ___ "); //print grid
            for (int row = 0; row < Globals.rows; row++)
            {
                for (int col = 0; col < Globals.cols; col++)
                {
                    Console.Write(" " + Globals.board[row, col] + " |"); //print a cell
                }
                Console.WriteLine();
                Console.WriteLine("___|___|___|___|___|___|___|___|___|"); //print grid
            }
        }
    }
}
