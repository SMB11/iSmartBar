using System.Collections.Generic;
using System.Reflection;

namespace Documents.Metadata
{
    public class MultiColumnMetadata : ColumnMetadata
    {
        public MultiColumnMetadata(PropertyInfo info, string name, List<string> headers) : base(info, name)
        {

            Headers = headers;
        }

        public List<string> Headers { get; private set; }

        public override ColumnType ColumnType => ColumnType.MultiColumn;
    }
}
