using DevExpress.Spreadsheet;
using System;

namespace Documents.Converters.Cell
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
