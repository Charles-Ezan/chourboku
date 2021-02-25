using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    public class Cell
    {
        public int number_in_cell;
        public DataGridViewCell cell;

        public Cell(DataGridViewCell new_cell)
        {
            cell = new_cell;
        }

        public int get_number_in_cell()
        {
            return number_in_cell;
        }

        public void cell_display()
        {
            if (number_in_cell == 0)
            {
                cell.Value = " ";
            }

            else
            {
                cell.Value = "{$number}";
            }
        }
    }
}
