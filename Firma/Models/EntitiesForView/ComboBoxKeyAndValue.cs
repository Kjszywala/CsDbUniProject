using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    
    public class ComboBoxKeyAndValue
    {
        /// <summary>
         /// Klucz obcy np. FakturaId
         /// </summary>
        public int Key { get; set; }
        /// <summary>
        /// Wyswietlana wartosc np Nazwa
        /// </summary>
        public string Value { get; set; }
    }
}
