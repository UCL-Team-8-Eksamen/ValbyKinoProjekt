﻿using System;
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
            movieRepository.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
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
        //public Movie SelectedMovie { get; set; }

        //Jeg laver en privat metode, jeg vil have at metoden tilføjer en ny Movie til Movies samlingen
        private void AddMovie()
        {
            // Movies er samlingen
            // Add er metoden
            // new Movie kalder konstruktøren med de nødvendige parametre
            Movies.Add(new Movie(
                "Zorro",                        // OriginalTitle
                "Zorro Den Maskerede Hævner",   // LocalTitle
                "Martin",                       // DirectorFirstName
                "Campbell",                     // DirectorLastName
                "US",                           // OriginalCountry
                new DateTime(1998, 7, 17),      // NationalReleaseDate
                true                            // AlternativeContent
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

        //Vi laver en objekt af vores RelayCommand som vi kalder AddCommand. Jeg sætter AddCommand til at være en new RelayCommand,
        //execute er sat til at være metoden AddItem som tilføjer nye item til samlingen som hedder items
        //Fordi vi vil have at AddCommand kan udføres altid, fjerner vi canExecute => (return true;)
        public RelayCommand AddCommand => new RelayCommand(execute => AddMovie());

        //execute er sat til at være metoden DeleteItem som fjerner  item fra samlingen som hedder items
        //canExecute her gør at DeleteItem ikke er aktiveret, hvis der ikke er valgt noget, knappen bliver aktiv, når vi har valgt noget
        public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteMovie(), canExecute => SelectedItem != null);



    }
}
