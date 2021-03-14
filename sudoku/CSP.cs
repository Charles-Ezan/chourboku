using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class CSP
    {

        // Toutes les variables du sudoku (non affectée et affectée)
        public List<Variable> variables = new List<Variable>();


        /*        // Domaine
                private List<int> domaine = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                //Contraintes
                List<int> row_constraints = new List<int>();
                List<int> column_constraints = new List<int>();
                List<int> mini_grid_constraints = new List<int>();
        */
        // Initialisation des variables du CSP
        /*        public CSP(int[,] sudoku)
                {
                    for (int i = 0; i < sudoku.GetLength(0); i++)
                    {
                        for (int j = 0; j < sudoku.GetLength(1); j++)
                        {
                            if (sudoku[i, j] == 0)
                            {
                                variables.Add(new Variable(Tuple.Create(i, j)));
                            }
                        }
                    }
                    foreach (var variable in variables)
                    {
                        Binary_initial_constraint_maker(variable.position, sudoku);
                    }

                    Console.WriteLine("Nombre de variables : " + variables.Count);
                }*/

        public CSP(int[,] sudoku)
        {
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    Variable a_variable = new Variable(Tuple.Create(i, j));

                    if(sudoku[i,j] != 0) { 
                        a_variable.domain = new List<int>() { sudoku[i, j] };
                        a_variable.value = sudoku[i, j];
                    }
                    else
                    {
                        a_variable.domain = Find_Domain(i,j,sudoku);//new List<int>() { 1,2,3,4,5,6,7,8,9 };

                    }

                    variables.Add(a_variable);

                }
            }
            foreach (var variable in variables)
            {
                Binary_initial_constraint_maker(variable.position, sudoku);
            }

        }



        public CSP() { }

        public CSP(CSP csp)
        {
            this.variables = csp.variables;
/*            foreach (Variable a_var in csp.variables)
            {
                variables.Add(a_var);

            }*/
/*            Display_csp_element();*/
        }

        // Récupérer le domaine d'une variable
        public List<int> Get_domain_of_variable(Tuple<int, int> a_variable_position)
        {
            List<int> domain = new List<int>();
            foreach (var variable in variables)
            {
                if (variable.position.Equals(a_variable_position))
                {
                    domain = variable.Get_domain();
                    break;
                }
            }
            return domain;
        }

        // Récupérer une variable à partir de sa position
        public Variable Get_variable_from_position(Tuple<int, int> a_variable_position)
        {
            Variable a_variable = new Variable();
            foreach (var variable in variables)
            {
                if (variable.position.Equals(a_variable_position))
                {
                    a_variable = variable;
                    break;
                }
            }
            return a_variable;
        }

        // Fonction d'affichage (debug)
        public void Display_csp_element()
        {
            Console.WriteLine("Fonction d'affichage !");
            Console.WriteLine("Nombre dans les domaines des variables !");
            Console.WriteLine("Nombre de variables : " + variables.Count);
            foreach (var variable in variables)
            {
                Console.Write(variable.domain.Count + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Nombre de contraintes des variables !");
            foreach (var variable in variables)
            {
                Console.Write(variable.binary_contraints.Count + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Nombre de voisins des variables !");
            foreach (var variable in variables)
            {
                Console.Write(variable.neighbours.Count + " ");
            }
            Console.WriteLine();
        }

        // Ajouter un nouveau domaine à une variable à partir de sa position
        public void Set_domain_of_variable(Tuple<int, int> a_variable_position, List<int> new_domain)
        {
            foreach (var variable in variables)
            {
                if (variable.position.Equals(a_variable_position))
                {
                    variable.domain = new_domain;
                    break;
                }
            }
        }

        // Trouver le domaine d'une variable
        public List<int> Find_Domain(int value_x, int value_y, int[,] sudoku)
        {
            List<int> domaine = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < sudoku.GetLength(1); i++)
            {
                if (sudoku[i, value_y] != 0)
                {
                    domaine.Remove(sudoku[i, value_y]);
                }

                if (sudoku[value_x, i] != 0)
                {
                    domaine.Remove(sudoku[value_x, i]);
                }
            }

            int interval_mini_grid_x = 0;
            int interval_mini_grid_y = 0;

            for (int n = 1; n <= (sudoku.GetLength(0) / 3); n++)
            {
                if ((1 * n <= value_x + 1) && (value_x + 1 <= 3 * n))
                {
                    interval_mini_grid_x = n;
                    break;
                }
            }

            for (int k = 1; k <= (sudoku.GetLength(0) / 3); k++)
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
                    if ((sudoku[i - 1, j - 1]) != 0)
                    {
                        domaine.Remove(sudoku[i - 1, j - 1]);
                    }
                }
            }

            return domaine;
        }




        // Test de la valeur avant de l'assigner au sudoku
        public bool Is_assignement_consistent(int value_tested, Tuple<int, int> variable_position, int[,] sudoku)
        {
            // Test des contraintes
            List<Tuple<int, int>> contraints = Get_variable_from_position(variable_position).neighbours;
            foreach (var contraint in contraints)
            {
                if (value_tested == sudoku[contraint.Item1, contraint.Item2])
                {
                    return false;
                }
            }
            return true;
        }


        // Fabrication contraintes binaire mini sudoku 
        public List<Tuple<int, int>> Mini_grid_binary_constraints_maker(int value_x, int value_y, int[,] sudoku)
        {
            int interval_mini_grid_x = 0;
            int interval_mini_grid_y = 0;

            for (int n = 1; n <= (sudoku.GetLength(0) / 3); n++)
            {
                if ((1 * n <= value_x + 1) && (value_x + 1 <= 3 * n))
                {
                    interval_mini_grid_x = n;
                    break;
                }
            }

            for (int k = 1; k <= (sudoku.GetLength(0) / 3); k++)
            {
                if ((1 * k <= value_y + 1) && (value_y + 1 <= 3 * k))
                {
                    interval_mini_grid_y = k;
                    break;
                }
            }

            List<Tuple<int,int>> mini_grid_sudoku_constraint = new List<Tuple<int, int>>();
            for (int i = 3 * interval_mini_grid_x - 2; i < interval_mini_grid_x * 3 + 1; i++)
            {
                for (int j = 3 * interval_mini_grid_y - 2; j < interval_mini_grid_y * 3 + 1; j++)
                {
                    mini_grid_sudoku_constraint.Add(Tuple.Create(i - 1, j - 1));
                }
            }
            return mini_grid_sudoku_constraint;

        }


        // Fabrication contrainte binaire colonne
        public List<Tuple<int, int>> Column_binary_constraints_maker( int value_y, int[,] sudoku)
        {
            // Récupérer les contraintes sur la ligne correspondante
            List<Tuple<int, int>> column_binary_constraint = new List<Tuple<int, int>>();
            for (int i = 0; i < sudoku.GetLength(1); i++)
            {
                column_binary_constraint.Add(Tuple.Create(i, value_y));
            }
            return column_binary_constraint;
        }


        // Test de la contrainte de ligne
        public List<Tuple<int, int>> Row_binary_constraints_maker(int value_x, int[,] sudoku)
        {
            // Récupérer les contraintes sur la ligne correspondante
            List<Tuple<int, int>> row_binary_constraints = new List<Tuple<int, int>>();
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                row_binary_constraints.Add(Tuple.Create(value_x, i));

            }
            return row_binary_constraints;
        }

        // Fabrication des contraintes binaires
        public void Binary_initial_constraint_maker(Tuple<int, int> var_position, int[,] sudoku)
        {
            List<Tuple<Tuple<int, int>, Tuple<int, int>>> list_binary_constraint = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();

            List<Tuple<int, int>> all_variable_constraints = new List<Tuple<int, int>>();
            all_variable_constraints = Row_binary_constraints_maker(var_position.Item1, sudoku);

            // Ajout des contraintes de colonne en élmininant les variables dupliqué
            all_variable_constraints = Lists_merger(all_variable_constraints, Column_binary_constraints_maker(var_position.Item2, sudoku));

            // Ajout des contraintes de colonne en élmininant les variables dupliqué
            all_variable_constraints = Lists_merger(all_variable_constraints, Mini_grid_binary_constraints_maker(var_position.Item1, var_position.Item2, sudoku));



            foreach (Tuple<int, int> variable_constraint in all_variable_constraints)
            {
                if (!variable_constraint.Equals(var_position)) { 
                    list_binary_constraint.Add(Tuple.Create(var_position, variable_constraint));
                    Get_variable_from_position(var_position).neighbours.Add(variable_constraint);
                }
            }
            Get_variable_from_position(var_position).binary_contraints = list_binary_constraint;
        }

        public List<Tuple<Tuple<int, int>, Tuple<int, int>>> Get_a_list_of_all_binary_constraints()
        {
            List<Tuple<Tuple<int, int>, Tuple<int, int>>> list_binary_constraint = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();

            foreach(var variable in variables)
            {
                list_binary_constraint.AddRange(variable.binary_contraints);
            }
            return list_binary_constraint;
        }


        public List<Tuple<int, int>> Lists_merger(List<Tuple<int,int>> list_1, List<Tuple<int, int>> list_2)
        {
            List<Tuple<int, int>> list_merged = new List<Tuple<int, int>>(list_1);
            List<Tuple<int, int>> list_reduced = new List<Tuple<int, int>>(list_2);

            for(int i=0; i<list_merged.Count; i++)
            {
                for (int j=0; j< list_reduced.Count; j++)
                {
                    if(list_reduced[j].Equals(list_merged[i]))
                    {
                        list_reduced.RemoveAt(j);
                    }
                }
            }
            // Concaténation des deux listes sans répétitions
            list_merged.AddRange(list_reduced);

            return list_merged;
        }
    }
}
