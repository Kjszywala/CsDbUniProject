using Firma.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Firma.ViewModels.Abstract
{
    /// <summary>
    /// ViewModel realizujacy polaczenie jeden-do-wielu.
    /// </summary>
    /// <typeparam name="J">Typ Jeden: np Faktura</typeparam>
    /// <typeparam name="W">Tym Wiele: np Pozycja faktury</typeparam>
    public abstract class JedenWszystkieViewModel<J, W> : JedenViewModel<J>
    {
        #region properties

        private ObservableCollection<W> _List;
        public ObservableCollection<W> List
        {
            get
            {
                return _List;
            }
            set
            {
                if (value != _List)
                {
                    _List = value;
                    OnPropertyChanged(() => List);
                }
            }
        }

        private ICommand _ShowAddViewCommand;
        public ICommand ShowAddViewCommand
        {
            get
            {
                if (_ShowAddViewCommand == null)
                {
                    _ShowAddViewCommand = new BaseCommand(() => ShowAddView());
                }
                return _ShowAddViewCommand;
            }
        }

        public string DisplayNameList { get; set; }

        #endregion

        #region Constructor
        public JedenWszystkieViewModel(string displayName, string displayNameList)
            : base(displayName)
        { 
            DisplayNameList = displayNameList;
        }
        #endregion

        #region Metody

        abstract protected void ShowAddView();

        #endregion
    }
}

