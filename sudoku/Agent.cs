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

        public int performance_measure = 0;

        private CSP csp;


        // Initialisation du CSP et de l'assignement
        public void Initialize_assignement(int[,] sudoku)
        {
            assignement.Initialize_sudoku(sudoku);
            csp = new CSP(sudoku);
        }


        public Assignement Get_asssignement()
        {
            return assignement;
        }

        public bool BacktrackingSearch()
            {
                return RecursiveBacktracking(csp);
            }

        // Retourne la position d'un élément non sélectionné
        private Tuple<int,int> Select_unassigned_variable()
        {
            for(int i=0; i<assignement.sudoku.GetLength(0); i++)
            {
                for(int j=0; j<assignement.sudoku.GetLength(1); j++)
                {
                    if(assignement.sudoku[i,j] == 0)
                    {
                        return Tuple.Create(i, j);

                    }
                }
            }
            return Tuple.Create(100,100);
        }
        // MRV - Choix de la variable avec le plus petit nombre de valeurs légales
        public Tuple<int, int> MRV(List<Variable> variables)
        {
            int x_pos = 0;
            int y_pos = 0;

            int min = assignement.sudoku.GetLength(0);

            foreach (Variable a_variable in variables)
            {
                if(a_variable.value == 0)
                {
                    if(a_variable.domain.Count < min)
                    {
                        min = a_variable.domain.Count;
                        x_pos = a_variable.position.Item1;
                        y_pos = a_variable.position.Item2;
                    }
                }
            }
            return Tuple.Create(x_pos, y_pos);

        }

/*        // Mettre à jour le domaine d'une variable
        public List<int> Find_Domain(int value_x, int value_y, Assignement assignement)
        {
            List<int> domaine = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

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
        }*/


        // Fonction récursive du Backtracking
        private bool RecursiveBacktracking(CSP a_csp)
        {
            // Incrémentation de la valeur de performance à chaque appelle
            performance_measure++;

            // Si le sudoku est complet alors on termine l'algorithme
            if (assignement.Get_complete()) { return true;  }

            // Sélection d'une variable vide
            Tuple<int, int> var_position = MRV(a_csp.variables);

            /*Tuple<int, int> var_position = Select_unassigned_variable();*/

            foreach (int value in a_csp.Get_domain_of_variable(var_position))
            {

                // Test de la consistance avec les contraintes
                if (a_csp.Is_assignement_consistent(value, var_position, assignement.sudoku))
                {
                    assignement.Set_variable_in_sudoku(value, var_position.Item1, var_position.Item2);
                    // Fonction Find domain

                    // Optimisation Ac-3
                    CSP new_csp = new CSP(assignement.sudoku);
                    new_csp = Ac_3(new_csp);


                    bool result = RecursiveBacktracking(new_csp);
                    if (result) { return true; }
                    assignement.Reset_variable_in_sudoku(var_position.Item1, var_position.Item2);
                }
            }
            return false;
        }

        // Algorithme de propagation de contrainte AC-3
        public CSP Ac_3(CSP a_csp)
        {
            CSP the_csp = a_csp;
            
            Queue<Tuple<Tuple<int, int>, Tuple<int, int>>> queue = new Queue<Tuple<Tuple<int, int>, Tuple<int, int>>>();

            // Ajout de tous les arcs du csp à la queue
            foreach (var element in csp.Get_a_list_of_all_binary_constraints())
            {
                queue.Enqueue(element);
            }            

            while (queue.Count != 0)
            {
                Tuple<Tuple<int, int>, Tuple<int, int>> arc_tested = queue.Dequeue();
                if (Remove_inconsistent_values(arc_tested,the_csp))
                {

                    foreach (var neighbor in the_csp.Get_variable_from_position(arc_tested.Item1).neighbours)
                    {
                        queue.Enqueue(Tuple.Create(neighbor, arc_tested.Item1));
                    }
                }
            }
            return the_csp;
        }

        private bool Remove_inconsistent_values(Tuple<Tuple<int,int>,Tuple<int, int>> a_couple, CSP a_csp)
        {
            bool removed = false;
            bool is_the_constraint_satisfied = false;

            List<int> first_element_domain = new List<int>(a_csp.Get_domain_of_variable(a_couple.Item1));
            List<int> second_element_domain = new List<int>(a_csp.Get_domain_of_variable(a_couple.Item2));

            for (int i=0; i< first_element_domain.Count; i++)
            {
                for (int j = 0; j < second_element_domain.Count; j++)
                {
                    if (first_element_domain[i] != second_element_domain[j])
                    {
                        is_the_constraint_satisfied = true;
                    }
                }
                if (!is_the_constraint_satisfied)
                {
                    first_element_domain.RemoveAt(i);
                    removed = true;
                }
                is_the_constraint_satisfied = false;
            }
            // On applique les modifications
            a_csp.Set_domain_of_variable(a_couple.Item1, first_element_domain);

            return removed;
        }
    }
}
