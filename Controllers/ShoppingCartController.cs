using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using bikevision.Models;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;

namespace bikevision.Controllers
{
    public class ShoppingCartController : Controller
    {
        public MainLayoutViewModel MainLayoutViewModel { get; set; }

        public ShoppingCartController()
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

            foreach (var item in itemsSpareParts)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if (this.MainLayoutViewModel.CategoriesOfSpareParts.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesOfSpareParts.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesOfSpareParts.Count() > 0)
                this.MainLayoutViewModel.CategoriesOfSpareParts = this.MainLayoutViewModel.CategoriesOfSpareParts.OrderBy(name => name.name).ToList();

            foreach (var item in itemsAccessories)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if (this.MainLayoutViewModel.CategoriesAccessories.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesAccessories.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesAccessories.Count() > 0)
                this.MainLayoutViewModel.CategoriesAccessories = this.MainLayoutViewModel.CategoriesAccessories.OrderBy(name => name.name).ToList();

            foreach (var item in itemsClothing)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if (this.MainLayoutViewModel.CategoriesOfClothing.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesOfClothing.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesOfClothing.Count() > 0)
                this.MainLayoutViewModel.CategoriesOfClothing = this.MainLayoutViewModel.CategoriesOfClothing.OrderBy(name => name.name).ToList();

            foreach (var item in itemsTools)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(item.Category_idCategory, item.Category.category1);

                if (this.MainLayoutViewModel.CategoriesOfTools.Where(id => id.id == item.Category_idCategory).Where(name => name.name == item.Category.category1).Count() <= 0)
                    this.MainLayoutViewModel.CategoriesOfTools.Add(newCat);
            }

            if (this.MainLayoutViewModel.CategoriesOfTools.Count() > 0)
                this.MainLayoutViewModel.CategoriesOfTools = this.MainLayoutViewModel.CategoriesOfTools.OrderBy(name => name.name).ToList();

            foreach (var bicycle in bicyclesUsages)
            {
                CategoryIdWithName newCat = new CategoryIdWithName(bicycle.Category_idCategory, bicycle.Category.category1);

                if (this.MainLayoutViewModel.BicyclesByUsage.Where(id => id.id == bicycle.Category_idCategory).Where(name => name.name == bicycle.Category.category1).Count() <= 0)
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

        bikewayDBEntities db = new bikewayDBEntities();
        private string sessionCartString = "Cart";

        // GET: ShoppingCart
        public ActionResult Index(string Error)
        {
            ViewBag.QuantityError = Error;
            return View();
        }
        [HttpPost]
        public ActionResult Index(int? id)
        {
            // NoError -> move to next step of ordering
            if (Session[sessionCartString] != null)
                return RedirectToAction("Order");
            else
                return View();
        }

        public ActionResult Order()
        {
            if (Session[sessionCartString] != null)
            {
                ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1");

                if (User.Identity.IsAuthenticated)
                {
                    IQueryable<Customer> existingCustomer = db.Customers.Where(cust => cust.AspNetUser.UserName == User.Identity.Name);

                    if (existingCustomer.Count() > 0)
                        return View(existingCustomer.First());
                    else
                        return View();
                }
                else
                    return View();
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order([Bind(Include = "idCustomer,name,surname,telephoneNumber,emailAddress,addressOfResidence,zipCode,AspNetUsers_idAspNetUsers,Locality_idLocality")] Customer customer)
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();

                    Sale sale = new Sale();

                    sale.Customer_idCustomer = customer.idCustomer;
                    sale.date = DateTime.Now;

                    SaleType saleTypeId = db.SaleTypes.Where(i => i.type == "Internetowa").First();
                    sale.SaleType_idSaleType = saleTypeId.idSaleType;

                    Employee employeeId = db.Employees.Where(i => i.name == "Internetowy").First();
                    sale.Employee_idEmployee = employeeId.idEmployee;

                    SaleState stateId = db.SaleStates.Where(i => i.state == "oczekuje na zatwierdzenie").First();
                    sale.SaleState_idSaleState = stateId.idSaleState;

                    db.Sales.Add(sale);
                    db.SaveChanges();

                    List<Cart> lsCart = new List<Cart>();
                    lsCart = (List<Cart>)Session[sessionCartString];

                    int lastSaleDetails = 0;

                    foreach (var item in lsCart)
                    {
                        SaleDetail detailOfSale = new SaleDetail();
                        Item itemInShop = new Item();

                        itemInShop = db.Items.Find(item.Item.idItem);
                        int availbility = (int)itemInShop.availability - item.Quantity;
                        try
                        {
                            itemInShop.availability = checked((byte)availbility);
                        }
                        catch (OverflowException e)
                        {
                            db.Customers.Remove(customer);
                            db.Sales.Remove(sale);
                            db.SaveChanges();

                            if (itemInShop.availability <= 0)
                                lsCart.Remove(item);
                            else
                                item.Quantity = itemInShop.availability;

                            return RedirectToAction("Index", "ShoppingCart", new { Error = "Podczas składania zamówienia wystąpił błąd. Twoje zamówienie zawierało więcej przedmiotów, niż wynosi stan magazynowy." });
                        }

                        db.Entry(itemInShop).Entity.availability = itemInShop.availability;
                        db.SaveChanges();

                        detailOfSale.Item_idItem = item.Item.idItem;
                        detailOfSale.Sale_idSale = db.Entry(sale).Entity.idSale;

                        lastSaleDetails = detailOfSale.Sale_idSale;

                        detailOfSale.quantity = (byte)item.Quantity;
                        detailOfSale.value = item.Item.price * item.Quantity;

                        db.SaleDetails.Add(detailOfSale);
                        db.SaveChanges();

                    }

                    Session[sessionCartString] = null;

                    return RedirectToAction("Final", new { idOfSale = lastSaleDetails });
                }

                ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1", customer.Locality_idLocality);

                return View(customer);
            }
            else
            {
                ViewBag.Locality_idLocality = new SelectList(db.Localities, "idLocality", "locality1", customer.Locality_idLocality);

                ModelState.Remove("idCustomer");

                if (ModelState.IsValid)
                {
                    IQueryable<Customer> Customers = db.Customers.Where(cust => cust.AspNetUser.UserName == User.Identity.Name);

                    customer.AspNetUsers_idAspNetUsers = db.AspNetUsers.Where(user => user.UserName == User.Identity.Name).First().Id;

                    if (Customers.Count() > 0)
                    {
                        //customer = Customers.First();
                        if (EntityState.Modified.ToString() == "Modified")
                        {
                            //customer.idCustomer = Customers.First().idCustomer;
                            db.Entry(customer).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        db.Customers.Add(customer);
                        db.SaveChanges();
                    }

                    Sale sale = new Sale();

                    sale.Customer_idCustomer = customer.idCustomer;
                    sale.date = DateTime.Now;
                    

                    SaleType saleTypeId = db.SaleTypes.Where(i => i.type == "Internetowa").First();
                    sale.SaleType_idSaleType = saleTypeId.idSaleType;

                    Employee employeeId = db.Employees.Where(i => i.name == "Internetowy").First();
                    sale.Employee_idEmployee = employeeId.idEmployee;

                    SaleState stateId = db.SaleStates.Where(i => i.state == "oczekuje na zatwierdzenie").First();
                    sale.SaleState_idSaleState = stateId.idSaleState;

                    db.Sales.Add(sale);
                    db.SaveChanges();

                    List<Cart> lsCart = new List<Cart>();
                    lsCart = (List<Cart>)Session[sessionCartString];

                    int lastSaleDetails = 0;

                    foreach (var item in lsCart)
                    {
                        SaleDetail detailOfSale = new SaleDetail();
                        Item itemInShop = new Item();

                        itemInShop = db.Items.Find(item.Item.idItem);
                        int availbility = (int)itemInShop.availability - item.Quantity;
                        try
                        {
                            itemInShop.availability = checked((byte)availbility);
                        }
                        catch (OverflowException e)
                        {
                            db.Sales.Remove(sale);
                            db.SaveChanges();

                            if (itemInShop.availability <= 0)
                                lsCart.Remove(item);
                            else
                                item.Quantity = itemInShop.availability;

                            return RedirectToAction("Index", "ShoppingCart", new { Error = "Podczas składania zamówienia wystąpił błąd. Twoje zamówienie zawierało więcej przedmiotów, niż wynosi stan magazynowy." });
                        }

                        db.Entry(itemInShop).Entity.availability = itemInShop.availability;
                        db.SaveChanges();

                        detailOfSale.Item_idItem = item.Item.idItem;
                        detailOfSale.Sale_idSale = db.Entry(sale).Entity.idSale;

                        lastSaleDetails = detailOfSale.Sale_idSale;

                        detailOfSale.quantity = (byte)item.Quantity;
                        detailOfSale.value = item.Item.price * item.Quantity;

                        db.SaleDetails.Add(detailOfSale);
                        db.SaveChanges();

                    }

                    Session[sessionCartString] = null;

                    return RedirectToAction("Final", new { idOfSale = lastSaleDetails });
                }

                return View(customer);
            }
        }

        // GET: ShoppingCart/Final
        public ActionResult Final(int? idOfSale)
        {
            if (idOfSale != null)
            {
                //IQueryable details = new IQueryable();

                IQueryable<SaleDetail> details = db.SaleDetails.Where(i => i.Sale_idSale == idOfSale);

                List<SaleDetail> saleDetail = details.ToList();

                decimal[] sum = new decimal[saleDetail.Count];
                int index = 0;

                foreach (var quantity in saleDetail)
                {
                    sum[index] = quantity.value / quantity.Item.price;
                    index++;
                }
                ViewBag.values = sum;

                return View(saleDetail);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: ShoppingCart/Final
        [HttpPost]
        public ActionResult Final()
        {
            return RedirectToAction("Index", "Shop");
        }

        public ActionResult OrderNow(int? id, int? quantity)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            if(Request["HiddenQuantity"] != null)
            {
                int quan = 0;
                if(Int32.TryParse(Request["HiddenQuantity"], out quan))
                {
                    quantity = (int?)quan;
                }
                else
                {
                    quantity = 1;
                }
            }

            if(Session[sessionCartString] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(db.Items.Find(id), (int)quantity)
                };

                Session[sessionCartString] = lsCart;
            }
            else
            {
                List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
                int indexOfItem = doseItemExistInCart(id);

                if (indexOfItem == -1)
                    lsCart.Add(new Cart(db.Items.Find(id), (int)quantity));
                else
                {
                    lsCart[indexOfItem].Quantity += (int)quantity;
                }

                Session[sessionCartString] = lsCart;
            }
            return RedirectToAction("Index");
        }

        private int doseItemExistInCart(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
            for (int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].Item.idItem == id)
                    return i;
            }
            return -1;
        }

        public ActionResult AddQuantity(int? id)
        {
            string errorQuantity = "";
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
            int indexOfItem = doseItemExistInCart(id);

            if (indexOfItem == -1)
                lsCart.Add(new Cart(db.Items.Find(id), 1));
            else
            {
                int itemAvailability = db.Items.Find(id).availability;

                if (itemAvailability >= (lsCart[indexOfItem].Quantity + 1))
                    lsCart[indexOfItem].Quantity++;
                else
                    errorQuantity = "Maksymalna liczba dostępnych sztuk została osiągnięta.";
            }

            Session[sessionCartString] = lsCart;

            return RedirectToAction("Index", "ShoppingCart", new { Error = errorQuantity });
        }
        public ActionResult DecrementQuantity(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }

            List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
            int indexOfItem = doseItemExistInCart(id);

            if (indexOfItem != -1)
            {
                lsCart[indexOfItem].Quantity--;
                if (lsCart[indexOfItem].Quantity == 0)
                    Delete(lsCart[indexOfItem].Item.idItem);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Shop");
            }
            
            int indexOfItem = doseItemExistInCart(id);
            List<Cart> lsCart = (List<Cart>)Session[sessionCartString];
            lsCart.RemoveAt(indexOfItem);

            if (lsCart.Count == 0)
                Session[sessionCartString] = null;

            return RedirectToAction("Index");
        }
    }
}
