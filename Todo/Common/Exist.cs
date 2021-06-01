using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Todo.Common
{
    public class Exist : ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            return true;
        }
    }
}
