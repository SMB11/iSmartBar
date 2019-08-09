using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents.Converters
{
    public interface ICellConverter
    {
        object Convert(CellValue value);
    }
}
