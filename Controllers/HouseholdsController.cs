using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FinancialPortal.Helpers;
using FinancialPortal.Models;
using Microsoft.AspNet.Identity;
using FinancialPortal.Extensions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinancialPortal.Controllers
{
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private NotificationHelper notificationHelper = new NotificationHelper();

        // GET: Households
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
           
            if (user.HouseholdId != null)
            {
                return RedirectToAction("Details", new { id = user.Household.Id });
            }
            else
            {
                return RedirectToAction("Create");
            }

        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            Households households = db.Households.Find(id);
            
            return View(households);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Greating,Created")] Households households)
        {
            if (ModelState.IsValid)
            {
                households.Created = DateTime.Now;
                db.Households.Add(households);

                //Add User to Household
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                user.HouseholdId = households.Id;

                //Add User Role
                roleHelper.AddUserToRole(userId, "HouseholdHead");

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(households);
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Households households = db.Households.Find(id);
            if (households == null)
            {
                return HttpNotFound();
            }
            return View(households);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Greating,Created")] Households households)
        {
            if (ModelState.IsValid)
            {
                db.Entry(households).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(households);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Households households = db.Households.Find(id);
            if (households == null)
            {
                return HttpNotFound();
            }
            return View(households);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Households households = db.Households.Find(id);
            db.Households.Remove(households);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> LeaveAsync()
        {
            var userId = User.Identity.GetUserId();

            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            var user = db.Users.Find(userId);

            switch (myRole)
            {
                case "HouseholdHead":

                    var inhabitants = db.Users.Where(u => u.HouseholdId == user.HouseholdId).Count();
                    if(inhabitants > 1)
                    {
                        TempData["Message"] = $"You are unable to leave the Household at this time as there are still <b>{inhabitants}</b> Members remaining.";
                        return RedirectToAction("ExitDenied");
                    }

                    user.HouseholdId = null;
                    db.SaveChanges();

                    roleHelper.RemoveUserFromRole(userId, "HouseholdHead");
                    await ControllerContext.HttpContext.RefreshAuthentication(user);

                    return RedirectToAction("Index", "Home");

                case "Member":
                default:
                    user.HouseholdId = null;
                    db.SaveChanges();

                    roleHelper.RemoveUserFromRole(userId, "Member");
                    await ControllerContext.HttpContext.RefreshAuthentication(user);

                    return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ExitDenied()
        {
            return View();
        }

        public ActionResult AppointSuccessor()
        {
            var userId = User.Identity.GetUserId();
            var myHouseholdId = db.Users.Find(userId).HouseholdId ?? 0;

            if (myHouseholdId == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var member = db.Users.Where(u => u.HouseholdId == myHouseholdId && u.Id != userId);
            ViewBag.NewHoh = new SelectList(member, "Id", "DisplayName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AppointSuccessorAsync(string newHoh)
        {
            if (string.IsNullOrEmpty(newHoh))
            {
                return RedirectToAction("AppointSuccessorAsync", "Households");
            }

            var me = db.Users.Find(User.Identity.GetUserId());
            me.HouseholdId = null;
            db.SaveChanges();

            roleHelper.RemoveUserFromRole(me.Id, "HouseholdHead");
            await ControllerContext.HttpContext.RefreshAuthentication(me);

            roleHelper.RemoveUserFromRole(newHoh, "Member");
            roleHelper.AddUserToRole(newHoh, "HouseholdHead");

            //notificationHelper.SendNewRoleNotification(newHoh, "HouseholdHead");

            return RedirectToAction("Index", "Home");
            
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
