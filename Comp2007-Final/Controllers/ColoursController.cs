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
    public class ColoursController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Colours
        public ActionResult Index()
        {
            //Sort by Name
           // var colours = db.Colours.AsQueryable();
            //colours = colours.OrderBy(x => x.Name).AsQueryable();

            return View(db.Colours.ToList());
        }

        // GET: Colours/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colour colour = db.Colours.Find(id);
            if (colour == null)
            {
                return HttpNotFound();
            }
            return View(colour);
        }

        // GET: Colours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Colour colour) //, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                colour.ColourId = Guid.NewGuid().ToString();
                colour.CreateDate = DateTime.Now;
                colour.EditDate = colour.CreateDate;

                db.Colours.Add(colour);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(colour);
        }

        // GET: Colours/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colour colour = db.Colours.Find(id);
            if (colour == null)
            {
                return HttpNotFound();
            }
            return View(colour);
        }

        // POST: Colours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColourId,Name")] Colour colour, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                Colour tmpcolour = db.Colours.Find(colour.ColourId);
                if (tmpcolour != null)
                {
                    tmpcolour.Name = colour.Name;
                    tmpcolour.EditDate = DateTime.Now;

                    db.Entry(tmpcolour).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
                }
            }
            return View(colour);
        }

        // GET: Colours/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colour colour = db.Colours.Find(id);
            if (colour == null)
            {
                return HttpNotFound();
            }
            return View(colour);
        }

        // POST: Colours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Colour colour = db.Colours.Find(id);

            //Delete Items with Colours attached
            foreach(var data in colour.Items.ToList())
            {
                db.Orders.Remove(data);
            }

            db.Colours.Remove(colour);
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
