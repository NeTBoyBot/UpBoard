using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpBoard.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Password : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (!(value is string valueAsString))
            {
                return false;
            }

            return valueAsString.Any(c => c.ToString().ToUpper() == c.ToString())
                && valueAsString.Any(c => int.TryParse(c.ToString(), out int i))
                && valueAsString.Length >= 6;
        }
    }
}
