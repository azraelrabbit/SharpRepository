using System;
using System.Xml.Serialization;
using Dapper.LambdaExtension.Extentions;
using Dapper.LambdaExtension.LambdaSqlBuilder.Attributes;
using OfficeOpenXml.Announce;
using WorkerService1.Data;
using ZPExam.logic.Common;

namespace ZPExam.logic.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [ZPTable("o_user")]
    [RepoFilter(Enabled = true)]
    public class User: TableBase<User, MySqlDbFactory>
    {
        [ZPKey]
        [EPIgnore]
        public string Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [EPColumnName("姓名")]
        public string UserName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [EPColumnName("电子邮件")]
        public string Email { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [EPColumnName("证件号码")]
        public string IdNumber { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [EPColumnName("单位全称")]
        public string Company { get; set; }
        
        /// <summary>
        /// 联系电话
        /// </summary>
        [EPColumnName("联系电话")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 参加培训类别
        /// </summary>
        [EPColumnName("参加培训名称")]
        public string AttendTraining { get; set; }

        /// <summary>
        /// 培训分类{目前两种: 简化培训,初始培训}
        /// </summary>
        [EPColumnName("培训分类")]
        public string TrainingCategory { get; set; }


        /// <summary>
        /// 用户状态
        /// </summary>
        [EPIgnore]
        [XmlIgnore]
       
        public string State { get; set; }

        /// <summary>
        /// 个人难度系数
        /// </summary>
       [ZPIgnore]
        [EPColumnName]
    
        public int DifficultyDegree { get; set; }

        /// <summary>
        ///  加密后密码
        /// </summary>
        [EPIgnore]
        [XmlIgnore]
   
        public DateTime CreatedTime { get; set; }

        /// <summary>
        ///  加密后密码
        /// </summary>
        [EPIgnore]
        [XmlIgnore]
 
        public string PasswdHashed { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        [EPIgnore]
        [XmlIgnore]
 
        public string Salt { get; set; }

        /// <summary>
        /// 登录标识
        /// </summary>
        [EPIgnore]
        [XmlIgnore]
 
   [ZPIgnore]
        public string Token { get; set; }

        /// <summary>
        /// 复训时间
        /// </summary>
        [EPIgnore]
        [XmlIgnore]
 
        public DateTime ReTrainingDate { get; set; }
        /// <summary>
        /// 复训时间
        /// </summary>
        [EPIgnore]
        [XmlIgnore]
   
        public DateTime ReTrainingNotifyDate { get; set; }

        /// <summary>
        /// 考勤分
        /// </summary>
        [EPColumnName("考勤分")]
        [XmlIgnore]
 
        public int CheckOnScore { get; set; }


        /// <summary>
        /// 实践分
        /// </summary>
        [EPColumnName("实践分")]
        [XmlIgnore]
 
        public int PracticeScore { get; set; }


        /// <summary>
        /// 地区编号
        /// </summary>
        [EPIgnore]
        public string AreaId { get; set; }


        /// <summary>
        /// 分类编号
        /// </summary>
        [EPIgnore]
        public string CategoryId { get; set; }


        //[EPColumnName("分类")]
        [EPIgnore]
        public string CategoryName { get; set;}


        //[EPColumnName("地区")]
        [EPIgnore]
        public string AreaName { get; set; }

         

    }
}
