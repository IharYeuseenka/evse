using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyStore.Models;
using WebMatrix.WebData;

namespace MyStore.Controllers
{
    public class HomeController : Controller
    {
        MyStoreContext _db = new MyStoreContext();

        public ActionResult Index(string returnUrl)
        {
            IEnumerable<Book> books = (from book in _db.Books
                                          orderby book.Date descending
                                          select book).Take(15);
            IEnumerable<Categorie> categories = _db.Categories;
            ViewBag.Books = books;
            ViewBag.Categories = categories;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpGet]
        public ActionResult Catalog(int? id, string returnUrl)
        {
            IEnumerable<Book> books;
            if (id != null)
            {
                books = from book in _db.Books
                                          where book.CategorieId == id
                                          select book;
                ViewBag.Title = _db.Categories.Find(id).Name;
            }
            else
            {
                books = _db.Books;
                ViewBag.Title = "Каталог";
            }
            IEnumerable<Categorie> categories = _db.Categories;
            ViewBag.Books = books;
            ViewBag.Categories = categories;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Productdetail(int id, string returnUrl)
        {
            var book = _db.Books.Find(id);
            if (book != null)
            {
                ViewBag.Book = book;
            }
            IEnumerable<Categorie> categories = _db.Categories;
            ViewBag.Categories = categories;
            ViewBag.BookId = id;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Contacts()
        {
            IEnumerable<Categorie> categories = _db.Categories;
            ViewBag.Categories = categories;
            return View();
        }

        
        public ActionResult About()
        {
            IEnumerable<Categorie> categories = _db.Categories;
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Catalog(string keyword, string returnUrl)
        {
            IEnumerable<Book> books = from book in _db.Books
                                      where book.Name.Contains(keyword)||book.Author.Contains(keyword)
                                      select book;
            ViewBag.Title = "Результаты поиска";
            IEnumerable<Categorie> categories = _db.Categories;
            ViewBag.Books = books;
            ViewBag.Count = books.Count<Book>();
            ViewBag.Categories = categories;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult ShoppingCart(Purchase purchase, string returnUrl)
        {
            purchase.Count = 1;
            purchase.UserId = WebSecurity.CurrentUserId;
                _db.Purchases.Add(purchase);
                _db.SaveChanges();
                ViewBag.ReturnUrl = returnUrl;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShoppingCart(int id)
        {
            IEnumerable<Purchase> purchases = from purchase in _db.Purchases
                                                  where purchase.UserId == id
                                                  select purchase;
            ViewBag.Purchases = purchases.ToList<Purchase>();
            IEnumerable<Categorie> categories = _db.Categories;
            ViewBag.Categories = categories;
            IEnumerable<Book> books = _db.Books;
            ViewBag.Books = books;
            ViewBag.Count = purchases.Count<Purchase>();
            return View();
        }

        public ActionResult Delete(int id)
        {
            Purchase purchase = _db.Purchases.Find(id);
            _db.Purchases.Remove(purchase);
            _db.SaveChanges();
            return RedirectToAction("ShoppingCart/"+WebSecurity.CurrentUserId);
        }
    }
}
