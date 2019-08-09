using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interface
{
    /// <summary>
    /// Abstraction over implementation framework's Window
    /// </summary>
    public interface IWindow
    {

        void Show();

        bool? ShowDialog();

        void Focus();

        void Unfocus();

        void Close();

    }
}
