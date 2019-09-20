using System.ComponentModel;

namespace Infrastructure.Interface
{
    public interface IDataErrorInfoExtended : IDataErrorInfo
    {
        bool HasErrors { get; }
    }
}
