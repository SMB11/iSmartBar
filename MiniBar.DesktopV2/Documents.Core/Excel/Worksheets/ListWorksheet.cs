using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Excel.Worksheets
{
    public class ListWorksheet
    {
        public Worksheet Worksheet { get; }

        private int avalilableColumn = 0;

        public ListWorksheet(Worksheet worksheet)
        {
            Worksheet = worksheet;
            worksheet.Name = "Lists";
            worksheet.Visible = false;
            worksheet.Protect("worksheetpass", WorksheetProtectionPermissions.InsertColumns | WorksheetProtectionPermissions.DeleteRows | WorksheetProtectionPermissions.DeleteColumns | WorksheetProtectionPermissions.InsertColumns);
        }

        public Range AddList(List<string> list)
        {
            int i;
            for (i = 0; i < list.Count; i++)
            {
                string item = list[i];
                Worksheet[i, avalilableColumn].SetValue(item);
            }
            Range range = Worksheet.Range.FromLTRB(avalilableColumn, 0, avalilableColumn, i-1).GetRangeWithAbsoluteReference();
            avalilableColumn++;
            return range;
        }
    }
}
