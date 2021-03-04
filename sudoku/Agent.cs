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
        private Tuple<int,int> Select_unassigned_variable(Assignement assignement)
        {
            int value_x = 0;
            int value_y = 0;
            for(int i=0; i<assignement.sudoku.GetLength(0); i++)
            {
                for(int j=0; j<assignement.sudoku.GetLength(1); j++)
                {
                    if(assignement.sudoku[i,j] == 0)
                    {
                        return Tuple.Create(i,j);
                    }
                }
            }
            return Tuple.Create(value_x, value_y);

        }

        private bool RecursiveBacktracking()
        {
            // Si le sudoku est complet alors on termine l'algorithme
            if (assignement.Get_complete()) { return true;  }

            // Sélection d'une variable vide
            Tuple<int,int> var_position = Select_unassigned_variable(assignement);
/*            Console.WriteLine("var_position = " + var_position);*/
            foreach (int value in csp.domaine)
            {
/*                Console.WriteLine("var_position = " + var_position);
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
