using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using rovnicaMVC.Models;
using rovnicaMVC.Domain;

using log4net;
using log4net.Config;

namespace rovnicaMVC.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewData["selectedMenu"] = "main";
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
          
            return View();
        }

        public ActionResult About()
        {
            ViewData["selectedMenu"] = "about";
            return View();
        }

        public ActionResult Opt()
        {
            ViewData["selectedMenu"] = "opt";
            return View();
        }

        public ActionResult Contacts()
        {
            ViewData["selectedMenu"] = "contacts";
            return View();
        }


        public ActionResult Category(int id, int? pageNumber, string priceFromStr, string priceToStr, int? status)
        {
            int priceFrom;
            int priceTo;

            if (!int.TryParse(priceFromStr, out priceFrom))
            {
                priceFrom = 0;
                ViewData["priceFrom"] = "";
            }
            else
                ViewData["priceFrom"] = priceFromStr;

            if (!int.TryParse(priceToStr, out priceTo))
            {
                priceTo = 1000000;
                ViewData["priceTo"] = "";
            }
            else
                ViewData["priceTo"] = priceToStr;

            pageNumber = pageNumber ?? 1;

            

            if (!status.HasValue)
                status = 0;//показывать все


            categoryInfo res = CategoryItemRepository.LoadCategoryItems(id, pageNumber.Value, priceFrom, priceTo, status.Value);

            ViewData["selectedMenu"] = "";
            ViewData["selectedCategory"] = getCategoryName(id);
            

            return View(res);
        }

        private string getCategoryName(int id)
        {
            if(Session["categories"]==null)
                Session["categories"] = CategoryItemRepository.getCategories();

            var categories = (IList<cCategory>)Session["categories"];
            
            return categories.Where(c => c.Id == id).First().Name;
        }

        public ActionResult CategoryItem(int id)
        {
            ViewData["item"] = CategoryItemRepository.LoadItem(id);
            return View();

        }

        public ActionResult Purchase(int id, int colorId, int sizeId)
        {
            Dictionary<int, purchasedItem> purchasedItems = (Dictionary<int, purchasedItem>)Session["purchasedItems"] ?? new Dictionary<int, purchasedItem>();
            
            int maxNumber = purchasedItems.Keys.Count;
            purchasedItems.Add(maxNumber + 1, new purchasedItem(id, colorId, sizeId));

            Session["purchasedItems"] = purchasedItems;

            return View();
        }

        public ActionResult Basket()
        {
            Dictionary<int, purchasedItem> purchasedItems = (Dictionary<int, purchasedItem>)Session["purchasedItems"] ?? new Dictionary<int, purchasedItem>();

            //var res = CategoryItemRepository.LoadItems(purchasedItems.Values.Select(c=>c.itemId).ToList());
            var res = CategoryItemRepository.LoadPurchasedItemsData(purchasedItems.Values.ToList());

            return View(res);
        }

        public ActionResult CreateOrder(cOrder o)
        {
            try
            {
                o.date = DateTime.Now;
                o.status = OrderState.New;

                Dictionary<int, purchasedItem> purchasedItems = (Dictionary<int, purchasedItem>)Session["purchasedItems"] ?? new Dictionary<int, purchasedItem>();
                foreach (var i in purchasedItems.Values)
                {
                    o.Items.Add(new cOrderItem()
                    {
                        Item = new cItem() { Id = i.itemId },
                        ColorId = i.colorId,
                        SizeId = i.sizeId,
                        Order = o
                    });
                }

                CategoryItemRepository.CreateOrder(o);
                CategoryItemRepository.SendEmail(o);
                //отослать письмо

                Session["purchasedItems"] = null;
                ViewData["resultSuccess"] = true;
                

            }
            catch (Exception ex)
            {
                ViewData["resultSuccess"] = false; //НЕ РАБОТАЕТ ВЫДАЧА ОШИБКИ
            }
            finally
            {
                
            }
            //return RedirectToAction("Basket");
            return View("Basket", new List<purchasedItem>());
             
            
        }

        
        
    }
}
