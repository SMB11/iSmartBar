using Infrastructure.Interface;
using Infrastructure.Utility;
using System;
using System.Windows;

namespace MiniBar.Common.MVVM
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
