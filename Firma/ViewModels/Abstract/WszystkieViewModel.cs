using Firma.Helpers;
using Firma.Models.Entities;
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
                    _loadCommand = new BaseCommand(() => Load());
                }
                return _loadCommand;
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

        #endregion

        #region Konstruktor
        public WszystkieViewModel(string displayName)
        {
            base.DisplayName = displayName;
            this.fakturyEntities = new FakturyEntities();
        }
        #endregion

        #region Helpers
        public abstract void Load();
        #endregion
    }
}
