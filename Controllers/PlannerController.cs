using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wedding_planner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace wedding_planner.Controllers
{
    public class PlannerController : Controller
    {
        private WeddingContext _context;
 
        public PlannerController(WeddingContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("current_userid") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.user = HttpContext.Session.GetInt32("current_userid");
            List<Wedding> wedding_query = _context.weddings.Include(w=>w.guests).ThenInclude(u=>u.user).ToList();
            wedding_query.OrderByDescending(w=>w.date);
            ViewBag.all_weddings = wedding_query;
            return View("Dashboard");
        }

        [HttpGet]
        [Route("add-wedding")]
        public IActionResult Add()
        {
            if(HttpContext.Session.GetInt32("current_userid") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("NewWedding");
        }

        [HttpPost]
        [Route("create-wedding")]
        public IActionResult Create(WedFormViewModel model)
        {
            if(HttpContext.Session.GetInt32("current_userid") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(ModelState.IsValid)
            {
                if(model.date <= DateTime.Now)
                {
                    TempData["date_error"] = "Date must be in the future";
                    return View("NewWedding");
                }
                var userId = HttpContext.Session.GetInt32("current_userid");
                Wedding newWedding = new Wedding{
                    wedder_one = model.wedder_one,
                    wedder_two = model.wedder_two,
                    date = model.date,
                    address = model.address,
                    userid = (int)userId
                };
                _context.weddings.Add(newWedding);
                _context.SaveChanges();
                return RedirectToAction("Show", new{id = newWedding.weddingid});
            }
            return View("NewWedding");
        }

        [HttpGet]
        [Route("show-wedding/{id}")]
        public IActionResult Show(int id)
        {
            if(HttpContext.Session.GetInt32("current_userid") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var info = _context.weddings.Include(u=>u.guests).ThenInclude(u=>u.user).SingleOrDefault(a=>a.weddingid == id);
            ViewBag.all_info = info;
            return View("Wedding");
        }


        [HttpPost]
        [Route("action")]
        public IActionResult Action(string action, int WeddingId)
        {
            if(HttpContext.Session.GetInt32("current_userid") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(action == "Delete")
            {
                // var del = _context.weddings.SingleOrDefault(c => c.weddingid == WeddingId);
                _context.weddings.Remove(_context.weddings.SingleOrDefault(c => c.weddingid == WeddingId));
            }
            else if(action == "RSVP")
            {
                Guest newGuest = new Guest{
                userid = (int)HttpContext.Session.GetInt32("current_userid"),
                weddingid = WeddingId
                };
                _context.guests.Add(newGuest);
            }
            else if(action == "Un-RSVP")
            {
                _context.guests.Remove( _context.guests.Where(e => e.userid == (int)HttpContext.Session.GetInt32("current_userid")).Where(c => c.weddingid == WeddingId).SingleOrDefault());
            }
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
        // [HttpGet]
        // [Route("delete/{id}")]
        // public IActionResult Delete(int id)
        // {
        //     var del = _context.weddings.SingleOrDefault(c => c.weddingid == id);
        //     _context.weddings.Remove(del);
        //     _context.SaveChanges();
        //     return RedirectToAction("Dashboard");
        // }

        // [HttpGet]
        // [Route("rsvp/{id}")]
        // public IActionResult Rsvp(int id)
        // {
        //     Guest newGuest = new Guest{
        //         userid = (int)HttpContext.Session.GetInt32("current_userid"),
        //         weddingid = id
        //     };
        //     _context.guests.Add(newGuest);
        //     _context.SaveChanges();
        //     return RedirectToAction("Dashboard");
        // }

        // [HttpGet]
        // [Route("un-rsvp/{id}")]
        // public IActionResult Unrsvp(int id)
        // {
        //     // _context.guests.SingleOrDefault(e => e.userid == HttpContext.Session.GetInt32("current_userid"));
        //     _context.guests.Remove( _context.guests.Where(e => e.userid == (int)HttpContext.Session.GetInt32("current_userid")).Where(c => c.weddingid == id).SingleOrDefault());
        //     _context.SaveChanges();
        //     return RedirectToAction("Dashboard");
        // }




// <tr>
//     <th><a href="/show-wedding/@wedding.weddingid">@wedding.wedder_one &  @wedding.wedder_two</a></th>
//     <th>@wedding.date.ToString("MMM dd, yyyy")</th>
//     <th>@wedding.guests.Count</th>
//     <th>
//         @if(@wedding.userid == @ViewBag.user)
//         {
//             <a href="/delete/@wedding.weddingid">Delete</a>
//         }

//         else
//         {
//             @if(@wedding.guests.Exists(u => u.userid == @ViewBag.user))
//             {
//                 <a href="/un-rsvp/@wedding.weddingid">Un - RSVP</a>
//             }
//             else
//             {
//                  <a href="/rsvp/@wedding.weddingid">RSVP</a>
//             }
//         }
//     </th>
// </tr>

