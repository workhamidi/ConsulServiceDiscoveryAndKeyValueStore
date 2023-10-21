namespace Consul.Service1
{
    public class ServiceRegister : IHostedService
    {
        private readonly IConsulClient _consulClient;

        public ServiceRegister(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            var registration = new AgentServiceRegistration
            {
                ID = "service1-1",
                Name = "service1",
                Address = "192.169.14.2",// current machine ip
                Port = 2000// current machine port
            };

            var check = new AgentServiceCheck
            {
                HTTP = "/Health",
                Interval = TimeSpan.FromSeconds(200),
                Timeout = TimeSpan.FromSeconds(15)
            };

            registration.Checks = new[] { check };


            await _consulClient.Agent.ServiceDeregister(registration.ID,
                cancellationToken);

            await _consulClient.Agent.ServiceRegister(registration,
                cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {

            var registration = new AgentServiceRegistration
            {
                ID = "service1-1"
            };


            await _consulClient.Agent.ServiceDeregister(registration.ID,
                cancellationToken);

        }
    }
}
