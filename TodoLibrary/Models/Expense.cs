using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoLibrary.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Expense")]
        [Required]
        public string ExpenseName { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Amount must be greater than zero!")]
        public int Amount { get; set; }
    }
}
