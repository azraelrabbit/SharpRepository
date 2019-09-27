using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper.LambdaExtension.Extentions;
using SharpRepository.Repository.Aspects;
using ZPExam.logic.Common;
using ZPExam.logic.Model;

namespace WorkerService1.Data
{
    public class RepoFilterAttribute : RepositoryActionBaseAttribute
    {
        private MySqlDbFactory factory;

        private bool Initialized;
        
 
        public override void OnInitialized<T, TKey>(RepositoryActionContext<T, TKey> context)
        {
            var count = context.Repository.Count();
            Console.WriteLine($"{typeof(T).Name} count is {count}");
            factory = new MySqlDbFactory();

            if (!Initialized)
            {
                using (var db = factory.OpenDbConnection())
                {
                    var results = db.Query<T>();

                    context.Repository.Add(results);
                 
                    this.Initialized = true;

                    context.Repository.Initialized = true;
                }

            }
            
            base.OnInitialized(context);
        }

        public override bool OnAddExecuting<T, TKey>(T entity, RepositoryActionContext<T, TKey> context)
        {
            var ci = context.Repository.Initialized;

            if (!Initialized)
            {
                return true;
            }
            using (var db = factory.OpenDbConnection())
            {
                try
                {
                    db.Insert(entity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return false;
                }

            }
            return base.OnAddExecuting(entity, context);
        }

        public override bool OnUpdateExecuting<T, TKey>(T entity, RepositoryActionContext<T, TKey> context)
        {
            using (var db = factory.OpenDbConnection())
            {
                try
                {
                    db.Update(entity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return false;
                }

            }
            return base.OnUpdateExecuting(entity, context);
        }

        public override bool OnDeleteExecuting<T, TKey>(T entity, RepositoryActionContext<T, TKey> context)
        {
            using (var db = factory.OpenDbConnection())
            {
                try
                {
                    db.Delete(entity);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    return false;
                }

            }
            return base.OnDeleteExecuting(entity, context);
        }

        
    }
}
