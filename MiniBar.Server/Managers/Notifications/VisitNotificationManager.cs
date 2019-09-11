using AutoMapper;
using BusinessEntities.Statistics;
using Common.Constants;
using Common.Services;
using Facade.Repository;
using Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Statistics;
using System;

namespace Managers.Notifications
{
    public class VisitNotificationManager : INotificationHandler
    {
        IServiceProvider ServiceProvider;

        public VisitNotificationManager(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public void Handle(string name, object data)
        {
            switch (name)
            {
                case NotificationNames.WebsiteVisited:
                    OnWebsiteVisited(data);
                    break;
            }
        }

        private async void OnWebsiteVisited(object data)
        {
            var visit = (Visit)data;
            var fullVisit = await ServiceProvider.GetService<IVisitRepository>().LoadWith(v => v.Hotel).FindByIDAsync(visit.ID);
            await ServiceProvider.GetService<IHubContext<VisitHub>>().Clients.All.SendAsync("WebsiteVisited", Mapper.Map<VisitDTO>(fullVisit));

        }
    }
}
