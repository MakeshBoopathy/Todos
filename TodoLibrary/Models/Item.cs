using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace TodoLibrary.Models
{
    public class ItemModel
    {
        public Item Item { get; set; }
        public IEnumerable<Item> Items { get; set; }

    }

    public class Item
    {
        [Key]
        public int Id { get; set; }

        public string Borrower { get; set; }

        public string Lender { get; set; }

        [DisplayName("Status")]
        public bool completed { get; set; }

        [DisplayName("Item Name")]
        [Required(ErrorMessage = "Enter Item")]
        [Remote(action: "ItemExist", controller: "Home", AdditionalFields = "Id")]
        public string ItemName { get; set; }

        [DisplayName("Completion Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Remote(action: "DateRange", controller: "Home")]
        public DateTime date { get; set; }

        public String RemainingDays( )
        {
            Int32 resDate;
            resDate = (date - DateTime.Now).Days + 1;

            if (resDate == 0 || resDate == 1)
            {
                return resDate + " Day";
            }
            else if (resDate > 1)
            {
                return resDate + " Days";
            }
            else
            {
                return "expired";
            }
        }

    }


    //  
}
