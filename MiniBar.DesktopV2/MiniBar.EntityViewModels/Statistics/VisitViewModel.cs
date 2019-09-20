using Infrastructure.Framework;
using MiniBar.EntityViewModels.Configuration;
using System;

namespace MiniBar.EntityViewModels.Statistics
{
    public class VisitViewModel : EditableViewModel<VisitViewModel>
    {

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                SetProperty(ref _ID, value, nameof(ID));
            }
        }

        private HotelViewModel _Hotel;
        public HotelViewModel Hotel
        {
            get { return _Hotel; }
            set
            {
                SetProperty(ref _Hotel, value, nameof(Hotel));
            }
        }

        private string _LangaugeID;
        public string LangaugeID
        {
            get { return _LangaugeID; }
            set
            {
                SetProperty(ref _LangaugeID, value, nameof(LangaugeID));
            }
        }

        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                SetProperty(ref _StartDate, value, nameof(StartDate));
            }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                SetProperty(ref _EndDate, value, nameof(EndDate));
            }
        }
    }
}
