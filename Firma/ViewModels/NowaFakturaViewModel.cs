using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using Firma.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class NowaFakturaViewModel : JedenWszystkieViewModel<Faktura, PozycjaFakturyForAllView> //bo wszystkie zakładki dziedzicza po workspaceVM
    {
        #region PolaIWlasciwosci
        public List<SposobPlatnosci> sposobPlatnosci { get; set; }
        public List<ComboBoxKeyAndValue> Kontrahenci { get; set; }
        private string _DaneKontrahenta;
        public string DaneKontrahenta
        {
            get
            {
                return _DaneKontrahenta;
            }
            set
            {
                if(value != _DaneKontrahenta)
                {
                    _DaneKontrahenta = value;
                    OnPropertyChanged(() => DaneKontrahenta);
                }
            }
        }
        #endregion

        #region Konstruktor
        public NowaFakturaViewModel()
            :base("Faktura", "Pozycje Faktury")
        {
            Item = new Faktura()
            {
                CzyAktywna = true,
                CzyZatwierdzona = false
            };
            Db.Faktura.AddObject(Item);

            List = new ObservableCollection<PozycjaFakturyForAllView>();
            sposobPlatnosci = Db.SposobPlatnosci.Where(item=> item.CzyAktywny == true).ToList();
            Kontrahenci = Db.Kontrahent.Where(item => item.CzyAktywny == true).Select(item => new ComboBoxKeyAndValue()
            {
                Key = item.IdKontrahenta,
                Value = item.Nazwa + " - " + item.NIP + " (" + item.Kod + ")"
            }).ToList();
            Messenger.Default.Register<Kontrahent>(this, GetWybranyKontrahent);
            Messenger.Default.Register<PozycjaFaktury>(this, PrzypiszPozycjeFaktury);
            Messenger.Default.Register<MessengerMessage<NowaFakturaViewModel, PozycjaFaktury, object>>(this, PrzypiszPozycjeFaktury2);
        }

        public NowaFakturaViewModel(int id)
           : base("Faktura", "Pozycje Faktury")
        {
            Item = Db.Faktura.First(item => item.IdFaktury == id);
            List = new ObservableCollection<PozycjaFakturyForAllView>();

            sposobPlatnosci = Db.SposobPlatnosci.Where(item => item.CzyAktywny == true).ToList();
            Kontrahenci = Db.Kontrahent.Where(item => item.CzyAktywny == true).Select(item => new ComboBoxKeyAndValue()
            {
                Key = item.IdKontrahenta,
                Value = item.Nazwa + " - " + item.NIP + " (" + item.Kod + ")"
            }).ToList();
            Messenger.Default.Register<Kontrahent>(this, GetWybranyKontrahent);
            Messenger.Default.Register<PozycjaFaktury>(this, PrzypiszPozycjeFaktury);
            Messenger.Default.Register<MessengerMessage<NowaFakturaViewModel, PozycjaFaktury, object>>(this, PrzypiszPozycjeFaktury2);
        }

        private void PrzypiszPozycjeFaktury2(MessengerMessage<NowaFakturaViewModel, PozycjaFaktury, object> obj)
        {
            if(obj.Sender == this && obj.Response != null)
            {
                Item.PozycjaFaktury.Add(obj.Response);
                Towar towar = Db.Towar.First(item => item.IdTowaru == obj.Response.IdTowaru);
                List.Add(new PozycjaFakturyForAllView(
                    towar.Kod,
                    towar.Nazwa,
                    obj.Response.Cena,
                    obj.Response.Ilosc,
                    obj.Response.Rabat)
                    );
            }
        }
        #endregion

        #region Properties
        public string Numer
        {
            get
            {
                return Item.Numer;
            }
            set
            {
                if (value != Item.Numer)
                {
                    Item.Numer = value;
                    OnPropertyChanged(() => Numer);
                }
            }
        }

        public DateTime? DataWystawienia
        {
            get
            {
                return Item.DataWystawienia;
            }
            set
            {
                if (value != Item.DataWystawienia)
                {
                    Item.DataWystawienia = value;
                    OnPropertyChanged(() => DataWystawienia);
                }
            }
        }

        public int? IdKontrahenta
        {
            get
            {
                return Item.IdKontahenta;
            }
            set
            {
                if (value != Item.IdKontahenta)
                {
                    Item.IdKontahenta = value;
                    OnPropertyChanged(() => IdKontrahenta);
                }
            }
        }

        public DateTime? TerminPlatnosci
        {
            get
            {
                return Item.TerminPlatnosci;
            }
            set
            {
                if (value != Item.TerminPlatnosci)
                {
                    Item.TerminPlatnosci = value;
                    OnPropertyChanged(() => TerminPlatnosci);
                }
            }
        }

        public int? IdSposobuPlatnosci
        {
            get
            {
                return Item.IdSposobuPlatnosci;
            }
            set
            {
                if (value != Item.IdSposobuPlatnosci)
                {
                    Item.IdSposobuPlatnosci = value;
                    OnPropertyChanged(() => IdSposobuPlatnosci);
                }
            }
        }
        public bool? CzyZatwierdzona
        {
            get
            {
                return Item.CzyZatwierdzona;
            }
            set
            {
                if (value != Item.CzyZatwierdzona)
                {
                    Item.CzyZatwierdzona = value;
                    OnPropertyChanged(() => CzyZatwierdzona);
                }
            }
        }

        public bool? CzyAktywna
        {
            get
            {
                return Item.CzyAktywna;
            }
            set
            {
                if (value != Item.CzyAktywna)
                {
                    Item.CzyAktywna = value;
                    OnPropertyChanged(() => CzyAktywna);
                }
            }
        }
        #endregion

        #region Komendy
        private ICommand _WybierzKontrahentaCommand;
        public ICommand WybierzKontrahentaCommand
        {
            get
            {
                if(_WybierzKontrahentaCommand == null)
                {
                    _WybierzKontrahentaCommand = new BaseCommand(() => WybierzKontrahenta());
                }
                return _WybierzKontrahentaCommand;
            }
        }

        #endregion

        #region Helpers
        private void GetWybranyKontrahent(Kontrahent kontrahent)
        {
            DaneKontrahenta = $"{kontrahent.Nazwa} {kontrahent.NIP} {kontrahent.Kod}";
            IdKontrahenta = kontrahent.IdKontrahenta;
        }
        private void PrzypiszPozycjeFaktury(PozycjaFaktury pozycjaFaktury)
        {
            
            Item.PozycjaFaktury.Add(pozycjaFaktury);
            Towar towar = Db.Towar.First(item => item.IdTowaru == pozycjaFaktury.IdTowaru);
            List.Add(new PozycjaFakturyForAllView(
                towar.Kod,
                towar.Nazwa,
                pozycjaFaktury.Cena,
                pozycjaFaktury.Ilosc,
                pozycjaFaktury.Rabat)
                );
            //FakturyEntities db1 = new FakturyEntities(), db2 = new FakturyEntities();
            //Faktura faktura = db1.Faktura.First();
            //faktura.Kontrahent = db2.Kontrahent.First();
            //db1.SaveChanges();
        }
        private void WybierzKontrahenta()
        {
            Window window = new Window();
            WszyscyKontrahenciView wszyscyKontrahenciView = new WszyscyKontrahenciView();
            wszyscyKontrahenciView.DataContext= new WszyscyKontrahenciViewModel();
            window.Content = wszyscyKontrahenciView;
            window.Show();
            //Messenger.Default.Send("Kontrahenci Show");
        }
        protected override void ShowAddView()
        {
            //Messenger.Default.Send(DisplayNameList + " Add");
            //Messenger.Default.Send(new MessengerMessage<NowaFakturaViewModel, PozycjaFaktury>() { Sender=this });
            MessengerMessage<NowaFakturaViewModel, PozycjaFaktury, object> message = new MessengerMessage<NowaFakturaViewModel, PozycjaFaktury, object>()
            {
                Sender = this
            };
            Messenger.Default.Send(message);
        }
        public override void Save()
        {
            //Item.CzyAktywna = true;
            //najpierw dodajemy towar do lokalnej kolekcji.
            //Db.Faktura.AddObject(Item);
            //nastepnie zapisujemy zmiany w bazie danych.
            Db.SaveChanges();
        }

        #endregion
    }
}
