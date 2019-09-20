using Infrastructure.Modularity;
using System;

namespace Infrastructure.Interface
{
    /// <summary>
    /// A view container interface
    /// </summary>
    public interface IViewContainer : IDisposable
    {

        /// <summary>
        /// Register a view in a specific screen region
        /// </summary>
        /// <param name="view">the view to register</param>
        /// <param name="region">the region to register in</param>
        /// <returns>the view</returns>
        object Register(object view, ScreenRegion region);

        /// <summary>
        /// Unregister a view from a specific screen region
        /// </summary>
        /// <param name="view">the view to unregister</param>
        /// <param name="region">the region to unregister from</param>
        void Unregister(object view, ScreenRegion region);

        /// <summary>
        /// Import views from another container
        /// </summary>
        /// <param name="viewContainer">Another container</param>
        void ImportFrom(IViewContainer viewContainer);

    }
}
