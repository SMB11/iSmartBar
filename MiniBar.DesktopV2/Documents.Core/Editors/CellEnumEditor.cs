using DevExpress.Spreadsheet;
using Documents.Excel.Worksheets;
using System;
using System.Linq;

namespace Documents.Editors
{
    public class CellEnumEditor<T> : CellEditor
    {
        public CellEnumEditor()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type T must be enum");
        }

        public override void Init(Worksheet mainWorksheet, ListWorksheet listWorksheet, Range range)
        {
            string[] names = Enum.GetNames(typeof(T));
            Range listRange = listWorksheet.AddList(names.ToList());
            mainWorksheet.DataValidations.Add(
                range,
                DataValidationType.List,
                ValueObject.FromRange(listRange)
            );
        }

        public override object Parse(CellValue cellValue, Type type)
        {
            string name = cellValue.ToString();
            return Enum.Parse(type, name);

        }
    }
}
