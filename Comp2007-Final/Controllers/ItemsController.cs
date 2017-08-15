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
    public class ItemsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Items
        public ActionResult Index()
        {
            //Sort Items
            var items = db.Items.AsQueryable();
            items = items.OrderBy(x => x.Name).AsQueryable();

            return View(items.ToList());
        }

        public ActionResult Gallery()
        {
            //Sort Items
            var items = db.Items.AsQueryable();
            items = items.OrderBy(x => x.Name);

            return View(items.ToList());
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
        [Authorize]
        public ActionResult Create()
        {
            Item model = new Item();
            //All colours
            ViewBag.Colours = new MultiSelectList(db.Colours.ToList(), "ColourId", "Name", model.Colours.Select(x => x.ColourId).ToArray());
            ViewBag.Finish = db.ItemFinishes.ToList();

            return View(model);
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Name,ColourIds")] Item model, string[] ColourIds, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                
                    Item checkmodel = db.Items.SingleOrDefault(x => x.Name.ToLower() == model.Name.ToLower());
                if (checkmodel == null)
                {
                    if (ColourIds != null)
                    {
                        db.Items.Add(model);
                        db.SaveChanges();

                        foreach (string colourid in ColourIds)
                        {
                            Order order = new Order();
                            order.ItemId = model.ItemId;
                            order.ColourId = colourid;
                            order.FinishId = fc["FinishId"];

                            model.Colours.Add(order);
                        }

                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Colours = new MultiSelectList(db.Colours.ToList(), "ColourId", "Name", model.Colours.Select(x => x.ColourId).ToArray());
                        ViewBag.Finish = db.ItemFinishes.ToList();
                        ModelState.AddModelError("", "Please pick at least one colour");
                    }
                }
                else
                {
                    ViewBag.Colours = new MultiSelectList(db.Colours.ToList(), "ColourId", "Name", model.Colours.Select(x => x.ColourId).ToArray());
                    ViewBag.Finish = db.ItemFinishes.ToList();
                    ModelState.AddModelError("", "Duplicate item entry");

                }
                
            }

            return View(model);
        }

        // GET: Items/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item model = db.Items.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.Colours = new MultiSelectList(db.Colours.ToList(), "ColourId", "Name", model.Colours.Select(x => x.ColourId).ToArray());
            ViewBag.Finish = db.ItemFinishes.ToList();

            return View(model);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ItemId,Name,ColourIds")] Item model, string[] ColourIds, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                Item tmpModel = db.Items.Find(model.ItemId);
                if (tmpModel != null)
                {
                    Item checkModel = db.Items.SingleOrDefault(
                                        x => x.Name == model.Name &&
                                        x.ItemId != model.ItemId);


                    if (checkModel == null)
                    {
                        if (ColourIds != null)
                        {
                            tmpModel.Name = model.Name;
                            tmpModel.EditDate = DateTime.UtcNow;


                            db.Entry(tmpModel).State = EntityState.Modified;

                            //To remove colours
                            // var removeItems = tmpModel.Colours.Where(x => !ColourIds.Contains(x.ColourId)).ToList();
                            var removeItems = tmpModel.Colours.ToList();

                            foreach (var removeItem in removeItems)
                            {
                                db.Entry(removeItem).State = EntityState.Deleted;
                            }

                            if (ColourIds != null && fc["FinishId"] != null)
                            {
                                var addItems = ColourIds.Where(x => !tmpModel.Colours.Select(y => y.ColourId).Contains(x));
                                //Items to add
                                foreach (string addItem in addItems)
                                {
                                    Order order = new Order();
                                    order.EditDate = DateTime.UtcNow;
                                    order.ItemId = tmpModel.ItemId;
                                    order.FinishId = fc["FinishId"];
                                    order.ColourId = addItem;

                                    db.Orders.Add(order);
                                }
                            }

                            db.SaveChanges();
                            return RedirectToAction("Index");
                        
                    }
                    else
                    {
                        ViewBag.Colours = new MultiSelectList(db.Colours.ToList(), "ColourId", "Name", model.Colours.Select(x => x.ColourId).ToArray());
                        ViewBag.Finish = db.ItemFinishes.ToList();
                        ModelState.AddModelError("", "Please pick at least one colour");
                    }
                }
                else
                    {
                        ViewBag.Colours = new MultiSelectList(db.Colours.ToList(), "ColourId", "Name", model.Colours.Select(x => x.ColourId).ToArray());
                        ViewBag.Finish = db.ItemFinishes.ToList();
                        ModelState.AddModelError("", "Duplicated game detected. ");
                }
                }
            }
            ViewBag.Genres = new MultiSelectList(db.Colours.ToList(), "GenreId", "Name", ColourIds);
            return View(model);
        }

        // GET: Items/Delete/5
        [Authorize]
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

            //delete game with genres attached
            foreach (var orderdata in item.Colours.ToList())
            {
                db.Orders.Remove(orderdata);
            }

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
