using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharpRepository.Repository;
using ZPExam.logic.Model;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IRepository<User, string> _userRepo;

        public Worker(ILogger<Worker> logger,IRepository<User, string> userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if (_userRepo.Initialized)
                {
                    var st = Stopwatch.StartNew();

                    var alluser = _userRepo.GetAll().Count();

                    st.Stop();

                    _logger.LogInformation("user total is: {ucount}, time elscaped: {stt} ms",alluser,st.ElapsedMilliseconds);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
