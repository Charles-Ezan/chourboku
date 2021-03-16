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
        public int value;

        public Variable(Tuple<int,int> new_position)
        {
            position = new_position;
        }


        public Variable()
        {}

    }
}
