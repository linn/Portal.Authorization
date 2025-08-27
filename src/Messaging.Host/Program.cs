using Linn.Portal.Authorization.IoC;
using Linn.Portal.Authorization.IoC.Logging.AmazonSQS;
using Linn.Portal.Authorization.Messaging.Host.Jobs;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
        {
            services.AddLog();
            services.AddCredentialsExtensions();
            services.AddServices();
            services.AddPersistence();
            services.AddSQSExtensions();
            services.AddRabbitConfiguration();
            services.AddMessageHandlers();
            services.AddHostedService<Listener>();
        })
    .Build();

await host.RunAsync();
