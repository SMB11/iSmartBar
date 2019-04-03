using DevExpress.Mvvm;
using System;
using System.ComponentModel;

namespace MiniBar.EntityViewModels.Base
{
    [Serializable]
    public class EntityViewModelBase<T> : BindableBase, IDataErrorInfo
        where T: class, INotifyPropertyChanged
    {
        public EntityViewModelBase()
        {
        }

        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }
        string IDataErrorInfo.this[string columnName]
        {
            get { return IDataErrorInfoHelper.GetErrorText(this, columnName); }
        }
    }
}
