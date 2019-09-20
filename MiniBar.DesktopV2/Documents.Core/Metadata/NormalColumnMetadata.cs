using System.Reflection;

namespace Documents.Metadata
{
    public class NormalColumnMetadata : ColumnMetadata
    {
        public NormalColumnMetadata(PropertyInfo info, string name) : base(info, name)
        {
        }

        public NormalColumnMetadata(PropertyInfo info1, PropertyInfo info2, string name) : base(info1, info2, name)
        {
        }

        public override ColumnType ColumnType => ColumnType.Normal;

    }
}
