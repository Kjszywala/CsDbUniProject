using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.Validators
{
    public class DecimalValidator : Validator
    {
        #region Methods
        public static string CzyNieUjemne(decimal value) => value < 0 ? "Ta wartosc nie moze byc ujemna" : string.Empty;
        public static string CzyPoprawnyProcent(decimal value) => value >=0 && value <= 100 ?  string.Empty : "Procent musi byc z zakresu od 0-100";
        #endregion
    }
}
