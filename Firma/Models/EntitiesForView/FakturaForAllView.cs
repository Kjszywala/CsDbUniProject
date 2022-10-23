using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    //Ta klasa jest po to zeby w sposob czytelny dla zwyklego uzytkownika
    //wyswietlac faktury na liscie faktur.
    public class FakturaForAllView
    {
        #region Properties
        public int IdFaktury { get; set; }
        public string Numer { get; set; }
        public DateTime DataWystawienia { get; set; }
        public string KontrahentNazwa { get; set; }
        #endregion
    }
}
