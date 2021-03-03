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
        public bool BacktrackingSearch()
        {
            return RecursiveBacktracking();
        }

        private bool RecursiveBacktracking()
        {
            //foreach (var cell in CSP.variables)
            //{
            //  if (cell.value == 0)
            //  {
            //      continue;
            //  }
            //  return assignment;
            //}
            //var candidate = SelectUnassignedVariable(CSP.variables, assignment, CSP)
            List<int> possibilities = OrderDomainValues();
            foreach (int value in possibilities)
            {
                //bool respectConstraints = CSP.Constraints(var, value, assignment);
                bool respectConstraints = true;
                if (respectConstraints)
                {
                    //add var = value to assignement
                    bool result = RecursiveBacktracking();
                    if (result)
                    {
                        return true;
                    }
                    //remove var = value to assignment
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

    }
}
