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
            List<FeatureValueOfItem> bicyclesWheels = db.FeatureValueOfItems.Include(feat => feat.Feature).Where(type => type.Item.ItemType.type == "Rowery").Where(feature => feature.Feature.feature1 == "Rozmiar kół").ToList();

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

        public ActionResult Product(int? id, string model)
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

            if (model != null)
                if (model != "")
                    Session["productModel"] = model;

            List<Item> lastViewedItems = new List<Item>();

            if (Session["lastViewedItems"] != null)
            {
                lastViewedItems = (List<Item>)(Session["lastViewedItems"]);
            }

            if (lastViewedItems.Where(itemId => itemId.idItem == product.idItem).ToList().Count() <= 0)
            {
                lastViewedItems = lastViewedItems.Append(product).ToList();
            }
            else
            {
                int index = lastViewedItems.FindIndex(itemId => itemId.idItem == product.idItem);
                lastViewedItems.RemoveAt(index);
                lastViewedItems = lastViewedItems.Append(product).ToList();
            }

            if (lastViewedItems.Count() >= 10)
            {
                lastViewedItems = lastViewedItems.Skip(lastViewedItems.Count()-10).Take(10).ToList();
            }

            Session["lastViewedItems"] = lastViewedItems;

            productDetails.FeaturesList = new List<Tuple<string, List<string>, bool>>();

            productDetails.product = product;
            List<FeatureValueOfItem> featuresOfItem = db.FeatureValueOfItems.Where(item => item.Item_idItem1 == id).ToList();

            List<string> featuresList = featuresOfItem.Select(feat => feat.Feature.feature1).Distinct().ToList();
                        
            foreach (var feat in featuresList)
            {
                List<string> featVal = featuresOfItem.Where(feature => feature.Feature.feature1 == feat).Select(fe => fe.FeatureValue.featureValue1).ToList();
                bool isSelectable = (featuresOfItem.Where(feature => feature.Feature.feature1 == feat).Select(fe => fe.Feature.selectable).First() != null) ? (bool)(featuresOfItem.Where(feature => feature.Feature.feature1 == feat).Select(fe => fe.Feature.selectable).First()) : false;

                Tuple<string, List<string>, bool> featureValues = new Tuple<string, List<string>, bool>(feat, featVal, isSelectable);

                productDetails.FeaturesList.Add(featureValues);
            }

            //productDetails.productDetail.FeaturesList = db.FeatureValueOfItems.Where(item => item.Item_idItem1 == id).ToList();
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
        public ActionResult ProductList(string Searching, int? categoryId, string types, int? featureId, int? brandId, string sortOrder)
        {
            List<Item> items;

            if (sortOrder == null)
                sortOrder = "";

            if (Request["category"] != null)
                if (Int32.Parse(Request["category"]) > 0)
                    categoryId = Int32.Parse(Request["category"]);

            if (Request["brand"] != null)
                if (Int32.Parse(Request["brand"]) > 0)
                    brandId = Int32.Parse(Request["brand"]);

            if (Request["feature"] != null)
                if (Int32.Parse(Request["feature"]) > 0)
                    featureId = Int32.Parse(Request["feature"]);

            if (Request["searching"] != null && Request["searching"] != "")
                Searching = Request["searching"];

            if (Request["sortingItems"] != null && Request["sortingItems"] != "")
                sortOrder = Request["sortingItems"];

            ViewBag.sortOrder = sortOrder;

            Func<Item, Object> orderByFunc = null;

            if (sortOrder.Contains("price"))
                orderByFunc = it => it.price;
            else if (sortOrder.Contains("alpha"))
                orderByFunc = it => it.name;
            else
                orderByFunc = it => it.idItem;
        
            if (categoryId != null)
            {
                items = db.Items.Where(cat => cat.Category_idCategory == categoryId).ToList();
                ViewBag.categoryId = categoryId;
                return View((sortOrder.Contains("asc") ? items.OrderBy(orderByFunc).ToList() : items.OrderByDescending(orderByFunc).ToList()));
            }

            if(featureId != null)
            {
                ViewBag.featureId = featureId;

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
                return View((sortOrder.Contains("asc") ? items.OrderBy(orderByFunc).ToList() : items.OrderByDescending(orderByFunc).ToList()));
            }

            if(brandId != null)
            {
                if(types != "" && types != null)
                    items = db.Items.Where(brand => brand.Brand_idBrand == brandId).Where(type => type.ItemType.type == types).ToList();
                else
                    items = db.Items.Where(brand => brand.Brand_idBrand == brandId).ToList();

                ViewBag.brandId = brandId;

                return View((sortOrder.Contains("asc") ? items.OrderBy(orderByFunc).ToList() : items.OrderByDescending(orderByFunc).ToList()));
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

            return View((sortOrder.Contains("asc") ? items.OrderBy(orderByFunc).ToList() : items.OrderByDescending(orderByFunc).ToList()));
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
            
            return RedirectToAction("Product", "Shop", new { id = (int)idProduct });
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Shop");
        }

        public ActionResult AddToFavorites(int? itemId, string returnUrl)
        {
            if (itemId == null)
                return RedirectToUrl(returnUrl);

            List<Item> newFavoriteItem = db.Items.Where(id => id.idItem == itemId).ToList();

            if(newFavoriteItem.Count() == 0)
                return RedirectToUrl(returnUrl);

            List<Item> favoriteItems = new List<Item>();

            if (Session["favoriteItems"] != null)
                favoriteItems = (List<Item>)(Session["favoriteItems"]);

            favoriteItems = favoriteItems.Append(newFavoriteItem.First()).ToList();
            Session["favoriteItems"] = favoriteItems;

            if(User.Identity.IsAuthenticated)
            {
                string thisUserId = (db.AspNetUsers.Where(name => name.UserName == User.Identity.Name).ToList().Count() > 0) ? db.AspNetUsers.Where(name => name.UserName == User.Identity.Name).ToList().First().Id : "";

                if (thisUserId == "" || thisUserId == null)
                    return RedirectToUrl(returnUrl);

                //db.AspNetUserFavorites.Where(item => item.Item_idItem == itemId).Where(user => user.AspNetUsers_Id == thisUserId).ToList();

                AspNetUserFavorite newFavoriteRow = new AspNetUserFavorite();
                newFavoriteRow.AspNetUsers_Id = thisUserId;
                newFavoriteRow.Item_idItem = (int)itemId;
                newFavoriteRow.dateOfCreation = DateTime.Now;

                db.AspNetUserFavorites.Add(newFavoriteRow);
                db.SaveChanges();
            }

            return RedirectToUrl(returnUrl);
        }

        public ActionResult DeleteFromFavorites(int? idItem, string returnUrl)
        {
            if (idItem == null)
                return RedirectToUrl(returnUrl);

            List<Item> favoriteItems = new List<Item>();

            if (Session["favoriteItems"] != null)
                favoriteItems = (List<Item>)(Session["favoriteItems"]);

            favoriteItems.Remove(favoriteItems.Where(id => id.idItem == idItem).First());

            Session["favoriteItems"] = favoriteItems;

            if (User.Identity.IsAuthenticated)
            {
                string thisUserId = (db.AspNetUsers.Where(name => name.UserName == User.Identity.Name).ToList().Count() > 0) ? db.AspNetUsers.Where(name => name.UserName == User.Identity.Name).ToList().First().Id : "";

                if (thisUserId == "" || thisUserId == null)
                    return RedirectToUrl(returnUrl);

                AspNetUserFavorite favUserItem = (db.AspNetUserFavorites.Where(item => item.Item_idItem == idItem).Where(user => user.AspNetUsers_Id == thisUserId).ToList().Count() > 0) ? db.AspNetUserFavorites.Where(item => item.Item_idItem == idItem).Where(user => user.AspNetUsers_Id == thisUserId).ToList().First() : null;

                if(favUserItem == null)
                    return RedirectToUrl(returnUrl);

                db.AspNetUserFavorites.Remove(favUserItem);
                db.SaveChanges();
            }

            return RedirectToUrl(returnUrl);
        }

        public ActionResult AddToComparation(int? itemId, string returnUrl)
        {
            if (itemId == null)
                return RedirectToUrl(returnUrl);

            Item newItem = (db.Items.Where(id => id.idItem == itemId) != null) ? db.Items.Where(id => id.idItem == itemId).ToList().First(): null;

            if (newItem == null)
                return RedirectToUrl(returnUrl);

            List<Item> itemsInComparation = new List<Item>();

            if (Session["comparation"] != null)
            {
                itemsInComparation = (List<Item>)(Session["comparation"]);

                if (itemsInComparation.Count() >= 3)
                {
                    return RedirectToUrl(returnUrl);
                }
            }

            itemsInComparation.Add(newItem);
            Session["comparation"] = itemsInComparation;

            return RedirectToUrl(returnUrl);
        }

        public ActionResult RemoveFromComparation(int? itemId, string returnUrl)
        {
            if (itemId == null)
                return RedirectToUrl(returnUrl);

            Item newItem = (db.Items.Where(id => id.idItem == itemId) != null) ? db.Items.Where(id => id.idItem == itemId).ToList().First() : null;

            if (newItem == null)
                return RedirectToUrl(returnUrl);

            List<Item> itemsInComparation = new List<Item>();

            if (Session["comparation"] != null)
            {
                itemsInComparation = (List<Item>)(Session["comparation"]);
            }

            itemsInComparation.Remove(itemsInComparation.Where(id => id.idItem == itemId).First());

            Session["comparation"] = (itemsInComparation.Count() > 0) ? itemsInComparation : null;

            return RedirectToUrl(returnUrl);
        }

        private ActionResult RedirectToUrl(string returnUrl)
        {
            if (returnUrl != null && returnUrl != "")
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Shop");
        }

        public ActionResult Favorites()
        {
            return View();
        }

        public ActionResult Comparation()
        {
            List<Item> itemsToCompare = new List<Item>();

            if (Session["comparation"] == null)
                return View();

            itemsToCompare = (List<Item>)(Session["comparation"]);

            if (itemsToCompare.Count() <= 0)
                return View();

            List<string> featuresList = new List<string>();

            List<ComparationViewModel> comparation = new List<ComparationViewModel>();

            foreach (var oneItem in itemsToCompare)
            {
                List<string> tempFeature = db.FeatureValueOfItems.Where(item => item.Item_idItem1 == oneItem.idItem).Select(feature => feature.Feature.feature1).ToList();

                if (tempFeature != null)
                    if (tempFeature.Count() > 0)
                        foreach (var feature in tempFeature)
                            if (!featuresList.Contains(feature))
                                featuresList.Add(feature);
            }

            foreach(var oneItem in itemsToCompare)
            { 
                ComparationViewModel newComparationItem = new ComparationViewModel();

                newComparationItem.ComparationItem = oneItem;
                newComparationItem.FeaturesList = new List<Tuple<string, List<string>>>();

                foreach(var feature in featuresList)
                {
                    List<string> featureValue = (db.FeatureValueOfItems.Where(item => item.Item_idItem1 == oneItem.idItem).Where(feat => feat.Feature.feature1 == feature).Count() > 0) ? db.FeatureValueOfItems.Where(item => item.Item_idItem1 == oneItem.idItem).Where(feat => feat.Feature.feature1 == feature).Select(val => val.FeatureValue.featureValue1).ToList() : null ;
                    Tuple<string, List<string>> newFeatureRow = new Tuple<string, List<string>>(feature, featureValue);

                    newComparationItem.FeaturesList.Add(newFeatureRow);
                }

                comparation.Add(newComparationItem);
            }
            
            return View(comparation);
        }
    }
}