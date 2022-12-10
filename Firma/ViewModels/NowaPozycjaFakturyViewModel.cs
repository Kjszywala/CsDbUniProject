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

namespace Firma.ViewModels
{
    public class NowaPozycjaFakturyViewModel : JedenViewModel<PozycjaFaktury>
    {
        #region PolaIWlasciwosci
        public List<ComboBoxKeyAndValue> Towary { get; set; }
        public int? IdTowaru 
        {
            get
            {
                return Item.IdTowaru;
            }
            set
            {
                if(Item.IdTowaru != value)
                {
                    Item.IdTowaru = value;
                    OnPropertyChanged(() => IdTowaru);
                }
            }
        }
        public decimal? Ilosc
        {
            get
            {
                return Item.Ilosc;
            }
            set
            {
                if (Item.Ilosc != value)
                {
                    Item.Ilosc = value;
                    OnPropertyChanged(() => Ilosc);
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
                if (Item.Cena != value)
                {
                    Item.Cena = value;
                    OnPropertyChanged(() => Cena);
                }
            }
        }
        public decimal? Rabat
        {
            get
            {
                return Item.Rabat;
            }
            set
            {
                if (Item.Rabat != value)
                {
                    Item.Rabat = value;
                    OnPropertyChanged(() => Rabat);
                }
            }
        }
        #endregion
        #region Constructor

        public NowaPozycjaFakturyViewModel() : base("Pozycja faktury")
        {
            Item = new PozycjaFaktury()
            {
                Czy_Aktywny = true,
                Cena = 0,
                Rabat = 0,
                Ilosc = 0
            };
            Towary = Db.Towar.Where(item => item.CzyAktywny == true)
                             .Select(item => new ComboBoxKeyAndValue() { Key = item.IdTowaru, Value = item.Nazwa })
                             .ToList();
        }

        #endregion

        #region Helpers

        public override void Save()
        {
            Messenger.Default.Send(Item);
        }

        #endregion
    }
}
