using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Localization
{
    /// <summary>
    /// Valiation attribute for a dictionary containing language data.
    /// Assures that data is entered in all supported languages.
    /// TODO: remove hardcoded languages
    /// </summary>
    public class LanguageDictionaryAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            IDictionary<string, string> dict = (IDictionary<string, string>)value;
            if (!dict.ContainsKey("en") || !dict.ContainsKey("it"))
            {
                ErrorMessage = "Data must be entered in all languages";
                return false;
            }
            return true;
        }
    }
}
