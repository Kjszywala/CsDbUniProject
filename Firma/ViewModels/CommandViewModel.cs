using System;
using System.Windows.Input;

namespace Firma.ViewModels
{
    //to jest klasa 
    public class CommandViewModel:BaseViewModel
    {
        #region Własciwosci
        public string DisplayName { get; set; } //to jest nazwa przycisku w menu z lewej strony
        public ICommand Command { get; set; } //każdy przycisk zawiera komende, ktora wywoluje funkcje, ktora otwiera zakladke
        #endregion
        #region Konstruktor
        public CommandViewModel(string displayName, ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("Command");
            this.DisplayName = displayName;
            this.Command = command;
        }
        #endregion
    }
}
