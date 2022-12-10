namespace Firma.Models.EntitiesForView
{
    public class PozycjaFakturyForAllView
    {
        
        #region Properties

        public string TowarKod { get; set; }
        public string towarNazwa { get; set; }
        public decimal? Cena { get; set; }
        public decimal? Ilosc { get; set; }
        public decimal? Rabat { get; set; }

        #endregion

        #region Constructor
        public PozycjaFakturyForAllView(string towarKod, string towarNazwa, decimal? cena, decimal? ilosc, decimal? rabat)
        {
            TowarKod = towarKod;
            this.towarNazwa = towarNazwa;
            Cena = cena;
            Ilosc = ilosc;
            Rabat = rabat;
        }

        #endregion

    }
}
