using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Criterion;



using rovnicaMVC.Domain;

using log4net;

using System.Net.Mail;



namespace rovnicaMVC.Models
{
    //public class SqlStatementInterceptor : EmptyInterceptor
    //{
    //    //public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
    //    //{
    //    //    Console.WriteLine(sql.ToString());
            
    //    //    return sql;
    //    //}

        
    //}
  


    public class DataStorage 
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(CategoryItemRepository));


        private static readonly int pageSize = 6;
        private static ISessionFactory factory;

        private static ISession OpenSession()
        {
            if (factory == null)
            {

               

        Configuration c = Fluently.Configure().
                Database(
                    MsSqlConfiguration.MsSql2008
                        .ConnectionString(
                        System.Configuration.ConfigurationManager.ConnectionStrings["cn_mssql"].ConnectionString
                            )
                            )

                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<rovnicaMVC.Domain.hq_need>())
                //Подключаем интерсептор (отключить позже)
                //.ExposeConfiguration(x =>
                //{
                //    x.SetInterceptor(new SqlStatementInterceptor());
                //})
                
                .BuildConfiguration();

                

 //new SchemaExport(c).Create(true, true);

                factory = c.BuildSessionFactory();
            }
            return factory.OpenSession();
        }
        

       public static IList<hq_need> getNeeds()
       {
           //List<hq_need> res = new List<hq_need>();

           using (ISession session = OpenSession())
            {
               var a = session.QueryOver<hq_need>().List();
               return a;
           }


           
                ////TODO: сделать правильно
                //if (categoryId != 0)
                //{
                //    res.pager.pageTotalCount = (session.QueryOver<cItem>()
                //        .Where(p => p.Category.Id == categoryId)
                //        .And(p => p.Price >= priceFrom)
                //        .And(p => p.Price <= priceTo)
                //        .RowCount() / pageSize) + 1;

                //    var items = session.QueryOver<cItem>()
                //        .Where(p => p.Category.Id == categoryId)
                //        .And(p => p.Price >= priceFrom)
                //        .And(p => p.Price <= priceTo)
                //        .Skip(pageSize * (pageNumber - 1))
                //        .Take(pageSize)
                //        .List();

       }
        

        public static void SendEmail(cOrder o)
        {
            //TODO: оформить тело письма

            return;

            MailMessage message = new MailMessage(new MailAddress("tyaps@mail.ru"), new MailAddress("tyaps@mail.ru"));
            message.Body = "Тело письма";
            message.Subject = "Новый заказ на rovnicaMVC.ru";

            System.Net.Mail.SmtpClient smtp = new SmtpClient(appSettings("smtpServer"));
            smtp.Credentials = new System.Net.NetworkCredential(appSettings("smtpUserName"), appSettings("smtpPassword"));
            smtp.Send(message);
            

        }

        //вынести в отдельный класс
        public static string appSettings(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }


        public static bool ValidateUser(string userName, string passwordEncrypted)
        {
            using (ISession session = OpenSession())
            {
                var match = session.QueryOver<cUser>().Where(u => u.userName == userName).And(u => u.Password == passwordEncrypted).RowCount();
                return match == 1;
            }
        }

    
               
    }

}