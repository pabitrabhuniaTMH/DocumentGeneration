using Domain.IRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class PersistanceService
    {
        public static IServiceCollection ConfigPersistanceServices(this IServiceCollection services)
        {
            services.AddScoped<IDBContext,DBContext>();
            return services;
        }
    }
}
