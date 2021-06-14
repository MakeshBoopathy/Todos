
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TodoLibrary.Models;
using TodoLibrary.Data;
using Microsoft.AspNetCore.Mvc;

namespace TodoLibrary.Controllers
{
    public class HomeController : Controller
    {

     /// <summary>
     /// ///////////////////////todo app//////////////////////
     /// </summary>
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [AcceptVerbs("GET","POST")]
        public IActionResult DateRange(Item item)
        {           
            if(item.date < DateTime.Now)
            {
                return Json("Date must be greater than today");
            }
            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult ItemExist(Item item)
        {            
            if(_db.Item.Any(itemd => itemd.ItemName == item.ItemName && itemd.Id != item.Id))            
            {
                return Json("Already Created");
            }
            return Json(true);
        }

        /*public IActionResult Index()
        {

            ItemModel itemObj = new ItemModel();
            itemObj.Items = _db.Item;
            return View(itemObj);
        }*/        

        public IActionResult Index(int page = 0,string filter="All")
        {
            int page_size = 5;

            ItemModel itemObj = new ItemModel();  

            itemObj.Items = _db.Item;

            int TotalTasks = _db.Item.Count();

            ViewBag.TotalPages = TotalTasks / page_size;


            if (filter == "Completed")
                {               
                itemObj.Items = _db.Item.Where(I => I.completed == true).OrderBy(I => I.Id).Skip(page * page_size).Take(page_size);
                    ViewBag.Selected = "Completed";
                }

                if (filter == "OnGoing")
                {
                    itemObj.Items = _db.Item.Where(I => I.date >= DateTime.Now && I.completed == false).OrderBy(I => I.Id).Skip(page * page_size).Take(page_size);
                    ViewBag.Selected = "OnGoing";
                }

                if (filter == "Expired")
                {
                    itemObj.Items = _db.Item.Where(I => I.date < DateTime.Now && I.completed == false).OrderBy(I => I.Id).Skip(page * page_size).Take(page_size);
                    ViewBag.Selected = "Expired";
                }

                if (filter == "All")
                {
                    itemObj.Items = _db.Item.OrderBy(I => I.Id).Skip(page * page_size).Take(page_size);
                    ViewBag.Selected = "All";
                }           
           
            return View(itemObj);
            }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ItemModel obj)
        {            
            _db.Item.Add(obj.Item);
            _db.SaveChanges();       
            return RedirectToAction("Index");
        }

    
        public IActionResult Remove(int id)
        {
            var obj = _db.Item.Find(id);
            if (obj == null)
            {
                return Ok(obj);
            }

            _db.Item.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Check(int id)
        {
            var obj = _db.Item.Find(id);
            var val = obj.completed;
            if (obj == null)
            {
                return Ok(obj);
            }

            if(val == true)
            {
                val = false;
            }
            else
            {
                val = true;
            }
            obj.completed = val;
                        
            _db.Item.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
           

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(ItemModel obj)
        {

        
            if (ModelState.IsValid)
            {
                _db.Item.Update(obj.Item);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Filter(string filter)
        {
            return View("Index");
        }

  

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
