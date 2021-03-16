using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class CSP
    {

        // Toutes les variables du sudoku (non affectée et affectée) - Avec la position et la valeur de la variable
        public List<Variable> variables = new List<Variable>();

        // Listes de domaines associées aux variables
        public List<List<int>> domains = new List<List<int>>();

        // Listes de contraintes binaires associées aux variables
        public List<List<Tuple<Tuple<int, int>, Tuple<int, int>>>> binary_contraints = new List<List<Tuple<Tuple<int, int>, Tuple<int, int>>>>();

        // Liste des voisins associées aux variables
        public List<List<Tuple<int, int>>> neighbours = new List<List<Tuple<int, int>>>();

        // Constructeur
        public CSP(int[,] sudoku)
        {
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    Variable a_variable = new Variable(Tuple.Create(i, j));

                    if (sudoku[i, j] != 0)
                    {
                        domains.Add(new List<int>() { sudoku[i, j] });
                        a_variable.value = sudoku[i, j];
                    }
                    else
                    {
                        domains.Add(Find_Domain(i, j, sudoku));

                    }

                    variables.Add(a_variable);
                }
            }

        }

        public CSP(int[,] sudoku, CSP csp)
        {
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    Variable a_variable = new Variable(Tuple.Create(i, j));

                    if (sudoku[i, j] != 0)
                    {
                        domains.Add(new List<int>() { sudoku[i, j] });
                        a_variable.value = sudoku[i, j];
                    }
                    else
                    {
                        domains.Add(Find_Domain(i, j, sudoku));

                    }

                    variables.Add(a_variable);
                }
            }
            binary_contraints = csp.binary_contraints;
            neighbours = csp.neighbours;

        }

        // Récupérer le domaine d'une variable
        public List<int> Get_domain_of_variable(Tuple<int, int> a_variable_position)
        {
            List<int> domain = new List<int>();
            for(int i=0; i< variables.Count; i++)
            {
                if (variables[i].position.Equals(a_variable_position))
                {
                    domain = domains[i];
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

        // Ajouter un nouveau domaine à une variable à partir de sa position
        public void Set_domain_of_variable(Tuple<int, int> a_variable_position, List<int> new_domain)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                if (variables[i].position.Equals(a_variable_position))
                {
                    domains[i] = new_domain;
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
            // On récupère la valeur des voisins pour vérifier que la valeur que l'on souhaite assigner n'est pas déjà prise
            int index_var = Get_index_of_variable_from_position(variable_position);

            List<Tuple<int, int>> contraints = neighbours[index_var];
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

        // Récupère l'index de la variable recherchée
        public int Get_index_of_variable_from_position(Tuple<int,int> var_position)
        {
            int index = 0;
            for (int i=0; i<variables.Count; i++)
            {
                if (variables[i].position.Equals(var_position))
                {
                    index = i;
                    break;
                }
            }
            
            return index;
        }

        // Création des contraintes binaires
        public void Binary_initial_constraint_maker(Tuple<int, int> var_position, int[,] sudoku)
        {
            List<Tuple<Tuple<int, int>, Tuple<int, int>>> list_binary_constraint = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();

            List<Tuple<int, int>> all_variable_constraints = new List<Tuple<int, int>>();
            all_variable_constraints = Row_binary_constraints_maker(var_position.Item1, sudoku);

            // Ajout des contraintes de colonne en élmininant les variables dupliquées
            all_variable_constraints = Lists_merger(all_variable_constraints, Column_binary_constraints_maker(var_position.Item2, sudoku));

            // Ajout des contraintes de colonne en élmininant les variables dupliquées
            all_variable_constraints = Lists_merger(all_variable_constraints, Mini_grid_binary_constraints_maker(var_position.Item1, var_position.Item2, sudoku));

            int index_var = Get_index_of_variable_from_position(var_position);

            neighbours.Add(new List<Tuple<int, int>>());
            foreach (Tuple<int, int> variable_constraint in all_variable_constraints)
            {
                if (!variable_constraint.Equals(var_position))
                {
                    list_binary_constraint.Add(Tuple.Create(var_position, variable_constraint));
                    neighbours[index_var].Add(variable_constraint);
                }
            }
            binary_contraints.Add(list_binary_constraint);
        }

        // Récupère une liste de toutes les contraintes binaires du sudoku
        public List<Tuple<Tuple<int, int>, Tuple<int, int>>> Get_a_list_of_all_binary_constraints()
        {
            List<Tuple<Tuple<int, int>, Tuple<int, int>>> list_binary_constraint = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();

            foreach(var binary_contraint in binary_contraints)
            {
                list_binary_constraint.AddRange(binary_contraint);
            }
            return list_binary_constraint;
        }

        // Fusionne deux listes en retirant les éléments redondants
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
