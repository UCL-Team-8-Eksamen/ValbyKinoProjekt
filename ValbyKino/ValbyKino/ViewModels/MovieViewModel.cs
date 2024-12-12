using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using ValbyKino.Models;

namespace ValbyKino.ViewModels
{
    public class MovieViewModel : ViewModelBase
    {

        //Nyeste udgave
        public string OriginalTitle { get; set; }
        public string LocalTitle { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }
        public string OriginalCountry { get; set; }
        public DateTime NationalReleaseDate { get; set; }
        public bool AlternativeContent { get; set; }
        IRepository<Movie> movieRepository = new MovieRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");

        //test
        IRepository<Show> showRepository = new ShowRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");

        Datahandler datahandler = new Datahandler("movies.csv");
        //Datahandler2 dh2 = new Datahandler2();
        public ObservableCollection<Movie> Movies { get; set; }
        public MovieViewModel()
        {
            ObservableCollection<Show> shows = (ObservableCollection<Show>)showRepository.GetAll();
            ObservableCollection<Movie> movies = (ObservableCollection<Movie>)movieRepository.GetAll();
            datahandler.PrintToCSV(movies, shows);

            //ObservableCollection<Show> shows2 = (ObservableCollection<Show>)movieRepository.GetShowsByMovie(movies[1]);

            //dh2.ConvertToExcel("movies.csv");
            //dh2.Convert_CSV_To_Excel("movies.csv");


            //Movie Wicked = new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false);
            Movies = (ObservableCollection<Movie>)movieRepository.GetAll();
            //movieRepository.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
            //Movies.Add(new Movie("Crossing", "En Kvinde i Istanbul", "Levan", "Akin", "TR", DateTime.Now, false));
            //Movies.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
            //Movies.Add(new Movie("Foredrag: Videnskaben bag øl", "Foredrag: Videnskaben bag øl", "", "", "DK", DateTime.Now, true));

        }

        private ICollectionView movieCcollectionView;

        public ICollectionView MovieCollectionView
        {
            get { return movieCcollectionView; }
            set
            {
                movieCcollectionView = value;
                OnPropertyChanged(nameof(MovieCollectionView));
            }
        }
        //public Movie SelectedMovie { get; set; }

        //Nyeste udgave
        private void AddMovie()
        {
            // Movies er samlingen
            // Add er metoden
            // new Movie kalder konstruktøren med de nødvendige parametre
            Movies.Add(new Movie(
                OriginalTitle = "Zorro", LocalTitle =                    // OriginalTitle
                "Zorro Den Maskerede Hævner",   // LocalTitle
                DirectorFirstName = "Martin",                       // DirectorFirstName
                DirectorLastName = "Campbell",                     // DirectorLastName
                OriginalCountry = "US",                           // OriginalCountry
                NationalReleaseDate = new DateTime(1998, 7, 17),      // NationalReleaseDate
                AlternativeContent = true                            // AlternativeContent
            ));
        }

        //Det her vil repræsenter, uanset hvilket element er valgt,og lader os ændre dens oplysninger
        private Movie selectedItem;

        public Movie? SelectedItem
        {
            get { return selectedItem; }

            //Hver gang setter bliver kaldt kalder vi OnPropertyChanged. Hvis selectedItem bliver sat bliver alle passende informeret og vores bindinger vil virke
            set { selectedItem = value; OnPropertyChanged(); }
        }

        //Movies er samlingen
        //Remove er metoden
        //selectedItem dvs. ting som er valgt
        private void DeleteMovie()
        {

            Movies.Remove(selectedItem);

        }

        //Vi laver en objekt af vores RelayCommand som vi kalder AddMovieCommand. Jeg sætter AddMovieCommand til at være en new RelayCommand,
        //execute er sat til at være metoden AddItem som tilføjer nye item til samlingen som hedder items
        //Fordi vi vil have at AddMovieCommand kan udføres altid, fjerner vi canExecute => (return true;)
        public RelayCommand AddMovieCommand => new RelayCommand(execute => AddMovie());

        //execute er sat til at være metoden DeleteItem som fjerner  item fra samlingen som hedder items
        //canExecute her gør at DeleteItem ikke er aktiveret, hvis der ikke er valgt noget, knappen bliver aktiv, når vi har valgt noget
        public RelayCommand DeleteMovieCommand => new RelayCommand(execute => DeleteMovie(), canExecute => SelectedItem != null);



    }
}
