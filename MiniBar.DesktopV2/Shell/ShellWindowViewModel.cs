using Infrastructure;
using Infrastructure.Interface;
using Infrastructure.Interface.Enums;
using Infrastructure.MVVM;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Attributes;

namespace Shell
{
    public class ShellWindowViewModel : BaseViewModel
    {
        public IConnectionMonitoringService ConnectionMonitoringService { get; set; }

        private string connectedStatusImage;

        private bool reconnecting;

        public string ConnectedStatusImage
        {
            get { return connectedStatusImage; }
            set { SetProperty(ref connectedStatusImage, value, nameof(ConnectedStatusImage)); }
        }

        private string connectedStatusToolTip;

        public string ConnectedStatusToolTip
        {
            get { return connectedStatusToolTip; }
            set { SetProperty(ref connectedStatusToolTip, value, nameof(ConnectedStatusToolTip)); }
        }

        private string ConvertConnectionStateToImage(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.Disconnected:
                    return "pack://application:,,,/Images/disconnected.png";
                case ConnectionState.Connected:
                    return "pack://application:,,,/Images/connected.png";
                case ConnectionState.Connecting:
                    return "pack://application:,,,/Images/connecting.png";
                default:
                    return "";
            }
        }
        private string ConvertConnectionStateToToolTip(ConnectionState state)
        {
            switch (state)
            {
                case ConnectionState.Disconnected:
                    return "Disconnected from Internet";
                case ConnectionState.Connecting:
                    return "Trying to reconnect...";
                case ConnectionState.Connected:
                    return "Connected to Internet";
                default:
                    return "";
            }
        }

        private async void FillNetworkState()
        {
            reconnecting = true;
            ConnectionState state = await ConnectionMonitoringService.GetStateAsync();
            SetState(state);
            reconnecting = false;
        }

        public ShellWindowViewModel(IConnectionMonitoringService connectionMonitoringService, IRegionManager regionManager)
        {
            ConnectionMonitoringService = connectionMonitoringService;
            FillNetworkState();
            connectionMonitoringService.ConnectionStateChanged += ConnectionMonitoringService_ConnectionStateChanged;
            CommandManager.RegisterCommand(Infrastructure.Constants.CommandNames.Reconnect, Reconnect);
        }

        private void Reconnect()
        {
            if (!reconnecting)
            {
                SetState(ConnectionState.Connecting);
                FillNetworkState();
            }
        }

        private void ConnectionMonitoringService_ConnectionStateChanged(object sender, Infrastructure.Interface.Events.ConnectionStateChangedEventArgs e)
        {
            SetState(e.Value);
        }

        private void SetState(ConnectionState state)
        {

            ConnectedStatusImage = ConvertConnectionStateToImage(state);
            ConnectedStatusToolTip = ConvertConnectionStateToToolTip(state);
        }
    }
}
