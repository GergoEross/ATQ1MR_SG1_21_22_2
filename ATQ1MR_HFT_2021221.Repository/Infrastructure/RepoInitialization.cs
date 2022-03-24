using ATQ1MR_HFT_2021221.Data;
using ATQ1MR_HFT_2021221.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATQ1MR_HFT_2021221.Repository.Infrastructure
{
    public static class RepoInitialization
    {
        public static void InitRepoServices(IServiceCollection services)
        {
            services.AddScoped<PcPartsDbContext>((sp) => new PcPartsDbContext());
            services.AddScoped<IMBrandRepository, MBrandRepository>();
            services.AddScoped<IPBrandRepository, PBrandRepository>();
            services.AddScoped<IMotherboardRepository, MotherboardRepository>();
            services.AddScoped<IProcessorRepository, ProcessorRepository>();
        }
    }
}
