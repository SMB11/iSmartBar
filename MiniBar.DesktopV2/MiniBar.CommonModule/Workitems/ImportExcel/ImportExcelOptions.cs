using DevExpress.Spreadsheet;
using System;
using System.IO;

namespace MiniBar.Common.Workitems.ImportExcel
{
    public class ImportExcelOptions
    {
        public ImportExcelOptions(Func<DocumentFormat, Stream> getTemplateStreamFunc)
        {
            GetTemplateStreamFunc = getTemplateStreamFunc;
        }

        public ImportExcelOptions(Func<DocumentFormat, Stream> getTemplateStreamFunc, string name) : this(getTemplateStreamFunc)
        {
            TemplateName = name;
        }

        public Func<DocumentFormat, Stream> GetTemplateStreamFunc { get; }
        public string TemplateName { get; }
    }
}
