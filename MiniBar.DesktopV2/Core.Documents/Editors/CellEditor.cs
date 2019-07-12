using System;
using Core.Documents.Excel.Worksheets;
using DevExpress.Spreadsheet;

namespace Core.Documents.Editors
{
    public abstract class CellEditor
        
    {

        public abstract void Init(Worksheet mainWorksheet, ListWorksheet listWorksheet, Range range);

        public abstract object Parse(CellValue cellValue, Type type);
        public T Parse<T>(CellValue cellValue)
            where T: class
        {
            return Parse(cellValue, typeof(T)) as T;
        }
    }
}
