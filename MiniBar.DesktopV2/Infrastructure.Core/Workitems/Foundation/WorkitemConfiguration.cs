using Infrastructure.Interface;
using System;
using System.Collections.Generic;

namespace Infrastructure.Workitems
{
    public class WorkitemConfiguration : IConfiguration
    {
        

        Dictionary<Type, IOption> Options = new Dictionary<Type, IOption>();
        Dictionary<Type, IOption> DefaultOptions = new Dictionary<Type, IOption>();

        private Type GetOptionType(IOption option)
        {
            foreach (Type inter in option.GetType().GetInterfaces())
            {
                if (typeof(IOption).IsAssignableFrom(inter) && !inter.Equals(typeof(IOption)))
                    return inter;
            }
            return option.GetType();
        }

        public void ConfigureDefault(IOption option)
        {

            Type optionType = GetOptionType(option);
            if (DefaultOptions.ContainsKey(optionType))
                throw new ArgumentException(String.Format("Default option can only be configured once. Option type: {0}.", optionType.FullName));

            DefaultOptions.Add(optionType, option);
        }

        public void Configure(IOption option)
        {
            Type optionType = GetOptionType(option);
            if (!Options.ContainsKey(optionType))
                Options.Add(optionType, option);
            else
                Options[optionType] = option;
        }
        
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
