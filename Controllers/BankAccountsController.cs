using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;

namespace FinancialPortal.Controllers
{
    public class BankAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BankAccounts
        public ActionResult Index()
        {
            var bankAccounts = db.BankAccounts.Include(b => b.Household).Include(b => b.Owner);
            return View(bankAccounts.ToList());
        }

        // GET: BankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccounts bankAccounts = db.BankAccounts.Find(id);
            if (bankAccounts == null)
            {
                return HttpNotFound();
            }
            return View(bankAccounts);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: BankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HouseholdId,OwnerId,Created,Name,StartingBalance,CurrentBalance")] BankAccounts bankAccounts)
        {
            if (ModelState.IsValid)
            {
                db.BankAccounts.Add(bankAccounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", bankAccounts.HouseholdId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", bankAccounts.OwnerId);
            return View(bankAccounts);
        }

        // GET: BankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccounts bankAccounts = db.BankAccounts.Find(id);
            if (bankAccounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", bankAccounts.HouseholdId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", bankAccounts.OwnerId);
            return View(bankAccounts);
        }

        // POST: BankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,OwnerId,Created,Name,StartingBalance,CurrentBalance")] BankAccounts bankAccounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bankAccounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", bankAccounts.HouseholdId);
            ViewBag.OwnerId = new SelectList(db.Users, "Id", "FirstName", bankAccounts.OwnerId);
            return View(bankAccounts);
        }

        // GET: BankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BankAccounts bankAccounts = db.BankAccounts.Find(id);
            if (bankAccounts == null)
            {
                return HttpNotFound();
            }
            return View(bankAccounts);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BankAccounts bankAccounts = db.BankAccounts.Find(id);
            db.BankAccounts.Remove(bankAccounts);
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
