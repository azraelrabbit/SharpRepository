using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharpRepository.InMemoryRepository;
using SharpRepository.Repository;
using WorkerService1.Data;
using ZPExam.logic.Model;

namespace WorkerService1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    
                    services.AddSingleton<IRepository<User,string>>(r => new UserRepo());

                    services.AddHostedService<Worker>();

                    
                });
    }
}
