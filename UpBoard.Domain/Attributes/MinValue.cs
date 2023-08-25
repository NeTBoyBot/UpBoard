using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinValue : ValidationAttribute
    {
        private double minValue;
        public MinValue(double _minValue)
        {
            minValue = _minValue;
        }
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }
               
            if (!double.TryParse(value.ToString(), out double price))
            {
                return false;
            }
               
            return price > minValue;
        }
    }
}
