using DevOps.Domain;
using AppLogic.Events;
using Microsoft.Extensions.Logging;

namespace DevOps.AppLogic.Events
{
    internal class EmployeeHiredEventHandler : IIntegrationEventHandler<EmployeeHiredIntegrationEvent>
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly ILogger<EmployeeHiredEventHandler> _logger;

        public EmployeeHiredEventHandler(IDeveloperRepository developerRepository, ILogger<EmployeeHiredEventHandler> logger)
        {
            _developerRepository = developerRepository;
            _logger = logger;
        }

        public Task Handle(EmployeeHiredIntegrationEvent @event)
        {
            _logger.LogDebug($"DevOps - Handling employee hire. Id: {@event.Id}");
            return Task.Run(async () =>
            {
                Developer developer = await _developerRepository.GetByIdAsync(@event.Number);
                if (developer != null)
                {
                    _logger.LogDebug($"DevOps - No developer added. A developer with id '{@event.Number}' already exists. Id: {@event.Id}");
                    return;
                }

                developer = Developer.CreateNew(@event.Number, @event.FirstName, @event.LastName, 0.0);
                await _developerRepository.AddAsync(developer);
                _logger.LogDebug($"DevOps - Developer with id '{@event.Number}' added. Id: {@event.Id}");
            });
        }
    }
}