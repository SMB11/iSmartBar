using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Exceptions
{
    public class ExcelParseException : Exception
    {
        public ExcelParseException(int column, int row) : base(String.Format("Invalid data at row {0} and column {1}", row, column))
        {
            Column = column;
            Row = row;
            
        }

        public ExcelParseException(int column, int row, string error) : base(String.Format("Invalid data at row {0} and column {1}. {2}", row, column, error))
        {
            Column = column;
            Row = row;
        }

        public int Column { get; private set; }

        public int Row { get; private set; }


    }
}
