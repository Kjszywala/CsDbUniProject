using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Firma.ViewModels
{
    public class WszyscyKontrahenciViewModel : WszystkieViewModel<Kontrahent>
    {
        private Kontrahent _WybranyKontrahent;
        public Kontrahent WybranyKontrahent
        {
            get
            {
                return _WybranyKontrahent;
            }
            set
            {
                if(_WybranyKontrahent != value)
                {
                    _WybranyKontrahent = value;
                    OnPropertyChanged(() => WybranyKontrahent);
                    if (_WybranyKontrahent != null)
                    {
                        MessageBox.Show($"Wybrano klienta {WybranyKontrahent.Nazwa} - {WybranyKontrahent.NIP} ({WybranyKontrahent.Kod})","Status");
                        Messenger.Default.Send(_WybranyKontrahent);
                        OnRequestClose();
                    }
                }
            }
        }

        #region Konstruktor
        public WszyscyKontrahenciViewModel()
            : base("Kontrahenci")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            try
            {
                List = new ObservableCollection<Kontrahent>((
                    from kontrahent in FakturyEntities.Kontrahent
                    where kontrahent.CzyAktywny == true
                    select kontrahent).ToList());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        protected override void Sort()
        {
            throw new NotImplementedException();
        }

        protected override void Search()
        {
            throw new NotImplementedException();
        }

        protected override List<string> GetSortComboBoxItems()
        {
            throw new NotImplementedException();
        }

        protected override List<string> GetSearchComboBoxItems()
        {
            throw new NotImplementedException();
        }

        protected override void Delete()
        {
            throw new NotImplementedException();
        }

        protected override int GetSelectedItemId()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
