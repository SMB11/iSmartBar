using Documents.Editors;
using System.Reflection;

namespace Documents.Metadata
{
    public abstract class ColumnMetadata
    {
        public ColumnMetadata(PropertyInfo info, string name)
        {
            PropertyInfo = info;
            Name = name;
            CellEditor = new CellTextEditor();
        }


        public ColumnMetadata(PropertyInfo info1, PropertyInfo info2, string name) : this(info1, name)
        {
            PropertyInfo2 = info2;
        }

        public PropertyInfo PropertyInfo { get; private set; }

        public PropertyInfo PropertyInfo2 { get; private set; }

        public string Name { get; private set; }

        public abstract ColumnType ColumnType { get; }

        public CellEditor CellEditor { get; set; }
    }
}
