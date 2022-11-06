using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Firma.ViewModels
{
    // bo wszystkie widoki zgodne ze scenriuszem wszystkie dziedzicza po wszystkieviewmodel
    public class WszystkieFakturyViewModel: WszystkieViewModel<FakturaForAllView>
    {
        #region Konstruktor
        public WszystkieFakturyViewModel()
            :base("Faktury")
        {
        }
        #endregion
        
        #region Helpers
        public override void Load()
        {
            try
            {
                List = new ObservableCollection<FakturaForAllView>(
                    // dla kazdej faktury z bazy danych wybieramy nowa Faktureforallview
                    from faktura in FakturyEntities.Faktura
                    where faktura.CzyAktywna == true
                    select new FakturaForAllView
                    {
                        IdFaktury=faktura.IdFaktury,
                        Numer=faktura.Numer,
                        DataWystawienia=faktura.DataWystawienia,
                        KontrahentNazwa=faktura.Kontrahent.Nazwa,
                        KontrahentNIP=faktura.Kontrahent.NIP,
                        KontrahentAdres=
                            faktura.Kontrahent.Adres.KodPocztowy + ", " +
                            faktura.Kontrahent.Adres.Miejscowosc + ", " +
                            faktura.Kontrahent.Adres.Ulica + ", " +
                            faktura.Kontrahent.Adres.NrDomu,
                        TerminPlatnosci=faktura.TerminPlatnosci,
                        SposobPlatnosciNazwa=faktura.SposobPlatnosci.Nazwa
                    }
                );
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
    }
}
