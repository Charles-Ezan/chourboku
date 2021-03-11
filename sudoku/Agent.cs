using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    class Agent
    {
        // Assignement
        private Assignement assignement = new Assignement();

        private CSP csp = new CSP();

        /*        List<int> order_domain_values = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };*/


        public void Initialize_assignement(int[,] sudoku)
        {
            assignement.Initialize_sudoku(sudoku);
        }


        public Assignement Get_asssignement()
        {
            return assignement;
        }

        public bool BacktrackingSearch()
        {
            return RecursiveBacktracking();
        }

        // Retourne la position de l'entier nul
        private Tuple<int, int> Select_unassigned_variable(Assignement assignement)
        {
            int value_x = 0;
            int value_y = 0;
            for (int i = 0; i < assignement.sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < assignement.sudoku.GetLength(1); j++)
                {
                    if (assignement.sudoku[i, j] == 0)
                    {
                        return Tuple.Create(i, j);
                    }
                }
            }
            return Tuple.Create(value_x, value_y);
        }

        public Tuple<int, int> MRV(Assignement assignement)
        {
            int x_pos = 0;
            int y_pos = 0;

            int min = assignement.sudoku.GetLength(0);

            Tuple<int, int> pos = new Tuple<int, int>(x_pos, y_pos);

            //Parcours les variables non assignées
            for (int i = 0; i < assignement.sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < assignement.sudoku.GetLength(1); j++)
                {
                    if (assignement.sudoku[i, j] == 0)
                    {
                        List<int> domaine = Find_Domain(i, j, assignement);
                        if (domaine.Count < min)
                        {
                            //Console.WriteLine("min avant=" + min);
                            min = domaine.Count;
                            //Console.WriteLine("min apres=" + min);
                            //Console.WriteLine("min= " + min + " domaine.count = " + domaine.Count);
                            x_pos = i;
                            y_pos = j;
                        }
                    }
                }
            }
            return Tuple.Create(x_pos, y_pos);
        }

        public List<int> Find_Domain(int value_x, int value_y, Assignement assignement)
        {
            List<int> domaine = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            List<int> column_sudoku = new List<int>();
            for (int i = 0; i < assignement.sudoku.GetLength(1); i++)
            {
                if (assignement.sudoku[i, value_y] != 0)
                {
                    domaine.Remove(assignement.sudoku[i, value_y]);
                }

                if (assignement.sudoku[value_x, i] != 0)
                {
                    domaine.Remove(assignement.sudoku[value_x, i]);
                }
            }

            int interval_mini_grid_x = 0;
            int interval_mini_grid_y = 0;

            for (int n = 1; n <= (assignement.sudoku.GetLength(0) / 3); n++)
            {
                if ((1 * n <= value_x + 1) && (value_x + 1 <= 3 * n))
                {
                    interval_mini_grid_x = n;
                    break;
                }
            }

            for (int k = 1; k <= (assignement.sudoku.GetLength(0) / 3); k++)
            {
                if ((1 * k <= value_y + 1) && (value_y + 1 <= 3 * k))
                {
                    interval_mini_grid_y = k;
                    break;
                }
            }

            List<int> mini_grid_sudoku = new List<int>();
            for (int i = 3 * interval_mini_grid_x - 2; i < interval_mini_grid_x * 3 + 1; i++)
            {
                for (int j = 3 * interval_mini_grid_y - 2; j < interval_mini_grid_y * 3 + 1; j++)
                {
                    if ((assignement.sudoku[i - 1, j - 1]) != 0)
                    {
                        domaine.Remove(assignement.sudoku[i - 1, j - 1]);
                    }
                }
            }
               
            return domaine;
        }

        private bool RecursiveBacktracking()
        {
            // Si le sudoku est complet alors on termine l'algorithme
            if (assignement.Get_complete()) { return true;  }

            // Sélection d'une variable vide
            Tuple<int,int> var_position = MRV(assignement);
            //Console.WriteLine("var_position = " + var_position);
            foreach (int value in csp.domaine)
            {/*
                Console.WriteLine("var_position = " + var_position);
                Console.WriteLine("sudoku");
                for (int i = 0; i < assignement.sudoku.GetLength(0); i++)
                {
                    for (int j = 0; j < assignement.sudoku.GetLength(1); j++)
                    {
                        Console.Write(assignement.sudoku[i, j]);
                    }
                    Console.WriteLine();
                }*/
                // Test de la consistance avec les contraintes
                if (csp.Is_assignement_consistent(value,var_position.Item1, var_position.Item2, assignement.sudoku))
                {
                    assignement.Set_variable_in_sudoku(value, var_position.Item1, var_position.Item2);
                    bool result = RecursiveBacktracking();
                    if (result) { return true; }
                    assignement.Reset_variable_in_sudoku(var_position.Item1, var_position.Item2);
                }

            }



            return false;
        }


        



        /*        private List<int> OrderDomainValues()
                {
                    //Pour l'instant, OrderDomainValues fournit une liste d'entier
                    //A voir pour qu'il fournisse CSP.domaine
                    return new List<int>() { 1, 2, 3, 3, 4, 5, 6, 7, 8, 9 };
                }

        /*        private Cell SelectUnassignedVariable()
                {
                    //SelectUnassignedVariable(CSP.variables, assignment, CSP)

            }*/
    }
}
