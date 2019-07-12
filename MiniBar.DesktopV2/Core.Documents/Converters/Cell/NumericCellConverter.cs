using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;

namespace Core.Documents.Converters.Cell
{
    class NumericCellConverter : ICellConverter
    {

        public object Convert(CellValue value)
        {
            if (!value.IsNumeric) throw new ArgumentException("Value must be numeric");
            return value.NumericValue;
        }
    }
}
