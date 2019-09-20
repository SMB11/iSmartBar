using System;
using System.Collections.Generic;

namespace Infrastructure.Modularity
{
    /// <summary>
    /// Defines dynamic region names for every screen region
    /// </summary>
    public class DynamicRegionNames
    {

        public DynamicRegionNames()
        {
            // initialize Names dicitonary
            Names = new Dictionary<ScreenRegion, string>();
            Names.Add(ScreenRegion.Content, string.Format("{0}_{1}", Constants.RegionNames.ContentRegion, Guid.NewGuid().ToString()));
            Names.Add(ScreenRegion.Ribbon, string.Format("{0}_{1}", Constants.RegionNames.RibbonRegion, Guid.NewGuid().ToString()));
        }

        /// <summary>
        /// Get the dynamic name of a screen region
        /// </summary>
        /// <param name="forRegion">the screen region</param>
        /// <returns></returns>
        public string GetName(ScreenRegion forRegion)
        {
            if (Names.ContainsKey(forRegion))
                return Names[forRegion];
            return null;
        }

        // Dicitonary to keep track of names
        private Dictionary<ScreenRegion, string> Names;
    }
}
