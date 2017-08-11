using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Comp2007_Final.Models;

namespace Comp2007_Final.Controllers
{
    public class OrdersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Colour).Include(o => o.ItemFinish).Include(o => o.Item);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name");
            ViewBag.FinishId = new SelectList(db.ItemFinishes, "FinishId", "Name");
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,ItemId,ColourId,FinishId,CreateDate,EditDate")] Order model)
        {
            if (ModelState.IsValid)
            {
                Order checkmodel = db.Orders.FirstOrDefault(x => x.ItemId == model.ItemId && x.ColourId == model.ColourId && x.FinishId == model.FinishId);

                if(checkmodel != null)
                {
                    ModelState.AddModelError("", "Duplicate entry found");
                }
                else
                {
                    db.Orders.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
              
            }

            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name", model.ColourId);
            ViewBag.FinishId = new SelectList(db.ItemFinishes, "FinishId", "Name", model.FinishId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", model.ItemId);
            return View(model);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name", order.ColourId);
            ViewBag.FinishId = new SelectList(db.ItemFinishes, "FinishId", "Name", order.FinishId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", order.ItemId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,ItemId,ColourId,FinishId,CreateDate,EditDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name", order.ColourId);
            ViewBag.FinishId = new SelectList(db.ItemFinishes, "FinishId", "Name", order.FinishId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", order.ItemId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
