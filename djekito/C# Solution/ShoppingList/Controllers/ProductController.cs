﻿using System.Linq;
using System.Web.Mvc;
using ShoppingList.Models;

namespace ShoppingList.Controllers
{
    [ValidateInput(false)]
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            using (var db = new ShoppingListDbContext())
            {
                var products = db.Products.ToList();
                return View(products);
            }
        }

        [HttpGet]
        [Route("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ShoppingListDbContext())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return Redirect("/");
                }
            }
            return View();
        }

        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int? id)
        {
            using (var db = new ShoppingListDbContext())
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    return View(product);
                }
            }
            return Redirect("/");
        }

        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirm(int? id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            using (var db = new ShoppingListDbContext())
            {
                var productFromDb = db.Products.Find(product.Id);
                if (productFromDb != null)
                {
                    productFromDb.Name = product.Name;
                    productFromDb.Priority = product.Priority;
                    productFromDb.Status = product.Status;
                    productFromDb.Quantity = product.Quantity;
                    db.SaveChanges();
                }
            }
            return Redirect("/");
        }
    }
}