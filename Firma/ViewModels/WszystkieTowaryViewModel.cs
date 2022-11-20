using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Firma.ViewModels
{
    public class WszystkieTowaryViewModel: WszystkieViewModel<Towar>
    {
        #region Konstruktor
        public WszystkieTowaryViewModel()
            :base("Towary")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            try
            {
                //To jest zapytanie LINQ (obiektowa wersja SQL)
                List = new ObservableCollection<Towar>(
                        //dla kazdego towaru z tabeli towar wybierz ten towar.
                        //SELECT * FROM Towar
                        //WHERE CzyAktywny = true
                        from towar in FakturyEntities.Towar
                        where towar.CzyAktywny == true
                        select towar
                    );
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
    }
}
