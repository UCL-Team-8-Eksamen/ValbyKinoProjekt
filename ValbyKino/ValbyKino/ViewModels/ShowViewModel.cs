using System.Collections.ObjectModel;
using System.ComponentModel;
using ValbyKino.Models;
using Version = ValbyKino.Models.Version;
using System.Net;

namespace ValbyKino.ViewModels
{
    public class ShowViewModel : ViewModelBase
    {
        //Nyeste udgave
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public Version Version { get; set; }
        //int?
        public int ScreeningFormat { get; set; }
        public string Category { get; set; }
        public int RoomNumber { get; set; }
        public ObservableCollection<Movie> Movies { get; set; }

        //??Hvad gør den her kode??
        IRepository<Show> showRepository = new ShowRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");
        public ObservableCollection<Show> Shows { get; set; }
        public ShowViewModel()
        {
            Shows = (ObservableCollection<Show>)showRepository.GetAll();
            showRepository.Add(new Show(DateTime.Now, DateTime.Now, Version.ST, 2, "Forpremiere", 2));
            //movieRepository.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
            //Movies.Add(new Movie("Crossing", "En Kvinde i Istanbul", "Levan", "Akin", "TR", DateTime.Now, false));
            //Movies.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
            //Movies.Add(new Movie("Foredrag: Videnskaben bag øl", "Foredrag: Videnskaben bag øl", "", "", "DK", DateTime.Now, true));

        }

        private ICollectionView showCollectionView;

        public ICollectionView ShowCollectionView
        {
            get { return showCollectionView; }
            set
            {
                showCollectionView = value;
                OnPropertyChanged(nameof(ShowCollectionView));
            }
        }


        //Metodeafsnit (AddShow og DeleteShow)
        private void AddShow()
        {
            // Shows er samlingen
            // Add er metoden
            // new Show kalder konstruktøren med de nødvendige parametre
            Shows.Add(new Show(
            Date, Time, Version, ScreeningFormat, Category, RoomNumber

            ));
        }


        //Movies er samlingen
        //Remove er metoden
        //selectedItem dvs. ting som er valgt
        private void DeleteShow()
        {

            Shows.Remove(selectedItem);

        }

        //Det her vil repræsenter, uanset hvilket element er valgt,og lader os ændre dens oplysninger
        private Show selectedItem;

        public Show? SelectedItem
        {
            get { return selectedItem; }

            //Hver gang setter bliver kaldt kalder vi OnPropertyChanged. Hvis selectedItem bliver sat bliver alle passende informeret og vores bindinger vil virke
            set { selectedItem = value; OnPropertyChanged(); }
        }


        //Vi laver en objekt af vores RelayCommand som vi kalder AddShowCommand. Jeg sætter AddShowCommand til at være en new RelayCommand,
        //execute er sat til at være metoden AddShow som tilføjer nye show til samlingen som hedder shows
        //Fordi vi vil have at AddShowCommand kan udføres, under vise betingelser skriver vi betingelserne i CanExecute kodedelen
        //public RelayCommand AddShowCommand => new RelayCommand(execute => AddShow(), canExecute => Movie != null && Date != null && Time != null && Version != null && ScreeningFormat != null && RoomNumber != null);

        //execute er sat til at være metoden DeleteItem som fjerner  item fra samlingen som hedder items
        //canExecute her gør at DeleteItem ikke er aktiveret, hvis der ikke er valgt noget, knappen bliver aktiv, når vi har valgt noget
        public RelayCommand DeleteShowCommand => new RelayCommand(execute => DeleteShow(), canExecute => SelectedItem != null);



    }
}
