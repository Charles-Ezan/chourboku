using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Assignement
    {
        // sudoku
        public int[,] sudoku = new int[8, 8];


        // Etat du sudoku
        private bool complete = false;

        // Initialisation du sudoku
        public void Initialize_sudoku(int[,] new_sudoku)
        {
            sudoku = new_sudoku;
            complete = false;
        }

        // Assignement d'un élément dans le sudoku
        public void Set_variable_in_sudoku(int a_value, int a_row, int a_column)
        {
            sudoku[a_row, a_column] = a_value;
            Is_complete();
        }

        // Remise à 0 d'un élément du sudoku
        public void Reset_variable_in_sudoku(int a_row, int a_column)
        {
            sudoku[a_row, a_column] = 0;
        }

        // Le sudoku est-il fini ?
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
        }
    }
}
