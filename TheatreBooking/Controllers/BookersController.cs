using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TheatreBooking.AppLayer;
using WebGrease.Css.Extensions;

namespace TheatreBooking.Controllers
{
    [Authorize]
    public class BookersController : Controller
    {
        private SeatContext db = new SeatContext();

        // GET: Bookers
        public ActionResult Index()
        {
            var bookers = db.Bookers.ToList();

            return View(bookers);
        }

        public ActionResult BookerAndSeats()
        {
            var bookers = GetBookersAndSeats();

            return View(bookers);
        }

        public ActionResult Unbook(int? bookerID)
        {
            if (bookerID != null)
            {
                //delete seats
                var seats = db.Seats.Where(s => s.BookedBy.ID == bookerID).ToList();

                if (seats.Count > 0)
                {
                    seats.ForEach(seat =>
                    {
                        if (seat.Status == SeatStatus.Booked)
                        {
                            seat.BookedAt = null;
                            seat.Status = SeatStatus.Available;
                            seat.SelectedAt = null;
                            seat.BookedBy = null;
                        }
                    });

                    db.SaveChanges();
                }

                //delete booker
                var booker = db.Bookers.FirstOrDefault(b => b.ID == bookerID);
                db.Bookers.Remove(booker);

                db.SaveChanges();
            }

            return RedirectToAction("BookerAndSeats");
        }

        public void ExportBookersToExcel()
        {
            var bookers = new System.Data.DataTable("bookers");
            bookers.Columns.Add("Фамилия", typeof(string));
            bookers.Columns.Add("Имя", typeof(string));
            bookers.Columns.Add("Email", typeof(string));
            bookers.Columns.Add("Телефон", typeof(string));
            bookers.Columns.Add("Забронированные места", typeof(string));
            bookers.Columns.Add("Приглашающее лицо", typeof(string));
            bookers.Columns.Add("Фуршет", typeof(string));

            var bookerList = GetBookersAndSeats();
            bookerList.ForEach(b =>
            {
                bookers.Rows.Add(b.LastName, b.FirstName, b.Email, b.PhoneNumber, String.Join(" ", b.Seats), b.Face, (b.Party ?? false) ? "Фуршет" : "");
            });

            var grid = new System.Web.UI.WebControls.GridView();

            grid.DataSource = bookers;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Bookers.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        private IList<BookerAndSeats> GetBookersAndSeats()
        {
            var bookers = db.Bookers.Select(b => new BookerAndSeats()
            {
                ID = b.ID,
                FirstName = b.FirstName,
                LastName = b.LastName,
                Email = b.Email,
                PhoneNumber = b.PhoneNumber,
                Face = b.Face,
                Party = b.Party
            }).ToList();

            foreach (var booker in bookers)
            {
                var seats = db.Seats.Where(s => s.BookedBy.ID == booker.ID).ToList();
                booker.Seats = new List<string>();

                seats.ForEach(s =>
                {
                    var seatDescription = s.AreaDescription + " " + s.RowName + " " + s.RowNumber + " Место " +
                                          s.SeatNumber;
                    booker.Seats.Add(seatDescription);
                });
            }

            return bookers;
        }

        //// GET: Bookers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Booker booker = db.Bookers.Find(id);
        //    if (booker == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(booker);
        //}

        //// GET: Bookers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Bookers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,LastName,FirstName,Email,PhoneNumber")] Booker booker)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Bookers.Add(booker);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(booker);
        //}

        //// GET: Bookers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Booker booker = db.Bookers.Find(id);
        //    if (booker == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(booker);
        //}

        //// POST: Bookers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,LastName,FirstName,Email,PhoneNumber")] Booker booker)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(booker).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(booker);
        //}

        //// GET: Bookers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Booker booker = db.Bookers.Find(id);
        //    if (booker == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(booker);
        //}

        //// POST: Bookers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Booker booker = db.Bookers.Find(id);
        //    db.Bookers.Remove(booker);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
