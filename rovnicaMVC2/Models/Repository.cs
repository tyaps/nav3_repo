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
    public class SqlStatementInterceptor : EmptyInterceptor
    {
        //public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        //{
        //    Console.WriteLine(sql.ToString());
            
        //    return sql;
        //}

        
    }



    public class CategoryItemRepository
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
                    MySQLConfiguration
                    .Standard
                    //.ShowSql()
                        .ConnectionString(
                        
                        System.Configuration.ConfigurationManager.ConnectionStrings["cn"].ConnectionString

                        //x => x


                        //    .Server(@"localhost")
                        //    .Database("rovnicamvc2")
                        //    .Username("root")
                        //    .Password("1")
                            
                            )
                            
                            
                            )


                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<rovnicaMVC.Domain.cItem>())
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
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageNumber">номер страницы, начиная с 1</param>
        /// <returns></returns>
        public static categoryInfo LoadCategoryItems(int categoryId, int pageNumber, int priceFrom, int priceTo, int status)
        {
            
            log.Info("LoadCategoryItems");
            

            var res = new categoryInfo() { categoryId = categoryId };
            res.pager.currentPage = pageNumber;

            using (ISession session = OpenSession())
            {

                //TODO: сделать правильно
                if (categoryId != 0)
                {
                    res.pager.pageTotalCount = (session.QueryOver<cItem>()
                        .Where(p => p.Category.Id == categoryId)
                        .And(p => p.Price >= priceFrom)
                        .And(p => p.Price <= priceTo)
                        .RowCount() / pageSize) + 1;

                    var items = session.QueryOver<cItem>()
                        .Where(p => p.Category.Id == categoryId)
                        .And(p => p.Price >= priceFrom)
                        .And(p => p.Price <= priceTo)
                        .Skip(pageSize * (pageNumber - 1))
                        .Take(pageSize)
                        .List();

                    foreach (var item in items)
                    {
                        var i = item.Images.Count; //подгружаем картинки
                    }

                    res.items = items.ToList();
                }
                else
                {
                    if (status == 1)
                    {
                        res.pager.pageTotalCount = (session.QueryOver<cItem>()
                            .Where(p => p.Price >= priceFrom)
                            .And(p => p.Price <= priceTo)
                            .And(p=>p.Status==status)
                            .RowCount() / pageSize) + 1;

                        var items = session.QueryOver<cItem>()
                            .Where(p => p.Price >= priceFrom)
                            .And(p => p.Price <= priceTo)
                            .And(p => p.Status == status)
                            .Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .List();

                        foreach (var item in items)
                        {
                            var i = item.Images.Count; //подгружаем картинки
                        }

                        res.items = items.ToList();
                    }
                    else
                    {
                        res.pager.pageTotalCount = (session.QueryOver<cItem>()
                            .Where(p => p.Price >= priceFrom)
                            .And(p => p.Price <= priceTo)
                            .RowCount() / pageSize) + 1;

                        var items = session.QueryOver<cItem>()
                            .Where(p => p.Price >= priceFrom)
                            .And(p => p.Price <= priceTo)
                            .Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .List();

                        foreach (var item in items)
                        {
                            var i = item.Images.Count; //подгружаем картинки
                        }

                        res.items = items.ToList();
                    }
                }
               

   

                return res;

            }
        }
        public static cItem LoadItem(int id)
        {
            using (ISession session = OpenSession())
            {
                var item = session.Get<cItem>(id);

                //Нужно правильно выцепить эти данные
                int a = item.Images.Count;
                a = item.Colors.Count;
                a = item.Sizes.Count;
                return item;
            }

            
        }

        public static List<cItem> LoadItems(List<int> ids)
        {
            using (ISession session = OpenSession())
            {
                var res = new List<cItem>();

                foreach (int id in ids)
                {
                    res.Add(session.Get<cItem>(id));
                }
            
                return res;
            }


        }

        public static List<purchasedItem> LoadPurchasedItemsData(List<purchasedItem> purchasedItems)
        {
            using (ISession session = OpenSession())
            {
                

                foreach (var item in purchasedItems)
                {
                    item.itemData = session.Get<cItem>(item.itemId);
                    item.colorName = session.Get<cColor>(item.colorId).Name;
                    item.sizeName = session.Get<cSize>(item.sizeId).Name;
                }

                return purchasedItems;
            }


        }

        public static void CreateOrder(cOrder order)
        {
            using (ISession session = OpenSession())
            {
                //TODO: в транзакцию!
                session.Save(order);
                
                //Уменьшить на складе кол-во одежды данного размера/цвета
                foreach (var item in order.Items)
                {
                    var warehouseAmount = session.QueryOver<cWarehouseAmountTotal>()
                        .Where(c => c.ColorId == item.ColorId).And(c => c.SizeId == item.SizeId).SingleOrDefault();

                    warehouseAmount.Amount--;
                    session.Save(warehouseAmount);

                }

                session.Flush();
            }

        }
        public static void SaveOrder(cOrder updated_order)
        {
            using (ISession session = OpenSession())
            {
                var o = session.Get<cOrder>(updated_order.Id);
                o.status = updated_order.status;
               
                session.Flush();
            }

        }

        public static IList<cOrder> GetOrders(OrderState status)
        {
            using (ISession session = OpenSession())
            {
                var disjunction = new Disjunction().Add(Restrictions.Eq("status", status));
                if (status == OrderState.All)
                    disjunction.Add(new NotNullExpression("status"));

                var res4 = session.QueryOver<cOrder>()
                   .Where(disjunction)

                    .JoinQueryOver<cOrderItem>(c => c.Items)
                    .JoinQueryOver<cItem>(c => c.Item)

                    .Fetch(c => c.Items).Eager.List();
                    //.Fetch(c => c.Items).th;


                string s =""; //Нелепая подгрузка (вместо Eager)
                foreach(var o in res4)
                    foreach(var o1 in o.Items)
                        s = o1.Item.Category.Name;

                //Подгрузить все используемые цвета и размеры
                //Назначить их текстовые имена
                //var colorIds = res4.Select(c => c.Items.Select(i => i.ColorId).ToArray()).Distinct().ToArray();
                //var sizeIds = res4.Select(c => c.Items.Select(i => i.SizeId).ToArray()).Distinct().ToArray();
                List<int> colorIds = new List<int>();
                List<int> sizeIds = new List<int>();
                foreach (var res in res4)
                {
                    colorIds.AddRange(res.Items.Select(c => c.ColorId));
                    sizeIds.AddRange(res.Items.Select(c => c.SizeId));
                }

                colorIds = colorIds.Distinct().Where(c => c != 0).ToList();
                sizeIds = sizeIds.Distinct().Where(c => c != 0).ToList();

                var colors = session.QueryOver<cColor>().Where(Restrictions.In("id",colorIds)).List();
                var sizes = session.QueryOver<cSize>().Where(Restrictions.In("id", sizeIds)).List();

                foreach (var res in res4)
                {
                    foreach (var i in res.Items)
                    {
                        if(i.ColorId!=0)
                            i.ColorName = colors.Where(c => c.Id == i.ColorId).Single().Name;

                        if (i.SizeId != 0)
                            i.SizeName = sizes.Where(c => c.Id == i.SizeId).Single().Name;
                    }
                    
                }

                return res4;
            }

        }

        public static IList<cCategory> getCategories()
        {
            

            using (ISession session = OpenSession())
            {
                var res = session.QueryOver<cCategory>().List();
                return res;
            }
        }


        public static IList<cItem> getCategoryItems(int categoryId)
        {
            using (ISession session = OpenSession())
            {
               

               var res = session.QueryOver<cItem>().Where(c=>c.Category.Id == categoryId).List();
                
                return res;
            }
        }
        public static cItem CreateItem(cItem item)
        {
            using (ISession session = OpenSession())
            {

                session.Save(item);
                return item;
            }
        }

        public static void saveItem(saveItemDetailsData updateData)
        {
            using (ISession session = OpenSession())
            {
                var item = session.Load<cItem>(updateData.itemId);

                item.Category.Id = updateData.categoryId;
                item.Status = updateData.status;
                item.Name = updateData.Name;
                item.Price = updateData.Price;
                item.PriceOpt = updateData.PriceOpt;

                item.DescriptionCommon = updateData.DescriptionCommon;
                item.DescriptionMaterial = updateData.DescriptionMaterial;

                if (updateData.removedColors != null)
                    foreach (var id in updateData.removedColors)
                    {

                        //удалить кол-во на складе
                        var warehouseAmounts = session.QueryOver<cWarehouseAmountTotal>().Where(c => c.ItemId == item.Id).And(c => c.ColorId == int.Parse(id)).List();
                        foreach (var wa in warehouseAmounts)
                            session.Delete(wa);
                    

                        var color = session.Load<cColor>(int.Parse(id));
                        session.Delete(color);
                        //item.Colors.Remove(color);
                       
                        
                       

                    }

                if (updateData.addedColors != null)
                    foreach (var colorName in updateData.addedColors)
                    {
                        var color = new cColor() { Name = colorName, Item = item };
                        item.Colors.Add(color);
                        session.Save(color); //играемся с inverse и cascade
                    }

                if (updateData.removedSizes != null)
                    foreach (var id in updateData.removedSizes)
                    {
                        //удалить кол-во на складе
                        var warehouseAmounts = session.QueryOver<cWarehouseAmountTotal>().Where(c => c.ItemId == item.Id).And(c => c.SizeId == int.Parse(id)).List();
                        foreach (var wa in warehouseAmounts)
                            session.Delete(wa);
                    

                        var size = session.Load<cSize>(int.Parse(id));
                        session.Delete(size);
                        //item.Sizes.Remove(size);

                        

                    }

                if (updateData.addedSizes != null)
                    foreach (var sizeName in updateData.addedSizes)
                    {
                        var size = new cSize() { Name = sizeName, Item = item };
                        item.Sizes.Add(size);
                    }

                

                //session.Save(item);
                session.Flush();

                generateWarehouseTotal(item.Id);
            }

            
        }

        public static void generateWarehouseTotal(int itemId)
        {
            using (ISession session = OpenSession())
            {
                var colors = session.QueryOver<cColor>().Where(c => c.Item.Id == itemId).OrderBy(c => c.Name).Asc.List();
                var sizes = session.QueryOver<cSize>().Where(c => c.Item.Id == itemId).OrderBy(c => c.Name).Asc.List();

                var warehouseAmounts = session.QueryOver<cWarehouseAmountTotal>().Where(c => c.ItemId == itemId).OrderBy(c => c.SizeId).Asc.ThenBy(c => c.ColorId).Asc.List();

                foreach(var color in colors)
                    foreach(var size in sizes)
                        if (warehouseAmounts.Where(c => c.ColorId == color.Id && c.SizeId == size.Id).Count() == 0)
                        {
                            cWarehouseAmountTotal newAmount = new cWarehouseAmountTotal()
                            {
                                ItemId = itemId,
                                SizeId = size.Id,
                                ColorId = color.Id,
                                Amount = 10,
                                Status = WarehouseAmoountTotalStatus.Exists
                            };

                            session.Save(newAmount);
                        }

                session.Flush();
            }
        }

        public static List<cWarehouseAmountTotal> getWarehouseTotalAmounts(int itemId)
        {
            using (ISession session = OpenSession())
            {
                var warehouseAmounts = session.QueryOver<cWarehouseAmountTotal>().Where(c => c.ItemId == itemId).OrderBy(c => c.SizeId).Asc.ThenBy(c => c.ColorId).Asc.List();
                return warehouseAmounts.ToList();
            }

        }


        public static void warehouseTotalAmountUpdate(cWarehouseAmountTotal updatedWa)
        {
            using (ISession session = OpenSession())
            {
                var existingWa = session.Load<cWarehouseAmountTotal>(updatedWa.Id);
                existingWa.Amount = updatedWa.Amount;
                existingWa.Status = updatedWa.Status;

                session.Update(existingWa);
                session.Flush();
            }



        }
        

        public static cItem addImage(int itemId, int picNumber, string mode, string fileName)
        {
            using (ISession session = OpenSession())
            {
                var item = session.Get<cItem>(itemId);
                cImage img = item.Images.Where(c => c.imgNumber == picNumber).SingleOrDefault();
                //новая картинка
                if (img == null)
                {
                    img = new cImage();
                    
                    img.imgNumber = picNumber;

                    img.Item = item;
                    item.Images.Add(img);
                }

                if (mode == "img")
                    img.FileName = fileName;
                else
                    img.FileNamePreview = fileName;

                

                session.Save(item);
                session.Flush();
                return item;
            }
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