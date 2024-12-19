using DocumentFormat.OpenXml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ValbyKino.Models;

namespace ValbyKino.ViewModels
{
    public class AnnualReportViewModel : ViewModelBase
    {
        Report report = new Report("movies.csv");
        public ObservableCollection<Report> ReportList { get; set; }

        IRepository<Movie> movieRepository = new MovieRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");

        //test
        IRepository<Show> showRepository = new ShowRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");
        public AnnualReportViewModel()
        {
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

        private void DownloadReport()
        {
            report.PrintToCSV((ObservableCollection<Movie>)movieRepository.GetAll(), (ObservableCollection<Show>)showRepository.GetAll());
        }

        public RelayCommand DownloadReportCommand => new RelayCommand(execute => DownloadReport());
    }
}
