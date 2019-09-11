using AutoMapper;
using BusinessEntities.Statistics;
using Common.Constants;
using Common.Core;
using Facade.Managers;
using Facade.Repository;
using Hubs;
using Managers.Base;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Statistics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Managers.Implementation
{
    public class VisitManager : ManagerBase, IVisitManager
    {
        public VisitManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<VisitDTO>> GetAllAsync()
        {
            IVisitRepository visitRepository = ServiceProvider.GetService<IVisitRepository>();
            List<Visit> cities = await visitRepository.LoadWith(v => v.Hotel).GetAllAsync();
            return Mapper.Map<List<VisitDTO>>(cities);
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<int> InsertAsync(VisitDTO visit)
        {
            IVisitRepository visitRepository = ServiceProvider.GetService<IVisitRepository>();
            Visit inserted = await visitRepository.InsertAsync(new Visit { LanguageID = visit.LanguageID, HotelID = visit.HotelID, EndDate = visit.EndDate, StartDate = visit.StartDate});
            Notification.Trigger(NotificationNames.WebsiteVisited, inserted);
            return inserted.ID;
        }
    }
}
