using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Documents.Validation
{
    public class DataValidationCriteria : DataValidation
    {
        public DataValidationCriteria(DataValidationType type, DataValidationOperator validationOperator, ValueObject criteria1)
        {
            ValidationType = type;
            Operator = validationOperator;
            Criteria = criteria1;
        }

        public Range Range { get; set; }
        public DataValidationType ValidationType { get; }
        public DataValidationOperator Operator { get; set; }
        public bool AllowBlank { get; set; }
        public DataValidationImeMode ImeMode { get; set; }
        public bool ShowDropDown { get; set; }
        public bool ShowInputMessage { get; set; }
        public string InputTitle { get; set; }
        public string InputMessage { get; set; }
        public bool ShowErrorMessage { get; set; }
        public DataValidationErrorStyle ErrorStyle { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorMessage { get; set; }
        public ValueObject Criteria { get; set; }
        public ValueObject Criteria2 { get; set; }
    }
}
