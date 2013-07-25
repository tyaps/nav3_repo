using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using rovnicaMVC.Domain;

namespace rovnicaMVC.Models
{
    public class p_needs
    {
        public p_needs()
        {
            hq_needs = new List<hq_need>();
            

        }


        public IList<hq_need> hq_needs { get; set; }
        public string test="aaa";
    }

    /// <summary>
    /// //////////////////////////////////////////////////////
    /// </summary>
    public class admin_items
    {
        public admin_items()
        {
            categories = new List<cCategory>();
            items = new List<cItem>();

        }

     
        public List<cCategory> categories { get; set; }
        public List<cItem> items { get; set; }
    }

    public class admin_itemDetails
    {
        public admin_itemDetails()
        {
            
            
        }
        public cItem item;
        public List<cCategory> categories;
    }

    public class admin_itemWareHouseAmount
    {
        public admin_itemWareHouseAmount()
        {


        }
        public cItem item;
        public List<cWarehouseAmountTotal> warehouseAmounts;
    }

    public class saveItemDetailsData
    {
        public saveItemDetailsData() { }
 
        public int itemId { get; set; }
        public string Name { get; set; }
        public int status{ get; set; }
        public int categoryId { get; set; }
        public int Price{ get; set; }
        public int PriceOpt{ get; set; }

        public string DescriptionCommon{ get; set; }
        public string DescriptionMaterial{ get; set; }



        public string[] removedColors { get; set; }
        public string[] addedColors { get; set; }
        public string[] removedSizes { get; set; }
        public string[] addedSizes { get; set; }
    }

    public class purchasedItem
    {
        //поля, необходимые для добавления в заказ
        public int itemId;
        public int colorId;
        public int sizeId;

        //поля, необходимые для вывода в корзине
        public cItem itemData;
        public string colorName;
        public string sizeName;


        public purchasedItem(int itemId, int colorId, int sizeId)
        {
            this.itemId = itemId;
            this.colorId = colorId;
            this.sizeId = sizeId;
        }

    }
}
