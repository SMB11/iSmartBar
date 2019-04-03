using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.EntityViewModels.Global
{
    public class LanguageViewModel : BindableBase
    {
        private string _ID;
        public string ID
        {
            get { return _ID; }
            set
            {
                SetProperty(ref _ID, value, nameof(ID));
            }
        }


        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value, nameof(Name));
            }
        }
    }
}
