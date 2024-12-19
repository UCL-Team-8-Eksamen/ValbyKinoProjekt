using System.Collections.ObjectModel;
using System.ComponentModel;
using ValbyKino.Models;
using Version = ValbyKino.Models.Version;

namespace ValbyKino.ViewModels
{
    public class ShowViewModel : ViewModelBase
    {
        // Nyeste udgave
        public DateTime Date { get; set; } // Dato for Forestillingen
        public DateTime Time { get; set; } // Tidspunkt for Forestillingen
        public Version Version { get; set; } // Version af Forestillingen (fx 2D eller 3D)
        // int?
        public int ScreeningFormat { get; set; } // Format for Forestillingen (fx digital, analog, etc.)
        public string Category { get; set; } // Kategori (fx action, drama, etc.)
        public int RoomNumber { get; set; } // Salens nummer

        public double Price { get; set; } //Prisen på filmforestillingen

        public int Admissions { get; set; } = 0;

        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>(); // Liste over film
        public ObservableCollection<Show> Shows { get; set; } // Liste over Forestillinger

        // Opretter et repository for film, som bruges til at hente og manipulere data
        IRepository<Show> showRepository = new ShowRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");

        public ShowViewModel()
        {
            Shows = (ObservableCollection<Show>)showRepository.GetAll();
            // Initialiserer listen over shows ved at hente data fra repository
            //try
            //{
            //    Shows = (ObservableCollection<Show>)showRepository.GetAll();
            //}
            //catch (Exception ex)
            //{
            //    // Log fejl, hvis data ikke kan hentes
            //    Console.WriteLine($"Fejl under indlæsning af shows: {ex.Message}");
            //}

        }

        private Show selectedItem; // Det valgte element i UI

        // Det her vil repræsentere, uanset hvilket element er valgt, og lader os ændre dens oplysninger
        public Show? SelectedItem
        {
            get { return selectedItem; }

            // Hver gang setter bliver kaldt, kalder vi OnPropertyChanged. Hvis selectedItem bliver sat,
            // bliver alle passende informeret, og vores bindinger vil virke
            set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        private void AddShow()
        {
            // Shows er samlingen
            // Add er metoden
            // new Show kalder konstruktøren med de nødvendige parametre
            Shows.Add(new Show(Date, Time, Version, ScreeningFormat.ToString(), Category, RoomNumber, Price, Admissions));
        }

        private void DeleteShow()
        {
            // Movies er samlingen
            // Remove er metoden
            // selectedItem dvs. ting som er valgt
            if (selectedItem != null)
            {
                Shows.Remove(selectedItem);
            }
        }

        private ICollectionView showCollectionView; // Brugt til filtrering og sortering af data i UI

        public ICollectionView ShowCollectionView
        {
            get { return showCollectionView; }
            set
            {
                showCollectionView = value;
                OnPropertyChanged(nameof(ShowCollectionView));
            }
        }

        // Vi laver en objekt af vores RelayCommand, som vi kalder AddShowCommand. Jeg sætter AddShowCommand til at være en ny RelayCommand,
        // execute er sat til at være metoden AddShow, som tilføjer nye shows til samlingen, som hedder shows
        // Fordi vi vil have, at AddShowCommand kan udføres under visse betingelser, skriver vi betingelserne i CanExecute kodedelen
        public RelayCommand AddShowCommand => new RelayCommand(
            execute => AddShow(),
            canExecute => Date != null && Time != null && Version != null && ScreeningFormat > 0 && RoomNumber > 0);

        // execute er sat til at være metoden DeleteShow, som fjerner et item fra samlingen, som hedder items
        // canExecute her gør, at DeleteShow ikke er aktiveret, hvis der ikke er valgt noget. Knappen bliver aktiv, når vi har valgt noget
        public RelayCommand DeleteShowCommand => new RelayCommand(
            execute => DeleteShow(),
            canExecute => SelectedItem != null);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



