using Firma.Helpers;
using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels.Abstract
{
    //to jest klasa z ktorej beda dziedziczyly wszystkie zakladki
    //np. dodajace rekordy.
    public abstract class JedenViewModel<T>: WorkspaceViewModel
    {
        #region Fields
        //tu jest cala baza danych.
        public FakturyEntities Db { get; set; }
        //tu jest nasz dodawany towar.
        public T Item { get; set; }
        #endregion

        #region Konstruktor
        public JedenViewModel(string displayName)
        {
            base.DisplayName = displayName;
            Db = new FakturyEntities();
        }
        #endregion

        #region Commands
        // to jest commenda ktoraa zostanie podpieta (zbindowana) z przyciskiem
        // zapisz i zamknij. Komenda ta wywola funkcje saveAndClose.
        private BaseCommand _SaveAndCloseCommand;
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null)
                {
                    _SaveAndCloseCommand = new BaseCommand(() => saveAndClose());
                }
                return _SaveAndCloseCommand;
            }
        }
        #endregion

        #region Save
        public abstract void Save();

        private void saveAndClose()
        {
            //Zapisuje towar.
            Save();
            //zamyka zakladke.
            base.OnRequestClose();
        }
        #endregion
    }
}
