using DevExpress.Spreadsheet;
using Documents.Converters.Cell;
using Documents.Excel.Worksheets;
using System;

namespace Documents.Editors
{
    public class CellTextEditor : CellEditor
    {
        public CellTextEditor()
        {

        }

        public CellTextEditor(DataValidation dataValidation)
        {
            DataValidation = dataValidation;
        }

        public DataValidation DataValidation { get; private set; }

        public override void Init(Worksheet mainWorksheet, ListWorksheet listWorksheet, Range range)
        {
            if (DataValidation != null)
                mainWorksheet.DataValidations.Add(
                    range,
                    DataValidation.ValidationType,
                    DataValidation.Operator,
                    DataValidation.Criteria,
                    DataValidation.Criteria2
                );
        }

        public override object Parse(CellValue cellValue, Type type)
        {
            return new CellConverter(type).Convert(cellValue);
        }
    }
}
