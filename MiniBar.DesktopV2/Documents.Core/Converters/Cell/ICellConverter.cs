using DevExpress.Spreadsheet;

namespace Documents.Converters
{
    public interface ICellConverter
    {
        object Convert(CellValue value);
    }
}
