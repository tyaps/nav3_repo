using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

using rovnicaMVC.Models;
using rovnicaMVC.Domain;

namespace rovnicaMVC.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index(OrderState? status)
        {
            status = status ?? OrderState.All;

            var res = CategoryItemRepository.GetOrders(status.Value);

            ViewData["status"] = status.Value;

            return View(res);
        }

        public ActionResult Login(string userName, string password, string ReturnUrl)
        {
            if (this.IsValidLoginArgument(userName, password))
            {
                var passwordEncrypted = FormsAuthentication.HashPasswordForStoringInConfigFile(userName + password, "MD5");
                if (CategoryItemRepository.ValidateUser(userName, passwordEncrypted))
                    this.RedirectFromLoginPage(userName, ReturnUrl);
                else
                    this.ViewData["LoginFaild"] = "Login faild! Make sure you have entered the right user name and password!";
            }


            return View();
        }

        private void RedirectFromLoginPage(string userName, string ReturnUrl)
        {
            FormsAuthentication.SetAuthCookie(userName, false);

            if (!string.IsNullOrEmpty(ReturnUrl))
                Response.Redirect(ReturnUrl);
            else
                Response.Redirect(FormsAuthentication.DefaultUrl);
        }

        private bool IsValidLoginArgument(string userName, string password)
        {
            return !(string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password));
        }

        public ActionResult Items(int? Id)
        {
            Id = Id ?? 1;

            var categories = CategoryItemRepository.getCategories();
            var items = CategoryItemRepository.getCategoryItems(Id.Value);

            var model = new admin_items() { categories = categories.ToList(), items = items.ToList() };

            return View(model);
        }

        public ActionResult saveOrderDetails(cOrder o)
        {
            CategoryItemRepository.SaveOrder(o);
            

            return null;
        }


        public ActionResult createNewItem(string Name, int Price, int categoryId)
        {
            var item = new cItem() { Name = Name, Price = Price, Category = new cCategory() { Id = categoryId }, Status = 1 };
            item = CategoryItemRepository.CreateItem(item);
            //Предложить 4 пустые картинки
            for (int i = 1; i <= 4; i++)
                item.Images.Add(new cImage() { imgNumber = i });


            return RedirectToAction("itemDetails", new { Id = item.Id });
        }


        public ActionResult itemDetails(int Id)
        {
            cItem c = CategoryItemRepository.LoadItem(Id);
            List<cCategory> categories = CategoryItemRepository.getCategories().ToList();

            admin_itemDetails model = new admin_itemDetails()
            {
                item = c,
                categories = categories
            };

            return View(model);
        }

        public ActionResult warehouseTotalAmount(int itemId)
        {

            var warehouseAmounts = CategoryItemRepository.getWarehouseTotalAmounts(itemId).ToList();

            var item = CategoryItemRepository.LoadItem(itemId);

            admin_itemWareHouseAmount model = new admin_itemWareHouseAmount()
            {
                item = item,
                warehouseAmounts = warehouseAmounts
            };


            return View(model);
        }


        public ActionResult warehouseTotalAmountUpdate(int warehousemountId, int amount, WarehouseAmoountTotalStatus status)
        {
            cWarehouseAmountTotal wa = new cWarehouseAmountTotal()
            {
                Id = warehousemountId,
                Amount = amount,
                Status = status

            };
            CategoryItemRepository.warehouseTotalAmountUpdate(wa);

           


            return null;
        }

        

        public ActionResult saveItemDetails(saveItemDetailsData data)
        {
            CategoryItemRepository.saveItem(data);

            return this.Content("success");
        }
        

        public ActionResult uploadImage(int itemId, int picNumber, string mode)
        {
            var uploadFile = Request.Files[0];
            var fileNameExtention = uploadFile.FileName.Split('.').Last();
            var fileName = string.Format("img{0}{1}{2}.{3}", itemId.ToString(), mode, picNumber.ToString(), fileNameExtention);
            
            uploadFile.SaveAs(Server.MapPath(CategoryItemRepository.appSettings("imageFolder") + "/" + fileName));

            CategoryItemRepository.addImage(itemId, picNumber, mode, fileName);

            return this.Content(fileName); 
        }

        public ActionResult addMoreImageTemplate(int itemId, int imgNumber)
        {

            //Предложить 1 пустой шаблон картинки
            cImage img = new cImage() { imgNumber = imgNumber };
            img.Item = new cItem() { Id = itemId };

            return PartialView("addImageTemplate", img);
        }
        

       

    }
}
