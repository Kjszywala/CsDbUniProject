using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
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

        protected override void Delete()
        {
            if (SelectedItem != null)
            {
                Towar towar = FakturyEntities.Towar.FirstOrDefault(item => item.IdTowaru == SelectedItem.IdTowaru);
                if (towar != null)
                {
                    towar.CzyAktywny = false;
                    FakturyEntities.SaveChanges();
                    AllList.Remove(SelectedItem);
                    List.Remove(SelectedItem);
                }
            }
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            throw new NotImplementedException();
        }

        protected override int GetSelectedItemId()
        {
            throw new NotImplementedException();
        }

        protected override List<string> GetSortComboBoxItems()
        {
            throw new NotImplementedException();
        }

        protected override void Search()
        {
            throw new NotImplementedException();
        }

        protected override void Sort()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
