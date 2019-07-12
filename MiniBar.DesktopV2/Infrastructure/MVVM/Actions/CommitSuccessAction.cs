using Infrastructure.Extensions;
using Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.MVVM.Actions
{
    public class CommitSuccessAction
    {
        public static CommitSuccessAction NoAction
        {
            get
            {
                return new CommitSuccessAction();
            }
        }

        private Action<ISupportsListManipulation> Action { get; set; }

        private CommitSuccessAction() { }

        public CommitSuccessAction(Action<ISupportsListManipulation> action)
        {
            Action = action;
        }

        public void Invoke(ISupportsListManipulation obj)
        {
            Application.Current.Dispatcher.InvokeIfNeeded(Action, obj);
        }
    }
}
