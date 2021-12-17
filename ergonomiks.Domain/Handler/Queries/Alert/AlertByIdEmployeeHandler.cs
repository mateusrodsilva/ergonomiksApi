using ergonomiks.Common.Commands;
using ergonomiks.Common.Handlers;
using ergonomiks.Common.Queries;
using ergonomiks.Domain.Interfaces;
using ergonomiks.Domain.Queries.Alert;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ergonomiks.Domain.Handler.Queries.Alert
{
    public class AlertByIdEmployeeHandler : Notifiable<Notification>, IHandlerQuery<AlertByIdEmployeeQuery>
    {
        private readonly IAlertRepository _alertRepository;

        public AlertByIdEmployeeHandler(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        public IQueryResult Handle(AlertByIdEmployeeQuery command)
        {
            var alertsList = _alertRepository.GetByIdEmployee(command.Id);

            if (alertsList.Count != 0)
            {
                return new GenericQueryResult(true, "Alerts List", alertsList);
            }

            return new GenericQueryResult(true, "No alert found", "");
        }
    }
}
