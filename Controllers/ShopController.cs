using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Net;
using System.Web.Mvc;
using bikevision.Models;

namespace bikevision.Controllers
{
    public class ShopController : Controller
    {
        public MainLayoutViewModel MainLayoutViewModel { get; set; }

        public ShopController()
        {
            this.MainLayoutViewModel = new MainLayoutViewModel();
            this.MainLayoutViewModel.Types = db.ItemTypes.ToList();
            this.MainLayoutViewModel.CategoriesOfSpareParts = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.CategoriesAccessories = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.CategoriesOfTools = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.CategoriesOfClothing = new List<CategoryIdWithName>();

            this.MainLayoutViewModel.BicyclesByUsage = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.BicyclesByBrands = new List<CategoryIdWithName>();
            this.MainLayoutViewModel.BicyclesByWheels = new List<CategoryIdWithName>();
            // this.MainLayoutViewModel.CategoriesOfSpareParts 

            List<Item> itemsSpareParts = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Części zamienne").ToList();
            List<Item> itemsAccessories = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Akcesoria").ToList();
            List<Item> itemsClothing = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Odzież").ToList();
            List<Item> itemsTools = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Narzędzia").ToList();

            List<Item> bicyclesUsages = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Rowery").ToList();
            List<Item> bicyclesBrands = db.Items.Include(brand => brand.Brand).Include(type => type.ItemType).Where(type => type.ItemType.type == "Rowery").ToList();
            List<Item> bicyclesWheels = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Rowery").ToList();

            foreach(var item in itemsSpareParts)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if(this.MainLayoutViewModel.CategoriesOfSpareParts.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesOfSpareParts.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesOfSpareParts.Count() > 0)
                this.MainLayoutViewModel.CategoriesOfSpareParts = this.MainLayoutViewModel.CategoriesOfSpareParts.OrderBy(name => name.name).ToList();

            foreach(var item in itemsAccessories)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if(this.MainLayoutViewModel.CategoriesAccessories.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesAccessories.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesAccessories.Count() > 0)
                this.MainLayoutViewModel.CategoriesAccessories = this.MainLayoutViewModel.CategoriesAccessories.OrderBy(name => name.name).ToList();

            foreach (var item in itemsClothing)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if(this.MainLayoutViewModel.CategoriesOfClothing.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesOfClothing.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesOfClothing.Count() > 0)
                this.MainLayoutViewModel.CategoriesOfClothing = this.MainLayoutViewModel.CategoriesOfClothing.OrderBy(name => name.name).ToList();

            foreach (var item in itemsTools)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if(this.MainLayoutViewModel.CategoriesOfTools.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesOfTools.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesOfTools.Count() > 0)
                this.MainLayoutViewModel.CategoriesOfTools = this.MainLayoutViewModel.CategoriesOfTools.OrderBy(name => name.name).ToList();

            foreach (var bicycle in bicyclesUsages)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(bicycle.Category_idCategory, bicycle.Category.category1);

                if(this.MainLayoutViewModel.BicyclesByUsage.Where(id => id.id == bicycle.Category_idCategory).Where(name => name.name == bicycle.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.BicyclesByUsage.Add(newCat);
            }

            if (this.MainLayoutViewModel.BicyclesByUsage.Count() > 0)
                this.MainLayoutViewModel.BicyclesByUsage = this.MainLayoutViewModel.BicyclesByUsage.OrderBy(name => name.name).ToList();


            //Make bicycles by wheels
            //foreach (var bicycle in bicyclesWheels)
            //{
            //    CategoryIdWithName newCat = new CategoryIdWithName(bicycle.Brand_idBrand, bicycle.Brand.brand1);

            //    if(this.MainLayoutViewModel.BicyclesByBrands.Where(id => id.id == bicycle.Brand_idBrand).Where(name => name.name == bicycle.Brand.brand1).Count() <= 0)
            //        this.MainLayoutViewModel.BicyclesByBrands.Add(newCat);
            //}

            //if (this.MainLayoutViewModel.BicyclesByBrands.Count() > 0)
            //    this.MainLayoutViewModel.BicyclesByBrands = this.MainLayoutViewModel.BicyclesByBrands.OrderBy(name => name.name).ToList();


            //Make bicycles by usage
            foreach (var bicycle in bicyclesBrands)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(bicycle.Brand_idBrand, bicycle.Brand.brand1);

                if (this.MainLayoutViewModel.BicyclesByBrands.Where(id => id.id == bicycle.Brand_idBrand).Where(name => name.name == bicycle.Brand.brand1).Count() <= 0)
                    this.MainLayoutViewModel.BicyclesByBrands.Add(newCat);
            }

            if (this.MainLayoutViewModel.BicyclesByBrands.Count() > 0)
                this.MainLayoutViewModel.BicyclesByBrands = this.MainLayoutViewModel.BicyclesByBrands.OrderBy(name => name.name).ToList();

            this.ViewData["MainLayoutViewModel"] = this.MainLayoutViewModel;
        }

        private bikewayDBEntities db = new bikewayDBEntities();
        // GET: Shop
        public ActionResult Index(string Searching)
        {
            if (Searching != null && Searching != "")
            {
                return RedirectToAction("ProductList", new { Searching = Searching });
            }
            CarouselItemsViewModel itemList = new CarouselItemsViewModel();
            
            itemList.ItemsOnPromotion = db.Items.Where(i => i.discount > 0).Take(15).ToList();
            itemList.NewestItems = db.Items.OrderByDescending(i => i.idItem).Take(15).ToList();
            itemList.PopularItems = db.Items.Take(15).ToList();

            if (itemList != null)
            {
                return View(itemList);
            }

            return View();
        }

        public ActionResult Product(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            ProductDetailsViewModel productDetails = new ProductDetailsViewModel();
            productDetails.isEligibleToAddOpinion = true;

            Item product = db.Items.Find(id);

            if(product == null)
            {
                return View("Index");
            }
            
            productDetails.product = product;
            productDetails.opinions = db.Opinions.Where(opinion => opinion.Item_idItem == id).ToList();

            if (User.Identity.IsAuthenticated)
            {
                int? customerId = db.Customers.Where(user => user.AspNetUser.UserName == User.Identity.Name).First().idCustomer;

                if (customerId == null)
                {
                    productDetails.isEligibleToAddOpinion = false;
                }

                int stateRealised = db.SaleStates.Where(i => i.state == "zrealizowane").First().idSaleState;

                List<SaleDetail> isEligibleToAddOpinion = db.SaleDetails.Include(d => d.Sale).Where(item => item.Item_idItem == (int)id).Where(cust => cust.Sale.Customer_idCustomer == customerId).Where(state => state.Sale.SaleState_idSaleState == stateRealised).ToList();

                if (isEligibleToAddOpinion.Count() <= 0)
                {
                    productDetails.isEligibleToAddOpinion = false;
                }
            }
            else
            {
                productDetails.isEligibleToAddOpinion = false;
            }

            return View(productDetails);
        }
        public ActionResult ProductList(string Searching, int? categoryId)
        {
            List<Item> items;

            if(categoryId != null)
            {
                items = db.Items.Where(cat => cat.Category_idCategory == categoryId).ToList();
                return View(items);
            }

            if (Searching == null || Searching == "")
            {
                items = db.Items.ToList();
            }
            else
            {
                items = db.Items.Where(i => i.name.Contains(Searching)).ToList();
                ViewBag.keyword = Searching;
            }

            return View(items);
        }

        [HttpPost]
        public ActionResult OpinionToOrder(int? idProduct)
        {
            int points = 0;
            if(Int32.TryParse(Request["points"], out points) && points >= 1 && points <= 5)
            {
                Opinion newOpinion = new Opinion();
                string opinion = Request["opinion"];
                if(opinion != "" && opinion != null)
                {
                    newOpinion.opinion1 = opinion;
                }

                newOpinion.date = DateTime.Now;
                newOpinion.Customer_idCustomer = db.Customers.Where(user => user.AspNetUser.UserName == User.Identity.Name).First().idCustomer; ;
                newOpinion.Item_idItem = (int)idProduct;
                newOpinion.points = (byte)points;

                db.Opinions.Add(newOpinion);
                db.SaveChanges();
            }
            else
            {
            }
            
            return RedirectToAction("Product", "Shop", new { id = (int)idProduct });
        }
        public ActionResult Favorites()
        {
            return View();
        }
    }
}