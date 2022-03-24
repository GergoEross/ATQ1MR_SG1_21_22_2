using ATQ1MR_HFT_2021221.Logic.Intefaces;
using ATQ1MR_HFT_2021221.Logic.Services;
using ATQ1MR_HFT_2021221.Repository.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Logic.Infrastructure
{
    public static class BLInitialization
    {
        public static void InitBlServices(IServiceCollection services)
        {
            RepoInitialization.InitRepoServices(services);

            services.AddScoped<IMBrandLogic, MBrandLogic>();
            services.AddScoped<IMotherboardLogic, MotherboardLogic>();
            services.AddScoped<IProcessorLogic, ProcessorLogic>();
            services.AddScoped<IPBrandLogic, PBrandLogic>();
        }
    }
}
