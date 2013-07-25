using System.Collections.Generic;

using System.Data.SqlClient;
//using System.Configuration;
using System;

//using MySql.Data.MySqlClient;

using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using FluentNHibernate.Mapping;

//using log4net;

namespace rovnicaMVC.Domain
{

    public class hq_needMap : ClassMap<hq_need>
    {
        public hq_needMap()
        {
            Table("hq_needs");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Title);
            Map(x => x.Amount);
            Map(x => x.DueDate);
            Map(x => x.Priority).CustomType(typeof(hq_need_priority));// для enum надо делать так;
            

        }
    }


    public class cItemMap : ClassMap<cItem>
    {
        public cItemMap()
        {
            Table("items");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Price);
            Map(x => x.PriceOpt);
            Map(x => x.DescriptionCommon);
            Map(x => x.DescriptionMaterial);
            Map(x => x.Status);

            References(x => x.Category).Column("cat_id").Cascade.All();//указываем поле FK

            HasMany(x => x.Images)
                .KeyColumn("item_id")
                .Inverse()
                .Cascade.All();

            //При inverse можно вызывать session.delete(color) или item.colors.remove(color), session.save(item)
            //а при отсутствии Inverse нужно обязательно делать session.delete(color)
            HasMany(x => x.Colors)
                .KeyColumn("item_id")
                .Inverse()
                .Cascade.All();

            HasMany(x => x.Sizes)
               .KeyColumn("item_id")
               .Inverse()
               .Cascade.All();

            //можно не указывать т.к. не будет связи от item к OrderItem напрямую в ПО
            //HasMany(x => x.OrderItems)
            //  .KeyColumn("itemId") //указываем поле FK
            //  .Inverse()
            //  .Cascade.All();
        }
    }

    public class cCategoryMap : ClassMap<cCategory>
    {
        public cCategoryMap()
        {
            Table("category");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
              HasMany(x => x.Items)
                .KeyColumn("cat_id") //указываем поле FK
                .Inverse()
                .Cascade.All();
            
        }
    }

    public class cImageMap : ClassMap<cImage>
    {
        public cImageMap()
        {
            Table("images");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.FileName);
            Map(x => x.FileNamePreview);
            Map(x => x.imgNumber);

            References(x => x.Item).Column("item_id");
        }
    }

    public class cSizeMap : ClassMap<cSize>
    {
        public cSizeMap()
        {
            Table("sizes");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Description);
            
            References(x => x.Item).Column("item_id");
        }
    }

    public class cWarehouseAmountTotalMap : ClassMap<cWarehouseAmountTotal>
    {
        public cWarehouseAmountTotalMap()
        {
            Table("warehouse_total");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.ColorId);
            Map(x => x.SizeId);
            Map(x => x.ItemId);
            Map(x => x.Amount);
            Map(x => x.Status).CustomType(typeof(WarehouseAmoountTotalStatus));

            
        }
    }

    public class cColorMap : ClassMap<cColor>
    {
        public cColorMap()
        {
            Table("colors");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            

            References(x => x.Item).Column("item_id");
        }
    }

    public class cOrderMap : ClassMap<cOrder>
    {
        public cOrderMap()
        {
            Table("orders");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.customerName);
            Map(x => x.customerPhone);
            Map(x => x.customerEmail);
            Map(x => x.comments);
            Map(x => x.date);
            Map(x => x.status).CustomType(typeof(OrderState));// для enum надо делать так
            HasMany(x => x.Items)
              .KeyColumn("orderId") //указываем поле FK
              //.Inverse()
              .Cascade.All();

        }
    }

    public class cOrderItemMap : ClassMap<cOrderItem>
    {
        public cOrderItemMap()
        {
            Table("order_items");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.ColorId);
            Map(x => x.SizeId);

            References(x => x.Order).Column("orderId").Cascade.All();
            References(x => x.Item).Column("itemId").Cascade.None();
        }
    }

    public class cUserMap : ClassMap<cUser>
    {
        public cUserMap()
        {
            Table("users");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.userName);
            Map(x => x.Password);
 
        }
    }
    


}