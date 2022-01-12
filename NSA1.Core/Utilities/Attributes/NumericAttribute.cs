using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Utilities.Attributes
{
    public class NumericAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && IsNumeric(value.ToString().Trim()) == true;
        }

        private bool IsNumeric(string value)
        {
            bool rtn = false;
            foreach (char a in value)
            {
                if (Char.IsDigit(a) == true)
                {
                    rtn = true;
                }
                else
                {
                    rtn = false;
                    break;
                }
            }


            return rtn;
        }
    }
}
