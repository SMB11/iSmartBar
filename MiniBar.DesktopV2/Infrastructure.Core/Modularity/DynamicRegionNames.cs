using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Modularity
{
    public class DynamicRegionNames
    {

        public DynamicRegionNames()
        {
            Names = new Dictionary<ScreenRegion, string>();
            Names.Add(ScreenRegion.Content, string.Format("{0}_{1}", Constants.RegionNames.ContentRegion, Guid.NewGuid().ToString()));
            Names.Add(ScreenRegion.Ribbon, string.Format("{0}_{1}", Constants.RegionNames.RibbonRegion, Guid.NewGuid().ToString()));
        }

        public string GetName(ScreenRegion forRegion)
        {
            if(Names.ContainsKey(forRegion))
                return Names[forRegion];
            return null;
        }

        private Dictionary<ScreenRegion, string> Names;
    }
}
