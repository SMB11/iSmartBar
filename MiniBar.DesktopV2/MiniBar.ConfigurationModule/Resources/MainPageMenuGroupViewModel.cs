using Infrastructure.Interface;
using System.Linq;
using MiniBar.ConfigurationModule.Workitems.CountryManager;
using MiniBar.ConfigurationModule.Workitems.CityManager;
using MiniBar.ConfigurationModule.Workitems.HotelManager;
using Infrastructure.Framework;

namespace MiniBar.ConfigurationModule.Resources
{
    public class MainPageMenuGroupViewModel : BaseViewModel
    {
        public MainPageMenuGroupViewModel(IContextService currentContextService)
        {
            currentContextService.WorkitemCollectionChanged += CurrentContextService_WorkitemCollectionChanged;
        }

        private void CurrentContextService_WorkitemCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null)
                    {
                        CountryManagerCount += e.NewItems.OfType<CountryManagerWorkitem>().Count();
                        CityManagerCount += e.NewItems.OfType<CityManagerWorkitem>().Count();
                        HotelManagerCount += e.NewItems.OfType<HotelManagerWorkitem>().Count();
                    }
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    if (e.OldItems!= null)
                    {
                        CountryManagerCount -= e.OldItems.OfType<CountryManagerWorkitem>().Count();
                        CityManagerCount -= e.OldItems.OfType<CityManagerWorkitem>().Count();
                        HotelManagerCount -= e.OldItems.OfType<HotelManagerWorkitem>().Count();
                    }
                    break;
            }
        }

        private int countryManagerCount;

        public int CountryManagerCount
        {
            get { return countryManagerCount; }
            set {
                SetProperty(ref countryManagerCount,  value, nameof(CountryManagerCount));
                RaisePropertyChanged(nameof(CountryManagerInformativeText));
            }
        }

        public string CountryManagerInformativeText
        {
            get
            {
                if (CountryManagerCount > 0)
                    return CountryManagerCount + " open";
                return null;
            }
        }


        private int cityManagerCount;

        public int CityManagerCount
        {
            get { return cityManagerCount; }
            set
            {
                SetProperty(ref cityManagerCount, value, nameof(CityManagerCount));
                RaisePropertyChanged(nameof(CityManagerInformativeText));
            }
        }

        public string CityManagerInformativeText
        {
            get
            {
                if (CityManagerCount > 0)
                    return CityManagerCount + " open";
                return null;
            }
        }

        private int hotelManagerCount;

        public int HotelManagerCount
        {
            get { return hotelManagerCount; }
            set
            {
                SetProperty(ref hotelManagerCount, value, nameof(HotelManagerCount));
                RaisePropertyChanged(nameof(HotelManagerInformativeText));
            }
        }

        public string HotelManagerInformativeText
        {
            get
            {
                if (HotelManagerCount > 0)
                    return HotelManagerCount + " open";
                return null;
            }
        }

    }
}