using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreBooking.AppLayer;
using System.Web.SessionState;
using System.Web.UI;
using Microsoft.Ajax.Utilities;

namespace TheatreBooking.Controllers
{
    public class SeatsEnController : Controller
    {
        private SeatContext db = new SeatContext();
        private EmailSender emailSender = new EmailSender();
        private System.Web.SessionState.HttpSessionState session = System.Web.HttpContext.Current.Session;

        // GET: Seats
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Seats.ToList());
        }

        [Authorize(Roles = "admin")]
        public ActionResult Plan()
        {
            return View();
        }

        [Authorize(Roles = "spectator,admin")]
        public ActionResult PlanRestricted()
        {
            return View();
        }

        [Authorize(Roles = "admin,spectator")]
        public ActionResult GetSeats()
        {
            var seats = db.Seats.ToList();

            var expiredSeats = seats.Where(
                s => (s.Status == SeatStatus.Selected) && (s.SelectedAt != null) && (DateTime.Now.Subtract(TimeSpan.FromSeconds(300)) > s.SelectedAt))
                .ToList();

            expiredSeats.ForEach(s =>
            {
                s.SelectedAt = null;
                s.Status = SeatStatus.Available;
            });

            db.SaveChanges();

            seats = db.Seats.ToList();

            return Json(seats, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin,spectator")]
        public void ExportBookedSeatsToExcel()
        {
            var seats = new System.Data.DataTable("bookers");
            seats.Columns.Add("Area", typeof(string));
            seats.Columns.Add("Seat", typeof(string));
            seats.Columns.Add("Number", typeof(string));
            seats.Columns.Add("Seat", typeof(string));
            seats.Columns.Add("Price", typeof(string));
            seats.Columns.Add("Booked by", typeof(string));
            seats.Columns.Add("Booked at", typeof(DateTime));

            db.Seats.Where(s => s.Status == SeatStatus.Booked).ToList().ForEach(s =>
            {
                seats.Rows.Add(s.AreaDescriptionEn, s.RowNameEn, s.RowNumber, s.SeatNumber, s.Price,
                    s.BookedBy.LastName + " " + s.BookedBy.FirstName, s.BookedAt);
            });

            var grid = new System.Web.UI.WebControls.GridView();

            grid.DataSource = seats;
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Seats.xls");
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        // GET: Seats/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Seat seat = db.Seats.Find(id);
        //    if (seat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(seat);
        //}

        //// GET: Seats/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Seats/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,RowName,RowNumber,SeatNumber,AreaDescription,Price,Information,Status,BookedAt,SelectedAt")] Seat seat)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Seats.Add(seat);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(seat);
        //}

        //// GET: Seats/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Seat seat = db.Seats.Find(id);
        //    if (seat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(seat);
        //}

        //// POST: Seats/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,RowName,RowNumber,SeatNumber,AreaDescription,Price,Information,Status")] Seat seat)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (seat.Status == SeatStatus.Booked)
        //        {
        //            seat.BookedAt = DateTime.Now;
        //        }
        //        else
        //        {
        //            seat.BookedAt = null;
        //        }

        //        db.Entry(seat).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(seat);
        //}

        [Authorize(Roles = "admin")]
        public ActionResult Unbook(int? id)
        {
            var seat = db.Seats.FirstOrDefault(s => s.ID == id);

            if (seat != null)
            {
                var booker = db.Bookers.FirstOrDefault(b => b.ID == seat.BookedBy.ID);
                if (seat.Status == SeatStatus.Booked)
                {
                    seat.BookedAt = null;
                    seat.Status = SeatStatus.Available;
                    seat.SelectedAt = null;
                    seat.BookedBy = null;
                }

                db.SaveChanges();

                if (!db.Seats.Any(s => s.BookedBy.ID == booker.ID))
                {
                    db.Bookers.Remove(booker);
                }
            }

            return RedirectToAction("Booked");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Book(List<int> ids, string FirstName, string LastName, string Email, string PhoneNumber, string face, bool? participation)
        {
            var seats = db.Seats.Where(s => ids.Contains(s.ID) && s.Status == SeatStatus.Selected).ToList();

            if (seats.Count > 0)
            {
                Booker booker = new Booker()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Face = face,
                    Party = participation ?? false
                };

                booker = db.Bookers.Add(booker);

                seats.ForEach(seat =>
                {
                    seat.Status = SeatStatus.Booked;
                    seat.BookedAt = DateTime.Now;
                    seat.BookedBy = booker;
                });

                db.SaveChanges();

                if (!Email.IsNullOrWhiteSpace())
                {
                    var seatString = "";
                    seats.ForEach(s => seatString = seatString + s.AreaDescription + " " + s.RowName + " " + s.RowNumber + " Место " + s.SeatNumber + " Стоимость: " + s.Price + "\r\n\r\n"); 
                    emailSender.Send(Email, "Билеты в большой театр успешно забронированы", String.Format("Вы успешно забронировали билеты в большой театр:\r\n\r\n{0}", seatString));
                }
                
                return Json(seats, JsonRequestBehavior.AllowGet);
            } 

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Select(int id, SeatStatus selected)
        {
            var seat = db.Seats.First(s => s.ID == id);

            if (seat.Status == SeatStatus.Available && selected == SeatStatus.Available)
            {
                seat.Status = SeatStatus.Selected;
                seat.SelectedAt = DateTime.Now;
            }
            else if (seat.Status == SeatStatus.Selected && selected == SeatStatus.Selected)
            {
                seat.Status = SeatStatus.Available;
                seat.SelectedAt = null;
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            db.SaveChanges();

            return Json(seat, JsonRequestBehavior.AllowGet);
        }


        //// GET: Seats/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Seat seat = db.Seats.Find(id);
        //    if (seat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(seat);
        //}

        //// POST: Seats/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Seat seat = db.Seats.Find(id);
        //    db.Seats.Remove(seat);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [Authorize(Roles = "admin")]
        public ActionResult Booked()
        {
            var bookedList = db.Seats.Where(seat => seat.Status == SeatStatus.Booked).ToList();

            return View(bookedList);
        }

        [Authorize(Roles = "admin,spectator")]
        public ActionResult BookedRestricted()
        {
            var bookedList = db.Seats.Where(seat => seat.Status == SeatStatus.Booked).ToList();

            return View(bookedList);
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
