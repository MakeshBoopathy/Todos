using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Common
{
    public class DateRangeAttribute : RangeAttribute
    {       

        public DateRangeAttribute(string maxvalue) : base(typeof(DateTime),DateTime.Now.ToShortDateString(),maxvalue)
        {

        }
        /*public override bool IsValid(object value)
        {
            DateTime propValue = Convert.ToDateTime(value);

            return propValue > DateTime.Now;
           
        }*/
    }
}
