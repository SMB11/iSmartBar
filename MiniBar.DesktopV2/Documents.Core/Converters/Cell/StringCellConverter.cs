using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;

namespace Documents.Converters.Cell
{
    class StringCellConverter : ICellConverter
    {
        public object Convert(CellValue value)
        {
            return value.ToString();
        }
    }
}
