using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class DataErrorInfoExtenisons
    {
        public static bool HasErrors(this IDataErrorInfo dataErrorInfo)
        {
            return IDataErrorInfoHelper.HasErrors(dataErrorInfo);
        }
    }
}
