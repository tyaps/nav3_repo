using System.Collections.Generic;

using System.Data.SqlClient;
//using System.Configuration;
using System;

//using MySql.Data.MySqlClient;

using System.Reflection;
using NHibernate;
using NHibernate.Cfg;

//using log4net;

namespace rovnicaMVC.Domain
{

    public class hq_need
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual int Amount { get; set; }
        public virtual DateTime ? DueDate { get; set; }
        public virtual hq_need_priority Priority { get; set; }
        
    }

    public enum hq_need_priority
    {

        Likely = 1,
        Wanted = 2,
        Critical = 3
    }

    /// <summary>
    /// ///////////////////////////
    /// </summary>
    public enum OrderState
    {

        New = 1,
        InProcess = 2,
        Ready = 3,
        Closed = 4,
        All = 5
    }

    public enum WarehouseAmoountTotalStatus
    {
        Exists = 1,
        NotExists = 0
    }

    public class cItem
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Price { get; set; }
        public virtual int PriceOpt { get; set; }
        public virtual string DescriptionCommon { get; set; }
        public virtual string DescriptionMaterial { get; set; }

        public virtual int Status  { get; set; }//enum

         public virtual cCategory Category { get; set; }
         public virtual IList<cImage> Images { get; set; }
         
        public virtual IList<cOrderItem> OrderItems { get; set; }

        public virtual IList<cColor> Colors { get; set; }
        public virtual IList<cSize> Sizes { get; set; }

         public cItem()
         {
             Images = new List<cImage>();
             OrderItems = new List<cOrderItem>();
         }
    }

    public class cCategory
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<cItem> Items { get; set; }

        public cCategory()
        {
            Items = new List<cItem>();
        }
    }

    public class cImage
    {
        public virtual int Id { get; set; }
        public virtual string FileName { get; set; }
        public virtual string FileNamePreview { get; set; }
        public virtual int imgNumber { get; set; }

        public virtual cItem Item { get; set; }
    }
    //посути cColor есть сама вешь - юбка "Элла" красная => имеет поэтому артикул
    //либо же связка Юбка Элла + размер + цвет = артикул
    public class cColor
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        
        public virtual cItem Item { get; set; }
    }

    public class cSize
    {
        public virtual int Id { get; set; }
        //например, 46-48
        public virtual string Name { get; set; }
        //Описание реальных размеров (талия, грудь...)
        public virtual string Description { get; set; }

        public virtual cItem Item { get; set; }
    }

    public class cUser
    {
        public virtual int Id { get; set; }

        public virtual string userName { get; set; }

        public virtual string Password { get; set; }


    }

    //Кол-во одежды заданного цвета-размера на складе (кол-во устанавливается вручную)
    public class cWarehouseAmountTotal
    {
        public virtual int Id { get; set; }

        public virtual int ItemId { get; set; }
        public virtual int ColorId { get; set; }
        public virtual int SizeId { get; set; }
        public virtual int Amount { get; set; }
        public virtual WarehouseAmoountTotalStatus Status { get; set; }
    }

    public class cOrder
    {
        public virtual int Id { get; set; }
        public virtual string customerName { get; set; }
        public virtual string customerPhone { get; set; }
        public virtual string customerEmail { get; set; }
        public virtual string comments { get; set; }
        public virtual OrderState status { get; set; }
        //public virtual int status { get; set; }

        public virtual DateTime? date { get; set; }
        public virtual IList<cOrderItem> Items { get; set; }

        public cOrder()
        {
           // date = new DateTime();
            Items = new List<cOrderItem>();
        }
        
    }

    public class cOrderItem
    {
        public virtual int Id { get; set; }

        public virtual int ItemNumber { get; set; } //Не идет в БД

        public virtual cItem Item { get; set; }
        public virtual cOrder Order { get; set; }
        public virtual int SizeId { get; set; }
        public virtual int ColorId { get; set; }
        public virtual string Comments { get; set; }

        //Для Model
        public virtual string SizeName { get; set; }
        public virtual string ColorName { get; set; }
    }

}