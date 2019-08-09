using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.ErrorHandling
{
    public static class ExceptionHandlingExtensions
    {
        public static IContainerRegistry AddDefaultExceptionHandling(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(((IContainerExtension)containerRegistry).Resolve<BaseExceptionHandler>());
            return containerRegistry;
        }
    }
}
