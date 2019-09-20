using DevExpress.Mvvm;

namespace MiniBar.EntityViewModels.Global
{
    public class BindableTuple<TItem1, TItem2> : ViewModelBase
    {
        public BindableTuple(TItem1 item1, TItem2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }


        private TItem1 _Item1;
        public TItem1 Item1
        {
            get { return _Item1; }
            set
            {
                SetProperty(ref _Item1, value, nameof(Item1));
            }
        }


        private TItem2 _Item2;
        public TItem2 Item2
        {
            get { return _Item2; }
            set
            {
                SetProperty(ref _Item2, value, nameof(Item2));
            }
        }
    }
}
