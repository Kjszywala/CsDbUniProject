using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.BusinessLogic
{
    public class TowarB : DatabaseClass
    {
        #region PolaIWlasciwosci

        public int TowarId { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }

        #endregion

        #region Konstruktory

        public TowarB(FakturyEntities db) : base(db)
        {
            DataOd = DateTime.Now;
            DataDo = DateTime.Now;
        }

        #endregion

        #region Metody

        public decimal ObliczUtarg()
        {
            return Db.PozycjaFaktury.Where(item => item.IdTowaru == TowarId
                                            && item.Faktura.DataWystawienia >= DataOd
                                            && item.Faktura.DataWystawienia <= DataDo).
                                            Sum(item => (item.Ilosc * item.Cena * (1 - (item.Rabat)))) ?? 0;
        }

        #endregion

    }
}
