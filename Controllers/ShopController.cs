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
            IQueryable<Item> allItems = db.Items.Include(cat => cat.Category).Include(type => type.ItemType);


            List<Item> itemsSpareParts = allItems.Where(type => type.ItemType.type == "Części zamienne").ToList();
                //  db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Części zamienne").ToList();
            List<Item> itemsAccessories = allItems.Where(type => type.ItemType.type == "Akcesoria").ToList();
                //  db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Akcesoria").ToList();
            List<Item> itemsClothing = allItems.Where(type => type.ItemType.type == "Odzież").ToList();
                //  db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Odzież").ToList();
            List<Item> itemsTools = allItems.Where(type => type.ItemType.type == "Narzędzia").ToList();
                //  db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Narzędzia").ToList();

            List<Item> bicyclesUsages = db.Items.Include(cat => cat.Category).Include(type => type.ItemType).Where(type => type.ItemType.type == "Rowery").ToList();
            List<Item> bicyclesBrands = db.Items.Include(brand => brand.Brand).Include(type => type.ItemType).Where(type => type.ItemType.type == "Rowery").ToList();
            List<FeatureValueOfItem> bicyclesWheels = db.FeatureValueOfItems.Include(feat => feat.Feature).Where(feature => feature.Feature.feature1 == "Rozmiar kół").ToList();

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
            foreach (var bicycle in bicyclesWheels)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(bicycle.FeatureValue_idFeatureValue, bicycle.FeatureValue.featureValue1);

                if (this.MainLayoutViewModel.BicyclesByWheels.Where(id => id.id == bicycle.FeatureValue_idFeatureValue).Where(name => name.name == bicycle.FeatureValue.featureValue1).Count() <= 0)
                    this.MainLayoutViewModel.BicyclesByWheels.Add(newCat);
            }

            if (this.MainLayoutViewModel.BicyclesByWheels.Count() > 0)
                this.MainLayoutViewModel.BicyclesByWheels = this.MainLayoutViewModel.BicyclesByWheels.OrderBy(name => name.name).ToList();


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
        public ActionResult ProductList(string Searching, int? categoryId, string types, int? featureId, int? brandId)
        {
            List<Item> items;

            if (TempData["tempItems"] != null)
            {
                IQueryable<Item> allItems;
                allItems = TempData["tempItems"] as IQueryable<Item>;
                //allItems.Include(cat => cat.Category).ToList();
                return View(allItems.Include(cat => cat.Category).ToList());
            }

            if (categoryId != null)
            {
                items = db.Items.Where(cat => cat.Category_idCategory == categoryId).ToList();
                return View(items);
            }

            if(featureId != null)
            {
                List<FeatureValueOfItem> features = db.FeatureValueOfItems.Where(i => i.Feature.feature1 == "Rozmiar kół").Where(i => i.FeatureValue.idFeatureValue == featureId).ToList();
                List<Int32> ids = new List<Int32>();

                foreach(var i in features)
                {
                    if(!ids.Contains(i.Item_idItem1))
                        ids.Add(i.Item_idItem1);
                }

                List<Item> allItems = db.Items.ToList();
                items = new List<Item>();

                foreach(var ID in ids)
                {
                    items.Add(allItems.Where(i => i.idItem == ID).First());
                }
                
                //items.SelectMany(items, i => i.idItem == 1);
                return View(items);
            }

            if(brandId != null)
            {
                if(types != "" && types != null)
                    items = db.Items.Where(brand => brand.Brand_idBrand == brandId).Where(type => type.ItemType.type == types).ToList();
                else
                    items = db.Items.Where(brand => brand.Brand_idBrand == brandId).ToList();
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

        public ActionResult SortProducts(int? number)
        {
            List<Item> AllItems = new List<Item>();

            if ((int)number <= 0)
                return RedirectToAction("ProductList");

            for (int i = 0; i < number; i++)
            {
                Item newItem = new Item();
                newItem.idItem = Int32.Parse(Request["product[" + i.ToString() + "].idItem"]);
                newItem.name = Request["product[" + i.ToString() + "].name"];
                newItem.description = Request["product[" + i.ToString() + "].description"];
                newItem.availability = Byte.Parse(Request["product[" + i.ToString() + "].availability"]);
                newItem.price = Decimal.Parse(Request["product[" + i.ToString() + "].price"]);
                newItem.discount = Int32.Parse(Request["product[" + i.ToString() + "].discount"]);
                newItem.outlet = Int16.Parse(Request["product[" + i.ToString() + "].outlet"]);
                newItem.weight = Double.Parse(Request["product[" + i.ToString() + "].weight"]);
                newItem.dimensions = Request["product[" + i.ToString() + "].dimensions"];
                newItem.ItemType_idItemType = Int32.Parse(Request["product[" + i.ToString() + "].ItemType_idItemType"]);
                newItem.Category_idCategory = Int32.Parse(Request["product[" + i.ToString() + "].Category_idCategory"]);
                newItem.Brand_idBrand = Int32.Parse(Request["product[" + i.ToString() + "].Brand_idBrand"]);

                AllItems.Add(newItem);
            }

            //IQueryable<Item> cos = AllItems.AsQueryable();

            //Session["tempItems"] = new List<Item>();
            string sorting = Request["sortingItems"];

            if (sorting == "price-asc") TempData["tempItems"] = AllItems.OrderBy(col => col.price).AsQueryable();
            if(sorting == "price-desc") TempData["tempItems"] = AllItems.OrderByDescending(col => col.price).AsQueryable();
            if (sorting == "alpha-asc") TempData["tempItems"] = AllItems.OrderBy(col => col.name).AsQueryable();
            if (sorting == "alpha-desc") TempData["tempItems"] = AllItems.OrderByDescending(col => col.name).AsQueryable();
            // if (sorting == "opinions-asc")

            return RedirectToAction("ProductList", "Shop");
        }

        public ActionResult Favorites()
        {
            return View();
        }
    }
}