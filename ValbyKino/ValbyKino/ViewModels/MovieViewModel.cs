using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValbyKino.Models;

namespace ValbyKino.ViewModels
{
    public class MovieViewModel : ViewModelBase
    {
        IRepository<Movie> movieRepository = new MovieRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");
        public ObservableCollection<Movie> Movies { get; set; }
        public MovieViewModel() 
        {
            Movies = (ObservableCollection<Movie>)movieRepository.GetAll();
            Movies.Add(new Movie("Crossing", "En Kvinde i Istanbul", "Levan", "Akin", "TR", DateTime.Now, false));
            Movies.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
            Movies.Add(new Movie("Foredrag: Videnskaben bag øl", "Foredrag: Videnskaben bag øl", "", "", "DK", DateTime.Now, true));

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
        public Movie SelectedMovie { get; set; }

    }
}
