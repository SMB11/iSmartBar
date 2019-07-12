using System;

namespace Infrastructure.DX.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GridColumnAttribute : Attribute
    {
        public GridColumnAttribute(string header = null, int order = 0)
        {
            Header = header;
            Order = order;
        }

        public string Header { get; set; }

        public int Order { get; set; }

        public string FieldName { get; set; }
        public bool IsSortField { get; set; }
        public DevExpress.Xpf.Core.BestFitMode BestFitMode { get; set; }
    }
}
