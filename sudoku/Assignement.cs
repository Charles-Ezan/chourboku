using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Assignement
    {

/*        private Cell[,] sudoku_cell = new Cell[8, 8];*/

        public int[,] sudoku = new int[8, 8];

        private bool complete = false;

        /*        public void Initialize_sudoku(int[,] new_sudoku)
                {
                    *//*            for(int i = 0; i< new_sudoku.GetLength(0))
                                {

                                }*//*
                    sudoku = new_sudoku;
                }*/

        public void Initialize_sudoku(int[,] new_sudoku)
        {
            /*            for(int i = 0; i< new_sudoku.GetLength(0))
                        {

                        }*/
            sudoku = new_sudoku;
            complete = false;
        }

        public void Set_variable_in_sudoku(int a_value, int a_row, int a_column)
        {
            sudoku[a_row, a_column] = a_value;
            Is_complete();
            //Console.WriteLine("Variable Set");
        }

        public void Reset_variable_in_sudoku(int a_row, int a_column)
        {
            sudoku[a_row, a_column] = 0;
            //Console.WriteLine("Variable Reset !");
        }


        public bool Get_complete()
        {
            return complete;
        }

        // Test pour savoir si le sudoku est fini
        public void Is_complete()
        {
            complete = true;
            int empty_var = 0;
            foreach(int value in sudoku)
            {
                if(value == 0) {
                    empty_var++;
                    complete = false;
                }
            }
/*            Console.WriteLine("Empty Variable : " + empty_var);*/
        }
    }
}
