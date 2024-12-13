using System.Collections.ObjectModel;
using System.ComponentModel;
using ValbyKino.Models;
using Version = ValbyKino.Models.Version;
using System.Net;

namespace ValbyKino.ViewModels
{
    public class ShowViewModel : ViewModelBase
    {


            // movieRepository.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
            // Movies.Add(new Movie("Crossing", "En Kvinde i Istanbul", "Levan", "Akin", "TR", DateTime.Now, false));
            // Movies.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
            // Movies.Add(new Movie("Foredrag: Videnskaben bag øl", "Foredrag: Videnskaben bag øl", "", "", "DK", DateTime.Now, true));
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
            Shows.Add(new Show(Date, Time, Version, ScreeningFormat, Category, RoomNumber));
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


    }
}

