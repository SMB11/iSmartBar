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
