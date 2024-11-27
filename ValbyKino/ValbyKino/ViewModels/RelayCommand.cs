using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ValbyKino.ViewModels
{
    //Jeg bruger buttondCommand og binder her til RelayCommand i ViewModel, hvilket giver os mulighed for at udføre al vores udregning/logik
    //her, vi slipper for at lave kode i code behind.

    //Vi laver her en klasse RelayCommand, som implementerrer ICommand(), så vi kan lave vores kommandoer i vores ViewModel
    //Vi skal bruge using System.Windows.Input;
    //Vi kan implementer ICommand for alle command, i stedet for at bruge minimum krav af interface kan vi gøre det mere alsidigt og kraftfuldt 
    //DEN HER KODE KAN VI BRUGE I ALLE MVVMprojekter uden at ændre det her overhoved

    public class RelayCommand : ICommand
    {
        //Det her er en metode som har en enkelt parameter og som ikke returnerer en værdi. execute er en funktionskald
        private readonly Action<object> execute;

        //returner en værdi som er specificeret af bool parameteren, vil returnere en bool værdi
        private readonly Func<object, bool>? canExecute;

        //

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        //
        public bool CanExecute(object? parameter)
        {
            //vi kan udføre noget hvis vi ikke har en funktion for canExecute eller vi kan køre canExecute med parameter
            //
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
