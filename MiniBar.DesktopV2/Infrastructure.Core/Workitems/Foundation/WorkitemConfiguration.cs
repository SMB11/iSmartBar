using Infrastructure.Interface;
using System;
using System.Collections.Generic;

namespace Infrastructure.Workitems
{
    /// <summary>
    /// Configuration implementation for WorkitemBase
    /// </summary>
    internal class WorkitemConfiguration : IConfiguration
    {
        Dictionary<Type, IOption> Options = new Dictionary<Type, IOption>();
        Dictionary<Type, IOption> DefaultOptions = new Dictionary<Type, IOption>();

        /// <summary>
        /// Get the type of the option that is the first interface that implements IOption but is not equal to it
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private Type GetOptionType(IOption option)
        {
            foreach (Type inter in option.GetType().GetInterfaces())
            {
                if (typeof(IOption).IsAssignableFrom(inter) && !inter.Equals(typeof(IOption)))
                    return inter;
            }
            return option.GetType();
        }

        /// <summary>
        /// Adds default options for this specific configuration
        /// Default options are returned if no option of the type is configured
        /// </summary>
        /// <param name="option">Default options</param>
        public void ConfigureDefault(IOption option)
        {

            Type optionType = GetOptionType(option);
            if (DefaultOptions.ContainsKey(optionType))
                throw new ArgumentException(String.Format("Default option can only be configured once. Option type: {0}.", optionType.FullName));

            DefaultOptions.Add(optionType, option);
        }

        /// <summary>
        /// Adds options to the configuration instance
        /// </summary>
        /// <param name="option">Options</param>
        public void Configure(IOption option)
        {
            Type optionType = GetOptionType(option);
            if (!Options.ContainsKey(optionType))
                Options.Add(optionType, option);
            else
                Options[optionType] = option;
        }

        /// <summary>
        /// Get the option of type T
        /// </summary>
        /// <typeparam name="T">Type of the option</typeparam>
        /// <returns>The option if configured</returns>
        public T GetOption<T>() where T : IOption
        {
            if (!Options.ContainsKey(typeof(T)))
            {
                if (!DefaultOptions.ContainsKey(typeof(T)))
                    throw new ArgumentException(String.Format("Option of type {0} isn't configured.", typeof(T).FullName));
                else
                    return (T)DefaultOptions[typeof(T)];
            }
            return (T)Options[typeof(T)];
        }
    }
}
