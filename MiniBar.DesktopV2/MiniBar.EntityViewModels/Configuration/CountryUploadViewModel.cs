using Infrastructure.Interface;
using Infrastructure.Localization;
using MiniBar.EntityViewModels.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MiniBar.EntityViewModels.Configuration
{
    public class CountryUploadViewModel : EntityViewModelBase<CountryUploadViewModel>, IIdEntityViewModel
    {
        private int id;

        public int ID
        {
            get { return id; }
            set { SetProperty(ref id, value, nameof(ID)); }
        }


        [Required]
        public string MainName
        {
            get { return (Names != null && Names.ContainsKey(CultureInfo.CurrentCulture.Name)) ? Names[CultureInfo.CurrentCulture.Name] : ""; }
        }

        private IDictionary<string, string> _Names;
        [LanguageDictionary]
        public IDictionary<string, string> Names
        {
            get { return _Names; }
            set
            {
                SetProperty(ref _Names, value, nameof(Names));

                RaisePropertiesChanged(nameof(MainName));
            }
        }
    }
}
