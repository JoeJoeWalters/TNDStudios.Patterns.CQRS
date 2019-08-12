using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(TNDStudios.Patterns.CQRS.Service.Startup))]

namespace TNDStudios.Patterns.CQRS.Service
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ISearchBroker, LocalSearchBroker>((s) => 
            {
                return new LocalSearchBroker(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            });
        }
    }
}
