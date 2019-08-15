﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documents.Converters.Cell;

using Documents.Excel.Worksheets;
using DevExpress.Spreadsheet;

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
            if(DataValidation  != null)
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