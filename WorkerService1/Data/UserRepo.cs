using System;
using System.Collections.Generic;
using System.Text;
using Dapper.LambdaExtension.Extentions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using SharpRepository.InMemoryRepository;
using ZPExam.logic.Common;
using ZPExam.logic.Model;

namespace WorkerService1.Data
{
   
    public class UserRepo:InMemoryRepository<User,string>
    {
        //private MySqlDbFactory factory;

        public UserRepo()
        {
            //if (!context.Repository.Initialized)
            //{
            //    using (var db=factory.OpenDbConnection())
            //    {
            //        var results=db.Query<T>();

            //        context.Repository.Add(results);

            //        context.Repository.Initialized = true;
            //    }
            //}
            //if (factory == null)
            //{
            //    factory=new MySqlDbFactory();
            //}
            //if (!this.Initialized)
            //{
            //    using (var db = factory.OpenDbConnection())
            //    {
            //        var results = db.Query<User>();

            //        this.Add(results);
                 
            //       this.Initialized = true;
            //    }
            //}
        }
    }
}
