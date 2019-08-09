using DevExpress.Mvvm;
using Documents.Adapter;
using Documents.Excel;
using System.Reflection;

namespace Infrastructure.Office
{
    public class ExcelDocument<T> : ExcelDocumentBase<T>
        where T : class, new()
    {
        public ExcelDocument(DocumentAdapter<T> adapter) : base(adapter)
        {
        }

        protected override string GetErrorText(object obj, PropertyInfo propertyInfo)
        {
            return IDataErrorInfoHelper.GetErrorText(obj, propertyInfo.Name);
        }
    }
}
