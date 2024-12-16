using System.Collections.ObjectModel;
using System.ComponentModel;
using ValbyKino.Models;

namespace ValbyKino.ViewModels
{
    public class AnnualReportViewModel : ViewModelBase
    {
        Report report = new Report("movies.csv");
        //Datahandler2 dh2 = new Datahandler2();
        public ObservableCollection<Report> ReportList { get; set; }

        IRepository<Movie> movieRepository = new MovieRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");

        //test
        IRepository<Show> showRepository = new ShowRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");
        public AnnualReportViewModel()
        {

            //movieRepository.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
            //Movies.Add(new Movie("Crossing", "En Kvinde i Istanbul", "Levan", "Akin", "TR", DateTime.Now, false));
            //Movies.Add(new Movie("Wicked", "Wicked", "John", "Chu", "US", DateTime.Now, false));
            //Movies.Add(new Movie("Foredrag: Videnskaben bag øl", "Foredrag: Videnskaben bag øl", "", "", "DK", DateTime.Now, true));
            report.PrintToCSV((ObservableCollection<Movie>)movieRepository.GetAll(), (ObservableCollection<Show>)showRepository.GetAll());
            ReportList = report.ReadFromCSV();
        }

        private ICollectionView reportCcollectionView;

        public ICollectionView ReportCollectionView
        {
            get { return reportCcollectionView; }
            set
            {
                reportCcollectionView = value;
                OnPropertyChanged(nameof(ReportCollectionView));
            }
        }
    }
}
