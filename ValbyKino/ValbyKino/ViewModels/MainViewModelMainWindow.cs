using System.ComponentModel;
using System.Windows.Input;
using ValbyKino.Views;
//MainWindowViewModel arbejder sammen med MainWindow.xaml, som definerer, hvordan hovedvinduet ser ud.
//Denne kode styrer navigationen i applikationen. 
//Den del af min kode, der får MainWindowViewModel til at arbejde sammen med MainWindow.xaml,
//er defineret i MainWindow.xaml gennem bindingen af DataContext

//Bindingen af MainWindowViewModel til MainWindow.xaml
//xmlns:local="clr-namespace:ValbyKino.ViewModels". Denne linje gør MainWindow.xaml opmærksom på, at der findes en klasse i namespace ValbyKino.ViewModels
//Det betyder, at XAML-filen kan referere til MainWindowViewModel.Uden xmlns:local="clr-namespace:ValbyKino.ViewModels inden referance.

//DataContext = new MainWindowViewModel();
//Denne del siger: "Lad os forbinde brugergrænsefladen (UI) i MainWindow.xaml med en instans af MainWindowViewModel.
//Her oprettes en ny instans af MainWindowViewModel og bruges som DataContext for hele vinduet.
//DataContext er det, der gør det muligt for XAML at "snakke" med ViewModel'en.
//Uden denne binding ville XAML ikke kunne få adgang til egenskaber og kommandoer fra ViewModel'en.
//Når DataContext er sat til en instans af MainWindowViewModel, bliver alle bindingsudtryk i XAML
//(f.eks. {Binding ShowMovieViewCommand}) automatisk rettet mod egenskaber og kommandoer i denne ViewModel.
//For eksempel <Button Content="FILM" Command="{Binding ShowMovieViewCommand}" /> 
//Her bruger Command="{Binding ShowMovieViewCommand}" bindingen til at finde ShowMovieViewCommand i MainWindowViewModel.
//Når brugeren klikker på knappen, aktiveres denne kommando, og ViewModel'en skifter, hvilken side der vises i applikationen.


// Denne kode fortæller mig, at koden er en del af ViewModels, som er en sektion i programmet,
// der håndterer logik for, hvordan visninger fungerer.
namespace ValbyKino.ViewModels
{
    //Dette er en plan for, hvordan applikationen skal håndtere logik for hovedvinduet (MainWindow).
    //Dette er en mekanisme, som hjælper applikationen med at fortælle skærmen, når noget i baggrunden ændrer sig.
    //For eksempel, hvis en ny side skal vises(INotifyPropertyChanged).
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        //Denne linje gemmer information om, hvilken side/view der vises lige nu i applikationen.
        //Det er "privat", hvilket betyder, at kun denne klasse kan ændre det.
        private object _currentView;

        //Denne del gør det muligt at læse eller ændre den aktuelle side
        //Når nogen spørger "Hvad er den aktuelle side?", svarer koden med det, der er gemt i _currentView.
        //Når nogen siger "Skift til denne side", opdaterer koden _currentView og fortæller resten af applikationen,
        //at noget har ændret sig ved hjælp af OnPropertyChanged
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        //Commands til MainWindow
        //Disse er "knapper" i programmet, men i kodeform.
        //Når brugeren klikker på en knap, udføres en handling, der ændrer, hvad brugeren ser.(udføres den relevante kommando, som ændrer CurrentView.)
        //Kommandoerne i denne ViewModel er bundet til knapper i XAML via Command="{Binding ...}".
        public ICommand ShowMovieViewCommand { get; }
        public ICommand ShowShowViewCommand { get; }
        public ICommand ShowAnnualReportViewCommand { get; }


        //kommandoer bliver (commands) oprettet og tilknyttet funktionalitet (forskellige visninger),
        //og en startværdi for CurrentView (LoginView) bliver indstillet.
        //Denne del kører, når applikationen starter. Den sørger for at opsætte det nødvendige for at programmet fungerer.
        public MainWindowViewModel()
        {
            //Disse linjer siger, hvad der skal ske, når brugeren klikker på en knap:
            //når kommandoen udføres skifter den aktuelle visning (CurrentView) til en instans af MovieView
            //For eksempel Når brugeren klikker på "Film"-knappen, vises MovieView.
            ShowMovieViewCommand = new RelayCommand(_ => CurrentView = new MovieView());
            ShowShowViewCommand = new RelayCommand(_ => CurrentView = new ShowView());
            ShowAnnualReportViewCommand = new RelayCommand(_ => CurrentView = new AnnualReportView());

            //Når programmet starter, vises LoginView som det første, så brugeren kan logge ind.
            CurrentView = new LoginView();
        }

        //Dette er en "alarmklokke", der fortæller resten af programmet, at noget er ændret. For eksempel,
        //når den aktuelle side (CurrentView) skifter, bliver skærmen opdateret.
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
