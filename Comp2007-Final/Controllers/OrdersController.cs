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
            var orders = db.Orders.Include(o => o.Item).Include(o => o.Colour).Include(o => o.ItemFinish);
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
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name");
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name");
            ViewBag.FinishId = new SelectList(db.ItemFinishes, "FinishId", "Name");
            
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ItemId,ColourId,FinishId")] Order model)
        {
            if (ModelState.IsValid)
            {
                Order tmporder = db.Orders
                    .SingleOrDefault(x => x.ItemId == model.ItemId &&
                    x.ColourId == model.ColourId && 
                    x.FinishId == model.FinishId);

                if(tmporder == null)
                {
                    model.OrderId = Guid.NewGuid().ToString();
                    model.CreateDate = DateTime.Now;
                    model.EditDate = model.CreateDate;

                    db.Orders.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicate key found!");
                }
              
            }
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", model.ItemId);
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name", model.ColourId);
            ViewBag.FinishId = new SelectList(db.ItemFinishes, "FinishId", "Name", model.FinishId);
            return View(model);
        }

        // GET: Orders/Edit/5
        [Authorize]
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
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", order.ItemId);
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name", order.ColourId);
            ViewBag.FinishId = new SelectList(db.ItemFinishes, "FinishId", "Name", order.FinishId);
            
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "OrderId,CreateDate,EditDate,ItemId,ColourId,FinishId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", order.ItemId);
            ViewBag.ColourId = new SelectList(db.Colours, "ColourId", "Name", order.ColourId);
            ViewBag.FinishId = new SelectList(db.ItemFinishes, "FinishId", "Name", order.FinishId);
            
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize]
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
