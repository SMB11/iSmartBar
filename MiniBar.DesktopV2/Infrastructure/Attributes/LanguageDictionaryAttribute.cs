using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Attributes
{
    public class LanguageDictionaryAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            IDictionary<string, string> dict = (IDictionary<string, string>)value;
            if(!dict.ContainsKey("en") || !dict.ContainsKey("it"))
            {
                ErrorMessage = "Data must be entered in all languages";
                return false;
            }
            return true;
        }
    }
}
