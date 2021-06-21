using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoLibrary.Data;
using TodoLibrary.Models;

namespace Todo.Controllers
{
    public class UpdatePageController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UpdatePageController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Update(int TodoId)
        {
            ItemModel itemObj = new ItemModel();
            ViewBag.Todoid = TodoId;          
            itemObj.Item = _db.Item.Find(TodoId);   
            return PartialView("Update",itemObj);
        }
    }
}
