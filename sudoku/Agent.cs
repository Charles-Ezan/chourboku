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
            //possibilities = OrderDomainValues(var, assignment, csp)
            //foreach (int value in possibilities)
            //{
            //
            //
            //
            //
            //
            //
            //
            //
            return true;
        }

/*        private List<int> OrderDomainValues()
        {

        }*/

/*        private Cell SelectUnassignedVariable()
        {
            //SelectUnassignedVariable(CSP.variables, assignment, CSP)

        }*/
    }
}
