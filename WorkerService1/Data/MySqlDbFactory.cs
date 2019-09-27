using System.Data;
using System.Data.Common;
using Dapper.LambdaExtension.Extentions;
using MySql.Data.MySqlClient;

namespace ZPExam.logic.Common
{
    public class MySqlDbFactory:DbFactoryBase<MySqlConnection>
    {
 
        public MySqlDbFactory()
        {
            ConnectionString = "server=192.168.10.25;port=3306;database=zpexam_201903;uid=root;pwd=ZeroPlus2013;";
  
        }
 
    }
}
