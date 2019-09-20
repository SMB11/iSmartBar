using MiniBar.EntityViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace MiniBar.EntityViewModels.Global
{
    public class LanguageDataViewModel : EntityViewModelBase<LanguageDataViewModel>
    {

        private LanguageViewModel language;
        public LanguageViewModel Language
        {
            get { return language; }
            set
            {
                SetProperty(ref language, value, nameof(Language));
            }
        }

        private string _value;
        [Required]
        [MinLength(3)]
        public string Value
        {
            get { return _value; }
            set
            {
                SetProperty(ref _value, value, nameof(Value));
            }
        }
    }
}
