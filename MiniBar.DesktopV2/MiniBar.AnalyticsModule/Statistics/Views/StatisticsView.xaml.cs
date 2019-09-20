using AutoMapper;
using Infrastructure.Framework;
using Infrastructure.Logging;
using Infrastructure.Security;
using Microsoft.AspNetCore.SignalR.Client;
using MiniBar.AnalyticsModule.Services;
using SharedEntities.DTO.Statistics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MiniBar.AnalyticsModule.Statistics.Views
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : UserControl
    {

        StatisticsService StatisticsService;
        IUIManager UIManager;
        ICompositeLogger Logger;
        IMapper Mapper;

        private List<VisitDTO> visits;
        public List<VisitDTO> Visits
        {
            get { return visits; }
            set { visits = value; }
        }

        private async void Start()
        {
            try
            {
                var visits = await StatisticsService.GetVisits();

                Visits = visits;
                dash.ReloadData();
            }
            catch (Exception e)
            {
                Logger.LogErrorSource("Failed to load data", e);
            }

            try
            {
                var hubConnection = new HubConnectionBuilder()
                    .WithUrl(Path.Combine(ConfigurationManager.AppSettings["HubBaseUrl"], "VisitHub"), options =>
                    {
                        options.AccessTokenProvider = TokenProvider;
                    })
                    .Build();
                hubConnection.On<VisitDTO>("WebsiteVisited", visit => OnVisited(visit));
                await hubConnection.StartAsync();
            }
            catch (Exception e)
            {
                UIManager.Error("Failed to set up live updates");
                Logger.LogErrorSource("Failed to set up live updates", e);
                // TODO: Log
            }
        }

        private Task<string> TokenProvider()
        {
            return Task.FromResult(AppSecurityContext.CurrentPrincipal.Identity.Token);
        }

        private void OnVisited(VisitDTO visit)
        {
            Visits.Add(visit);
            Dispatcher.Invoke(() => dash.ReloadData());

        }

        public StatisticsView()
        {
            InitializeComponent();
            Logger = CommonServiceLocator.ServiceLocator.Current.GetInstance<ICompositeLogger>();
            UIManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUIManager>();
            StatisticsService = CommonServiceLocator.ServiceLocator.Current.GetInstance<StatisticsService>();
            Mapper = CommonServiceLocator.ServiceLocator.Current.GetInstance<IMapper>();
            Start();
        }

        private void Dash_AsyncDataLoading(object sender, DevExpress.DashboardCommon.DataLoadingEventArgs e)
        {
            e.Data = Visits;
        }
    }
}
