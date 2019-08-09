using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interface
{
    public interface IConfiguration
    {
        void ConfigureDefault(IOption option);
        void Configure(IOption option);
        T GetOption<T>() where T : IOption;
    }
}
