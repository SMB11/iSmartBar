using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ErrorHandling
{
    /// <summary>
    /// Extensions for type <see cref="IExceptionHandler"/>
    /// </summary>
    public static class ExceptionHandlingExtensions
    {
        /// <summary>
        /// Adds the default exception handling type definitions to the Container Registry
        /// </summary>
        /// <param name="containerRegistry">The Container Registry</param>
        /// <returns></returns>
        public static IContainerRegistry AddDefaultExceptionHandling(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IExceptionHandler>(((IContainerExtension)containerRegistry).Resolve<BaseExceptionHandler>());
            return containerRegistry;
        }
    }
}
