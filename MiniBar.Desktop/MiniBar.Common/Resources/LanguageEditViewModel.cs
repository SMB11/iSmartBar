﻿using DevExpress.Mvvm;
using Infrastructure.Extensions;
using MiniBar.EntityViewModels.Global;
using MiniBar.EntityViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBar.Common.Resources
{
    public class LanguageEditViewModel : ViewModelBase
    {

        public bool Validate()
        {
            bool isValid = !LanguageDatas.All(d => ((IDataErrorInfo)d).HasErrors());

            return isValid;
        }

        private List<LanguageDataViewModel> languageDatas;
        public List<LanguageDataViewModel> LanguageDatas
        {
            get
            {
                return languageDatas;
            }
            set { SetProperty(ref languageDatas, value, nameof(LanguageDatas)); }
        }


        public LanguageEditViewModel()
        {
            LanguageDatas = new List<LanguageDataViewModel>();
            LanguageDatas.Add(
                new LanguageDataViewModel { Language = new LanguageViewModel { ID = "en", Name = "English" } }
                );
            LanguageDatas.Add(
                new LanguageDataViewModel { Language = new LanguageViewModel { ID = "it", Name = "Italian" } }
            );
        }

        public void SetData(Dictionary<string, string> names)
        {
            foreach(var data in languageDatas)
            {
                if (names.ContainsKey(data.Language.ID))
                {
                    data.Value = names[data.Language.ID];
                }
            }
        }

        public Dictionary<string, string> GetData()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach (var data in languageDatas)
            {
                res.Add(data.Language.ID, data.Value);
            }
            return res;
        }
    }
}