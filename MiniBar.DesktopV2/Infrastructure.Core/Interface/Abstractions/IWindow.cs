namespace Infrastructure.Interface
{
    /// <summary>
    /// Abstraction over implementation framework's Window
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Show the window
        /// </summary>
        void Show();

        /// <summary>
        /// Show the window in dialog mode
        /// </summary>
        bool? ShowDialog();

        /// <summary>
        /// Bring the window into view
        /// </summary>
        void Focus();

        /// <summary>
        /// Take the window out of view
        /// </summary>
        void Unfocus();

        /// <summary>
        /// Close the window
        /// </summary>
        void Close();

    }
}
