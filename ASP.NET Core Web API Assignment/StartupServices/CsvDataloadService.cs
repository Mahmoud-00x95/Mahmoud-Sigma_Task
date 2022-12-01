using Common.DataContexts;

namespace Sigma_Software_Task.StartupServices
{
    public class CsvDataloadService : IHostedService
    {

        private readonly IServiceProvider _serviceProvider;
        public CsvDataloadService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var csvContext = scope.ServiceProvider.GetRequiredService<CSVContext>();

                await csvContext.ReloadCSVFile();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
