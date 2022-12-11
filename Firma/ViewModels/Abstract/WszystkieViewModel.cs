using Firma.Helpers;
using Firma.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels.Abstract
{
    //bo wszystkie zakladki dziedzicza po WorkspaceViewModel.
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel
    {
        #region Fields
        // To jest obiekt ktory bedzie sluzyl do operacji na bazie danych.
        public T SelectedItem { get; set; }
        private readonly FakturyEntities fakturyEntities;
        public FakturyEntities FakturyEntities 
        { 
            get 
            { 
                return fakturyEntities; 
            } 
        }
        //to jest komenda do zaladowania towarow.
        private BaseCommand _loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    // pusta wywoluje load.
                    _loadCommand = new BaseCommand(() => LoadItems());
                }
                return _loadCommand;
            }
        }
        private ICommand _RefreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if(_RefreshCommand == null)
                {
                    _RefreshCommand = new BaseCommand(() => LoadItems());
                }
                return _RefreshCommand;
            }
        }
        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new BaseCommand(() => Delete());
                }
                return _DeleteCommand;
            }
        }

        // komenda do dodawania.
        private BaseCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    // pusta wywoluje load.
                    _AddCommand = new BaseCommand(() => Add());
                }
                return _AddCommand;
            }
        }
        // w tym obiekcie beda wszystkie towary.
        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
        {
            get
            {
                // jak lista pusta wywolujemy load.
                if (_List == null)
                {
                    Load();
                }
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        public List<T> AllList { get; set; }
        public List<string> SortComboBoxItems { get; set; }
        private string _SortField;
        public string SortField
        {
            get
            {
                return _SortField;
            }
            set
            {
                if (_SortField != value)
                {
                    _SortField = value;
                    OnPropertyChanged(() => SortField);
                }
            }
        }
        private bool _SortDescending;
        public bool SortDescending
        {
            get
            {
                return _SortDescending;
            }
            set
            {
                if (_SortDescending != value)
                {
                    _SortDescending = value;
                    OnPropertyChanged(() => SortDescending);
                }
            }
        }
        private ICommand _SortCommand;
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                {
                    _SortCommand = new BaseCommand(() => Sort());
                }
                return _SortCommand;
            }
        }

        public List<string> SearchComboBoxItems { get; set; }
        private string _SearchField;
        public string SearchField
        {
            get
            {
                return _SearchField;
            }
            set
            {
                if (_SearchField != value)
                {
                    _SearchField = value;
                    OnPropertyChanged(() => SearchField);
                }
            }
        }
        private string _SearchText;
        public string SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                if (_SearchText != value)
                {
                    _SearchText = value?.ToLower().Trim();
                    OnPropertyChanged(() => SearchText);
                }
            }
        }
        private ICommand _SearchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_SearchCommand == null)
                {
                    _SearchCommand = new BaseCommand(() => Search());
                }
                return _SearchCommand;
            }
        }

        #endregion

        #region Commands

        #endregion

        #region Konstruktor
        public WszystkieViewModel(string displayName)
        {
            base.DisplayName = displayName;
            this.fakturyEntities = new FakturyEntities();
            SortComboBoxItems = GetSortComboBoxItems();
            SearchComboBoxItems = GetSearchComboBoxItems();
        }
        #endregion

        #region Helpers

        protected abstract void Delete();
        
        abstract protected void Sort();
        abstract protected void Search();
        abstract protected List<string> GetSortComboBoxItems();
        abstract protected List<string> GetSearchComboBoxItems();

        /// <summary>
        /// Pobiera modele z bazy danych, filtruje i sortuje. Uzywa Load() oraz Search().
        /// </summary>
        protected void LoadItems()
        {
            Load();
            Search();
        }

        /// <summary>
        /// Pobiera modele z bazy danych.
        /// </summary>
        public abstract void Load();
        public void Add()
        {
            Messenger.Default.Send(DisplayName + " Add");
        }

        #endregion
    }
}
