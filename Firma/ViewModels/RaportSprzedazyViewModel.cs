using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Input;
using Firma.Helpers;
using Firma.Models.BusinessLogic;
using Firma.Models.EntitiesForView;

namespace Firma.ViewModels
{
    public class RaportSprzedazyViewModel: WorkspaceViewModel
    {
        #region PolaIWlasciwosci

        private ICommand _ObliczCommand;
        public ICommand ObliczCommand
        {
            get 
            { 
                if(_ObliczCommand == null)
                {
                    _ObliczCommand = new BaseCommand(() => ObliczUtarg());
                }
                return _ObliczCommand; 
            }
        }

        public TowarB TowarB { get; set; }

        public DateTime DataOd
        {
            get
            {
                return TowarB.DataOd;
            }
            set
            {
                if(TowarB.DataOd != value)
                {
                    TowarB.DataOd = value;
                    OnPropertyChanged(() => DataOd);
                }
            }
        }
        public DateTime DataDo
        {
            get
            {
                return TowarB.DataDo;
            }
            set
            {
                if (TowarB.DataDo != value)
                {
                    TowarB.DataDo = value;
                    OnPropertyChanged(() => DataDo);
                }
            }
        }

        public List<ComboBoxKeyAndValue> Towary { get; set; }

        public int WybraneTowarId 
        {
            get
            {
                return TowarB.TowarId;
            }
            set
            {
                if (TowarB.TowarId != value)
                {
                    TowarB.TowarId = value;
                    OnPropertyChanged(() => WybraneTowarId);
                }
            }
        }

        private decimal _Utarg;
        public decimal Utarg
        {
            get
            {
                return _Utarg;
            }
            set
            {
                if(_Utarg != value)
                {
                    _Utarg = value;
                    OnPropertyChanged(() => Utarg);
                }
            }
        }

        #endregion

        #region Konstruktor
        public RaportSprzedazyViewModel()
        {
            base.DisplayName = "Raport Sprzedazy";

            FakturyEntities db = new FakturyEntities();
            TowarB = new TowarB(db);
            Towary = db.Towar.Where(item => item.CzyAktywny == true)
                .Select(item => new ComboBoxKeyAndValue() { Key = item.IdTowaru, Value = item.Nazwa})
                .ToList();
        }

        #endregion

        #region Metody

        private void ObliczUtarg() => Utarg = TowarB.ObliczUtarg();

        #endregion

    }
}
