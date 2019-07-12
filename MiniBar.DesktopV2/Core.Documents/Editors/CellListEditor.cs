using Core.Documents.Excel;
using Core.Documents.Excel.Worksheets;
using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Documents.Editors
{
    public class CellListEditor<T> : CellEditor
    {
        Expression<Func<T, int>> IDExpression { get; set; }
        Expression<Func<T, string>> ValueExpression { get; set; }

        List<T> List { get; set; }

        public CellListEditor(List<T> list, Expression<Func<T, int>> idExpresion, Expression<Func<T, string>> valueExpression)
        {
            IDExpression = idExpresion;
            ValueExpression = valueExpression;
            List = list;
        }


        public override void Init(Worksheet mainWorksheet, ListWorksheet listWorksheet, Range range)
        {
            Range listRange = listWorksheet.AddList(List.Select(o => ValueExpression.Compile().Invoke(o)).ToList());
            mainWorksheet.DataValidations.Add(
                range,
                DataValidationType.List,
                ValueObject.FromRange(listRange)
            );
        }

        private T FindByID(int id)
        {
            foreach(T item in List)
            {
                if (IDExpression.Compile().Invoke(item) == id)
                    return item;
            }
            return default(T);
        }


        private T FindByName(string name)
        {
            foreach (T item in List)
            {
                if (ValueExpression.Compile().Invoke(item) == name)
                    return item;
            }
            return default(T);
        }

        public override object Parse(CellValue cellValue, Type type)
        {
            string name = cellValue.ToString();
            T found = FindByName(name);
            if (found != null)
            {
                if (type == typeof(int) || type == typeof(int?))
                {
                    return IDExpression.Compile().Invoke(found);
                }
                else if (type == typeof(string))
                {
                    return name;
                }
                else if (type == typeof(T))
                {
                    return found;
                }
            }
            else
                throw new ArgumentException("Not found in list");
            throw new ArgumentException("Type not supported");
        }
    }
}
