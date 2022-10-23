using Firma.Models.Entities;
using Firma.ViewModels.Abstract;

namespace Firma.ViewModels
{
    public class NowyTowarViewModel: JedenViewModel<Towar>
    {
        #region Konstruktor
        public NowyTowarViewModel()
            :base("Towar")
        {
            Item = new Towar();
        }
        #endregion

        #region Properties
        //dla kazdego pola edytowalnego na interfejsie tworzymy propertiesa.
        // te propertiesy bedziemy podlaczac do textboxow na interface.
        public string Kod
        {
            get
            {
                return Item.Kod;
            }
            set
            {
                if(value != Item.Kod)
                {
                    Item.Kod = value;
                    OnPropertyChanged(()=> Kod);
                }
            }
        }
        public string Nazwa
        {
            get
            {
                return Item.Nazwa;
            }
            set
            {
                if (value != Item.Nazwa)
                {
                    Item.Nazwa = value;
                    OnPropertyChanged(() => Nazwa);
                }
            }
        }
        public decimal? StawkaVatSprzedazy
        {
            get
            {
                return Item.StawkaVatSprzedazy;
            }
            set
            {
                if (value != Item.StawkaVatSprzedazy)
                {
                    Item.StawkaVatSprzedazy = value;
                    OnPropertyChanged(() => StawkaVatSprzedazy);
                }
            }
        }
        public decimal? StawkaVatZakupu
        {
            get
            {
                return Item.StawkaVatZakupu;
            }
            set
            {
                if (value != Item.StawkaVatZakupu)
                {
                    Item.StawkaVatZakupu = value;
                    OnPropertyChanged(() => StawkaVatZakupu);
                }
            }
        }
        public decimal? Cena
        {
            get
            {
                return Item.Cena;
            }
            set
            {
                if (value != Item.Cena)
                {
                    Item.Cena = value;
                    OnPropertyChanged(() => Cena);
                }
            }
        }
        public decimal? Marza
        {
            get
            {
                return Item.Marza;
            }
            set
            {
                if (value != Item.Marza)
                {
                    Item.Marza = value;
                    OnPropertyChanged(() => Marza);
                }
            }
        }
        public bool? CzyAktywny
        {
            get
            {
                return Item.CzyAktywny;
            }
            set
            {
                if (value != Item.CzyAktywny)
                {
                    Item.CzyAktywny = value;
                    OnPropertyChanged(() => CzyAktywny);
                }
            }
        }
        #endregion

        #region Save
        public override void Save()
        {
            Item.CzyAktywny = true;
            //najpierw dodajemy towar do lokalnej kolekcji.
            Db.Towar.AddObject(Item);
            //nastepnie zapisujemy zmiany w bazie danych.
            Db.SaveChanges();
        }
        #endregion
    }
}
