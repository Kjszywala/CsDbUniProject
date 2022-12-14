using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public  class MainWindowViewModel : BaseViewModel
    {
        //będzie zawierała kolekcje komend, które pojawiają się w menu lewym oraz kolekcje zakładek 
        #region Komendy menu i paska narzedzi
        public ICommand NowyTowarCommand //ta komenda zostanie podpieta pod menu i pasek narzedzi
        { 
            get
            {
                return new BaseCommand(()=>createView(new NowyTowarViewModel()));//to jest komenda, która wyw. funkcję createTowar
            }
        }
        public ICommand TowaryCommand
        {
            get
            {
                return new BaseCommand(showAll<WszystkieTowaryViewModel>);
            }
        }
        public ICommand NowaFakturaCommand 
        {
            get
            {
                return new BaseCommand(() => createView(new NowaFakturaViewModel()));
            }
        }
        public ICommand FakturyCommand
        {
            get
            {
                return new BaseCommand(showAll<WszystkieFakturyViewModel>);
            }
        }
        #endregion

        #region Przyciski w menu z lewej strony
        private ReadOnlyCollection<CommandViewModel> _Commands;//to jest kolekcja komend w emnu lewym
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get 
            { 
                if(_Commands == null)//sprawdzam czy przyciski z lewej strony menu nie zostały zainicjalizowane
                {
                    List<CommandViewModel> cmds = this.CreateCommands();//tworzę listę przyciskow za pomocą funkcji CreateCommands
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);//tę listę przypisuje do ReadOnlyCollection (bo readOnlyCollection można tylko tworzyć, nie można do niej dodawać)
                }
                return _Commands; 
            }   
        }
        private List<CommandViewModel> CreateCommands()//tu decydujemy jakie przyciski są w lewym menu
        {
            Messenger.Default.Register<string>(this, Open); //item => Open(item)
            Messenger.Default.Register<MessengerMessage<NowaFakturaViewModel, PozycjaFaktury, object>>(this, OpenPozycjaFaktury);
            Messenger.Default.Register<MessengerMessage<WszystkieViewModel<object>, Type, int>>(this, Edit);

            return new List<CommandViewModel>
            {
                new CommandViewModel("Towary",new BaseCommand(showAll<WszystkieTowaryViewModel>)), //to tworzy pierwszy przycisk o nazwie Towary, który pokaże zakładkę wszystkie towary
                new CommandViewModel("Towar",new BaseCommand(()=>createView(new NowyTowarViewModel()))),
                new CommandViewModel("Faktury",new BaseCommand(showAll<WszystkieFakturyViewModel>)),
                new CommandViewModel("Fatura",new BaseCommand(()=>createView(new NowaFakturaViewModel()))),
                new CommandViewModel("Raport Sprzedazy",new BaseCommand(showAll<RaportSprzedazyViewModel>))
            };
        }

        private void OpenPozycjaFaktury(MessengerMessage<NowaFakturaViewModel, PozycjaFaktury, object> obj)
        {
            if(obj.Response == null)
            {
                NowaPozycjaFakturyViewModel nowaPozycjaFakturyViewModel = new NowaPozycjaFakturyViewModel();
                nowaPozycjaFakturyViewModel.Message = obj;
                createView(nowaPozycjaFakturyViewModel);
            }
        }


        #endregion

        #region Zakładki
        private ObservableCollection<WorkspaceViewModel> _Workspaces; //to jest kolekcja zakładek
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get 
            {
                if(_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.onWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void onWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.onWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.onWorkspaceRequestClose;
        }
        private void onWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispos();
            this.Workspaces.Remove(workspace);
        }
        #endregion

        #region Funkcje pomocnicze
        private void Edit(MessengerMessage<WszystkieViewModel<object>, Type, int> message)
        {
            if(message.Response == typeof(FakturaForAllView))
            {
                createView(new NowaFakturaViewModel(message.Argument));
            }
        }

        /// <summary>
        /// Ta metoda jest wywolywana przez messenger po otrzymaniu wiadomosci
        /// </summary>
        /// <param name="name">wiadomosc</param>
        private void Open(string name)
        {
            if(name == "Towary Add")
            {
                createView(new NowyTowarViewModel());
            }
            else if(name == "Faktury Add")
            {
                createView(new NowaFakturaViewModel());
            }
            else if(name == "Kontrahenci Show")
            {
                showAll<WszyscyKontrahenciViewModel>();
            }
            else if (name == "Pozycje Faktury Add")
            {
                createView(new NowaPozycjaFakturyViewModel());
            }
        }
        //to jest funkcja, która otwiera nową zakładke Towar
        //za każdym tworzy nową NOWĄ zakładkę do dodawania towaru
        private void createView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.setActiveWorkspace(workspace);
        }

        //to jest funkcja, która otwiera nową zakładke Towary, faktury, i wszystkie inne
        //tworzy lub przelacza na taka jesli jest juz otwarta.
        private void showAll<T>() where T : WorkspaceViewModel, new()
        {
            T workspace = Workspaces.FirstOrDefault(vm => vm is T) as T;
            if (workspace == null)
            {
                workspace = new T();
                this.Workspaces.Add(workspace);
            }
            this.setActiveWorkspace(workspace);
        }

        private void setActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }


        #endregion

    }
}
