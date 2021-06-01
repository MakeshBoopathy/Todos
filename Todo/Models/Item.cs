using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Todo.Common;
using System.Globalization;

namespace Todo.Models
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
        [Required(ErrorMessage ="Enter Item")]
        [Exist(ErrorMessage = "Item Exist")]
        public string ItemName { get; set; }

        [DisplayName("Completion Date")]
        [DataType(DataType.Date)]       
        [DateRange("01/01/2030")]        
        public DateTime date { get; set; }

    }

   
  //  
}
