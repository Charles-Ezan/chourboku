using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class CSP
    {
        //Variables
        //Ensemble des cases de la grille 9x9 (81 variables)
/*        Cell[,] grid = new Cell[8, 8];*/

        // Domaine
        public List<int> domaine = new List<int>(){1, 2, 3, 4, 5, 6, 7, 8, 9};

        //Contraintes


        // Test de la contrainte de ligne
        public bool Test_row_constraints(int value_tested, int value_x, int[,] sudoku)//, int value_y)
        {
            // Récupérer les contraintes sur la ligne correspondante
            List<int> row_sudoku = new List<int>();
            for(int i=0; i< sudoku.GetLength(0); i++)
            {
                if (sudoku[value_x, i] != 0)
                    row_sudoku.Add(sudoku[value_x,i]);
            }
            // Test de la valeur par rapport aux autres numéros dans la ligne
            foreach (int row_value in row_sudoku)
            {
                if (value_tested == row_value)
                {
                    return false;
                }
            }
            return true;
        }


        // Test de la contrainte de colonne
        public bool Test_column_constraints(int value_tested, int value_y, int[,] sudoku)//, int value_y)
        {

            // Récupérer les contraintes sur la ligne correspondante
            List<int> column_sudoku = new List<int>();
            for (int i = 0; i < sudoku.GetLength(1); i++)
            {
                if(sudoku[i, value_y] != 0)
                    column_sudoku.Add(sudoku[i, value_y]);
            }

/*            Console.WriteLine("nb : " + column_sudoku.Count());
            foreach (int value in column_sudoku)
            {
                Console.WriteLine(value);
            }*/

            // Test de la valeur par rapport aux autres numéros dans la colonne
            foreach (int column_value in column_sudoku)
            {
                if(value_tested == column_value)
                {
                    return false;
                }
            }

            return true;
        }


        // Test de contraintes sur la mini grille pour n cases
        public bool Test_mini_grid_constraints(int value_tested, int value_x, int value_y, int[,] sudoku)//, int value_y)
        {
            int interval_mini_grid_x = 0;
            int interval_mini_grid_y = 0;

            for (int n=1; n <= (sudoku.GetLength(0)/3); n++) 
            {
                if ((1*n <= value_x+1) && (value_x + 1 <= 3*n))
                {
                    interval_mini_grid_x = n;
                    break;
                }
            }

            for (int k = 1; k <= (sudoku.GetLength(0)/3); k++)
            {
                if ((1 * k <= value_y + 1) && (value_y + 1 <= 3 * k))
                {
                    interval_mini_grid_y = k;
                    break;
                }
            }
/*            Console.WriteLine("interval_mini_grid_x  interval_mini_grid_y : " + interval_mini_grid_x.ToString() + " " + interval_mini_grid_y.ToString());*/            List<int> mini_grid_sudoku = new List<int>();
            for (int i = 3 * interval_mini_grid_x-2; i < interval_mini_grid_x*3; i++)
            {
                for (int j = 3 * interval_mini_grid_y-2; j < interval_mini_grid_y*3; j++)
                {
                    if ((sudoku[i - 1, j - 1]) != 0) { 
                        mini_grid_sudoku.Add(sudoku[i - 1, j - 1]);
                    }
                }
            }
/*            Console.WriteLine("nb : " + mini_grid_sudoku.Count());
            foreach (int value in mini_grid_sudoku)
            {
                Console.WriteLine(value);
            }*/

            foreach (int mini_grid_value in mini_grid_sudoku)
            {
                if (value_tested == mini_grid_value)
                {
                    return false;
                }
            }

            return true;
        }



        public bool Is_assignement_consistent(int value_tested, int value_x, int value_y, int[,] sudoku)
        {
            /*Console.WriteLine("value_tested : ", value_tested);*/
            // Test des contraintes
            if ((Test_row_constraints(value_tested, value_x, sudoku) == false) ||
                (Test_column_constraints(value_tested, value_y, sudoku) == false) ||
                (Test_mini_grid_constraints(value_tested, value_x, value_y, sudoku) == false))
            {
                return false;
            }

            return true;
        }
    }
}
