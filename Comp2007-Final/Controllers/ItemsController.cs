using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Comp2007_Final.Models
{
    public class ItemsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Items
        public ActionResult Index()
        {
            //Sort Items
            var items = db.Items.AsQueryable();

            return View(db.Items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            Item model = new Item();
            ViewBag.Colours = new MultiSelectList(db.Colours.ToList(), "ColourId", "Name", model.Colours.Select(x => x.ColourId).ToArray());
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,IsGift,ColourIds")] Item model, string[] ColourIds)
        {
            if (ModelState.IsValid)
            {


                Item checkmodel = db.Items.SingleOrDefault(x => x.Name.ToLower() == model.Name.ToLower() && x.IsGift == model.IsGift);
                if (checkmodel == null)
                {
                    db.Items.Add(model);
                    db.SaveChanges();

                    //foreach (string colourid in ColourIds)
                    //{
                    //    Stock stock = new Stock();

                    //    stock.ItemId = model.ItemId;
                    //    stock.ColourId = model.colourid;

                    //    model.Colours.Add(stock);
                    //}

                    //db.Entry(model).State = EntityState.Modified;
                    //db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Duplicate Item Entry");
            }
            ViewBag.Colours = new MultiSelectList(db.Colours.ToList(), "ColourId", "Name", ColourIds);

            return View(model);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Name,IsGift,CreateDate,EditDate")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
