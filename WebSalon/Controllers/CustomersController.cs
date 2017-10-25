using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSalon.Models;
using PagedList;

namespace WebSalon.Controllers
{
    [AllowAnonymous]
    public class CustomersController : Controller
    {
        private SalonEntities db = new SalonEntities();

        // GET: Customers
        public ActionResult Index(string sortOrder,string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            //Filter nachname
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var customers = from c in db.Customers
                            select c;
           

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.LName.Contains(searchString));
            }

            

            if (ViewBag.NameSortParm == "name_desc")
            {
               
                switch (sortOrder)
                {
                    case "Vorname":
                        customers = customers.OrderBy(s => s.FName);
                        break;
                    case "Nachname":
                        customers = customers.OrderBy(s => s.LName);
                        break;
                    default:
                        customers = customers.OrderBy(s => s.FName);
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case "Vorname":
                        customers = customers.OrderByDescending(s => s.FName);
                        break;
                    case "Nachname":
                        customers = customers.OrderByDescending(s => s.LName);
                        break;
                    default:
                        customers = customers.OrderByDescending(s => s.FName);
                        break;
                }
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(pageNumber,pageSize));
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
           
            }
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "CustomerID,FName,LName,BirthDay,Female,Notes")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult CustomerVisits(List<Customer> customerList = null)
        {
            if(customerList == null)
            {
                customerList = this.db.Customers.ToList();
            }
            else
            {

            }
            return View(customerList);
        }

        public void DetailVisit(int? id)
        {
            var customerList = this.db.Customers.ToList();

            if (this.db.Visits.Where(v => v.CustomerID == id).ToList() != null)
            {
                customerList.SelectMany(c => c.Visits = this.db.Visits.Where(v => v.CustomerID == id).ToList());
            }
            CustomerVisits(customerList);
        }

        // GET: Customers/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // Aktivieren Sie zum Schutz vor übermäßigem Senden von Angriffen die spezifischen Eigenschaften, mit denen eine Bindung erfolgen soll. Weitere Informationen 
        // finden Sie unter http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "CustomerID,FName,LName,BirthDay,Female,Notes")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
