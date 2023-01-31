using Application.DocumentGenerationServices;
using Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationService
    {
        public static IServiceCollection ConfigApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IGenerate,Generate>();
            services.AddScoped<ITemplate,Template>();
            return services;
        }
    }
}
