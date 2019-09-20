namespace Infrastructure.Interface
{
    /// <summary>
    /// Configuration interface powered by options pattern
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Adds default options for this specific configuration
        /// Default options are returned if no option of the type is configured
        /// </summary>
        /// <param name="option">Default options</param>
        void ConfigureDefault(IOption option);
        /// <summary>
        /// Adds options to the configuration instance
        /// </summary>
        /// <param name="option">Options</param>
        void Configure(IOption option);
        /// <summary>
        /// Get the option of type T
        /// </summary>
        /// <typeparam name="T">Type of the option</typeparam>
        /// <returns>The option if configured</returns>
        T GetOption<T>() where T : IOption;
    }
}
