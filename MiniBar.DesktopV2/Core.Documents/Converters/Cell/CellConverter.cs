using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;

namespace Core.Documents.Converters.Cell
{
    public class CellConverter : ICellConverter
    {
        ICellConverter Converter;
        Type Type;

        public CellConverter(Type type)
        {
            Type = type;
            if(type == typeof(int) || type == typeof(double) || type == typeof(float))
            {
                Converter = new NumericCellConverter();
            }
            else if (type == typeof(string))
            {
                Converter = new StringCellConverter();
            }
        }

        public object Convert(CellValue value)
        {
            return System.Convert.ChangeType(Converter.Convert(value), Type);
        }
    }
}
