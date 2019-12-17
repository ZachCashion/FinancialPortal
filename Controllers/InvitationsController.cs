using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using FinancialPortal.Helpers;
using FinancialPortal.Extensions;
using System.Threading.Tasks;

namespace FinancialPortal.Controllers
{
    public class InvitationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private EmailHelper emailHelper = new EmailHelper();
        

        // GET: Invitations
        public ActionResult Index()
        {
            var invitations = db.Invitations.Include(i => i.Household);
            return View(invitations.ToList());
        }

        // GET: Invitations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitations invitations = db.Invitations.Find(id);
            if (invitations == null)
            {
                return HttpNotFound();
            }
            return View(invitations);
        }

        [Authorize(Roles = "HouseholdHead")]
        // GET: Invitations/Create
        public ActionResult Create()
        {
            var houseId = db.Users.Find(User.Identity.GetUserId()).HouseholdId ?? 0;
            if (houseId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var invitation = new Invitations 
            { 
                HouseholdId = houseId 
            };

            return View(invitation);
        }

        // POST: Invitations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HouseholdId,TTL,RecipientEmail")] Invitations invitations)
        {
            if (ModelState.IsValid)
            {
                invitations.Created = DateTime.Now;
                invitations.Code = Guid.NewGuid();
                invitations.IsValid = true;
                db.Invitations.Add(invitations);
                db.SaveChanges();

                await invitations.EmailInvitation();

                return RedirectToAction("Index");
            }

            
            return View(invitations);
        }

        // GET: Invitations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitations invitations = db.Invitations.Find(id);
            if (invitations == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitations.HouseholdId);
            return View(invitations);
        }

        // POST: Invitations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HouseholdId,IsValid,Created,TTL,RecipientEmail,Code")] Invitations invitations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invitations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", invitations.HouseholdId);
            return View(invitations);
        }

        // GET: Invitations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invitations invitations = db.Invitations.Find(id);
            if (invitations == null)
            {
                return HttpNotFound();
            }
            return View(invitations);
        }

        // POST: Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invitations invitations = db.Invitations.Find(id);
            db.Invitations.Remove(invitations);
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
