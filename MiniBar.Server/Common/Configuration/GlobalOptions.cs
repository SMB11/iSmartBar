using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Configuration
{
    public class GlobalOptions
    {
        public int MaxAllowedArrayLength { get; set; }

        public List<string> SupportedCultures { get; set; }

        public List<CultureInfo> SupportedCulturesFull
        {
            get
            {
                List<CultureInfo> res = new List<CultureInfo>();
                foreach(string cult in SupportedCultures)
                {
                    res.Add(CultureInfo.GetCultureInfo(cult));
                }
                return res;
            }
        }
    }
}
