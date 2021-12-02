using E_CommerceSite.infrestracuter;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Controllers
{
    public class CardController : Controller
    {
        private readonly Ecommersitecontext db;

        public CardController(Ecommersitecontext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            List<carditem> cart = HttpContext.Session.Getjson<List<carditem>>("Card") ?? new List<carditem>();

            cardViewModel cardvm = new cardViewModel
            {
                carditem = cart,
                grandtotal = cart.Sum(x => x.price * x.quantity)

            };
            return View(cardvm);
        }

        public   IActionResult Add(int id)
        {

            product prod = db.products.Find(id);
            List<carditem> cart = HttpContext.Session.Getjson<List<carditem>>("Card") ?? new List<carditem>();

            carditem carditem = cart.Where(x => x.productid == id).FirstOrDefault();

            if (carditem == null)
            {
                cart.Add(new carditem(prod));
            }
            else
            {
                carditem.quantity += 1;
            }

            HttpContext.Session.setjson("Card", cart);

            return RedirectToAction("Index");
        }
    }
}
