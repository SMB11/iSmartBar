using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Infrastructure.Interface
{
    /// <summary>
    /// Command container
    /// </summary>
    public interface ICommandContainer : IDisposable
    {
        /// <summary>
        /// Register a command with the container
        /// </summary>
        /// <param name="name">Name of the command</param>
        /// <param name="command">The command to register</param>
        void Register(string name, ICommand command);
    }
}
