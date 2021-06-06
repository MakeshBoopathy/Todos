using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Controllers
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
            if( _db.Item.Any(itemd => itemd.ItemName == item.ItemName))            
            {
                return Json("Already Created");
            }
            return Json(true);
        }

        public IActionResult Index()
        {

            ItemModel itemObj = new ItemModel();
            itemObj.Items = _db.Item;
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

        public String Filter(string filterType)
        {
            return filterType;
        }

  

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
