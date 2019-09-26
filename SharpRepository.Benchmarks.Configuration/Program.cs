using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using SharpRepository.Benchmarks.Configuration.Models;
using SharpRepository.Benchmarks.Configuration.Repositories;
using SharpRepository.InMemoryRepository;
using SharpRepository.Repository;
using SharpRepository.Repository.Configuration;
using StructureMap;
using SharpRepository.Ioc.StructureMap;
using Microsoft.Extensions.Configuration;

namespace SharpRepository.Benchmarks.Configuration
{
    class BenchmarkItem
    {
        public Action<int> Test { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
    }

    class Program
    {
        private const int Max = 250000;
        static void Main(string[] args)
        {
            var benchmarks = new Benchmarks();
            var tests = new List<BenchmarkItem>()
                            {
                                new BenchmarkItem()
                                    {
                                        Title = "Direct Creation: [new InMemoryRepository()]",
                                        Test = Benchmarks.DirectCreation,
                                        Order = 1
                                    },
                                new BenchmarkItem()
                                {
                                    Title = "Test Adding Item[UserRepository : InMemoryRepository<User,int>]",
                                    Test = Benchmarks.AddNewItem,
                                    Order = 2
                                },
                                new BenchmarkItem()
                                {
                                    Title = "Test Adding Item[UserRepository : InMemoryRepository<User,int>]",
                                    Test = Benchmarks.FindItem,
                                    Order = 7
                                },
                                new BenchmarkItem()
                                    {
                                        Title = "Custom repo hardcoded: [UserRepository : InMemoryRepository<User,int>]",
                                        Test = Benchmarks.CustomRepositoryHardCoded,
                                        Order = 3
                                    },
                                //new BenchmarkItem()
                                //    {
                                //        Title = "From config file: [RepositoryFactory.GetInstance<User, int>()]",
                                //        Test = Benchmarks.CreateFromConfigFile,
                                //        Order = 3
                                //    },
                                new BenchmarkItem()
                                    {
                                        Title = "From config obj: [RepositoryFactory.GetInstance<User, int>(config)]",
                                        Test = Benchmarks.CreateFromConfigObject,
                                        Order = 4
                                    },
                                new BenchmarkItem()
                                    {
                                        Title = "Custom repo config: [UserRepository : ConfigurationBasedRepository<User, int>]",
                                        Test = Benchmarks.CustomRepositoryFromConfig,
                                        Order = 5
                                    },
                                new BenchmarkItem()
                                    {
                                        Title = "StructureMap w/ config: [container.GetInstance<IRepository<User, int>>()]",
                                        Test = benchmarks.DirectFromStructureMap,
                                        Order = 6
                                    }
                            };


            Benchmarks.resp=new InMemoryRepository<User, int>();

            // run thru each of them once because otherwise the first loop is slower for some reason
            foreach (var item in tests)
            {
                 
                    item.Test(0);
               
             
            }

            

            Console.WriteLine("Running each test {0:#,0} times", Max);
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();

            var sw = new Stopwatch();

            foreach (var benchmarkItem in tests.AsQueryable().OrderBy(x => x.Order))
            {
                Console.WriteLine(benchmarkItem.Title);
                sw.Reset();
                sw.Start();

                for (var i = 0; i < Max; i++)
                {

                    benchmarkItem.Test(i);
                }
                sw.Stop();
                Console.WriteLine("   {0} ms total -- {1} avg ms per\n", sw.Elapsed.TotalMilliseconds, sw.Elapsed.TotalMilliseconds / Convert.ToDouble(Max));
            }
            
            Console.WriteLine("\nDone: press enter to quit");
            Console.Read();
        }
    }

    public class Benchmarks
    {
        public IContainer StructureMapContainer { get; set; }
        
        public Benchmarks()
        {
            StructureMapContainer = new Container(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });

                var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("repository.json")
               .Build();

                var sectionName = "sharpRepository";

                IConfigurationSection sharpRepoSection = config.GetSection(sectionName);

                if (sharpRepoSection == null)
                    throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");

                var sharpRepoConfig = RepositoryFactory.BuildSharpRepositoryConfiguation(sharpRepoSection);
               
                x.ForRepositoriesUseSharpRepository(sharpRepoConfig);
            });
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void DirectCreation(int order)
        {
            new InMemoryRepository<User, int>();
        }

        public static InMemoryRepository<User,int> resp =null;

        public static void AddNewItem(int order)
        {
            
            resp.Add(new User(){Email = "aa@a.com",FullName = "aabbcc",UserId = order});
        }

        public static void FindItem(int order)
        {
            var st = Stopwatch.StartNew();
            var ret = resp.Find(p => p.UserId == order);

            st.Stop();

            var tt = st.ElapsedMilliseconds;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void CreateFromConfigFile(int order)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("repository.json")
               .Build();

            var sectionName = "sharpRepository";

            IConfigurationSection sharpRepoSection = config.GetSection(sectionName);

            if (sharpRepoSection == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");

            var factory = new RepositoryFactory(sharpRepoSection);

            factory.GetInstance<User, int>();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void CustomRepositoryFromConfig(int order)
        {
            
            var config = new SharpRepositoryConfiguration();
            var repoConf = new RepositoryConfiguration("inMemory")
            {
                Factory = typeof(InMemoryConfigRepositoryFactory)
            };
            config.AddRepository(repoConf);

            new UserFromConfigRepository(config);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void CustomRepositoryHardCoded(int order)
        {
            new UserRepository();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void DirectFromStructureMap(int order)
        {
            StructureMapContainer.GetInstance<IRepository<User, int>>();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void CreateFromConfigObject(int order)
        {
            var config = new SharpRepositoryConfiguration();
            config.AddRepository(new InMemoryRepositoryConfiguration("default"));
            RepositoryFactory.GetInstance<User, int>(config);
        }
    }
}
