using System.ComponentModel;
using System.Windows.Input;
using ValbyKino.Views;

namespace ValbyKino.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView;
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
        public ICommand ShowMovieViewCommand { get; }
        public ICommand ShowShowViewCommand { get; }
        public ICommand ShowAnnualReportViewCommand { get; }


        //kommandoer bliver (commands) oprettet og tilknyttet funktionalitet (forskellige visninger),
        //og en startværdi for CurrentView (LoginView) bliver indstillet.
        public MainWindowViewModel()
        {
            //når kommandoen udføres skifter den aktuelle visning (CurrentView) til en instans af MovieView
            ShowMovieViewCommand = new RelayCommand(_ => CurrentView = new MovieView());
            ShowShowViewCommand = new RelayCommand(_ => CurrentView = new ShowView());
            ShowAnnualReportViewCommand = new RelayCommand(_ => CurrentView = new AnnualReportView());


            CurrentView = new LoginView();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
