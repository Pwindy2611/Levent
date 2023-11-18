using Levent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Levent.Controllers
{
    public class ShoppingCartController : Controller
    {
        LeventEntities2 db = new LeventEntities2();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("EmptyCart","ShoppingCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id)
        {
            var _pro = db.Product.SingleOrDefault(s => s.ID_Pro == id);
            if (_pro != null)
            {
                GetCart().Add_Product_Cart(_pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.Update_quantity(id_pro, _quantity);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_cartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public PartialViewResult BagCart()
        {
            int toltal_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if(cart!=null)
                toltal_quantity_item = cart.Total_quantity();
                ViewBag.QuantityCart = toltal_quantity_item;
                return PartialView("BagCart");
        }
        public ActionResult EmptyCart()
        {
            return View();
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                OrderPro _order = new OrderPro();
                Random r = new Random();
                _order.ID_OrderPro = r.Next(1, 10000);
                _order.Time = DateTime.Now;
                _order.ID_OrderPro = r.Next(1, 10000);
                _order.ID_User = db.User1.Select(s => s.ID_User).FirstOrDefault();
                db.OrderPro.Add(_order);
                foreach(var item in cart.Items)
                {
                    OrderDetail _order_detail = new OrderDetail();
                    _order_detail.ID_OderDetail =r.Next(1,10000);
                    _order_detail.ID_Product = item._product.ID_Pro;
                    _order_detail.ID_Order = _order.ID_OrderPro;
                    _order_detail.UnitPrice = (double)item._product.Price_pro;
                    _order_detail.Quantity = item._quantity;
                    _order_detail.ID_User = _order.ID_User;
                    db.OrderDetail.Add(_order_detail);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Error");
            }
        }
        public ActionResult CheckOut_Success()
        {
            return View();
        }
    }
}