using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku
{
    class Variable
    {
        // Position de la variable dans le sudoku
        public Tuple<int, int> position;

        // Valeur de la variable
        public int value = 0;

        // Est ce une variable que l'on peut changer ?
        public bool fixed_variable = false;

        // Domaine de la variable
        public List<int> domain = new List<int>();
        /*public List<int> domain = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };*/

        // Liste des voisins de la variables
        public List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();

        // Liste des contraintes binaires
        public List<Tuple<Tuple<int, int>, Tuple<int, int>>> binary_contraints = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();


        public Variable(Tuple<int,int> new_position)
        {
            position = new_position;
        }

        public Variable()
        {}

        public List<int> Get_domain()
        {
            return domain;
        }
        public void Set_domain(List<int> new_domain)
        {
            domain = new_domain;
        }
    }
}
