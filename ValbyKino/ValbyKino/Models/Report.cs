using System.Collections.ObjectModel;
using System.IO;

namespace ValbyKino.Models
{
    public class Report
    {
        public string FileName { get; set; }
        public Show Show { get; set; }
        public int AmountOfWeeks { get; set; }
        public int TotalScreenings { get; set; }
        public double BoxOffice { get; set; }
        public double Admissions { get; set; }
        public string Is3D = " ";
        
        public ObservableCollection<Report> ReportList { get; set; }

        public Report(string fileName)
        {
            ReportList = new ObservableCollection<Report>();
            FileName = fileName;
        }

        public Report(Show show, int weeks, int screenings, double boxoffice, int admissions)
        {
            Show = show;
            AmountOfWeeks = weeks;
            TotalScreenings = screenings;
            BoxOffice = boxoffice;
            Admissions = admissions;
        }

        public ObservableCollection<Report> ReadFromCSV()
        {
            ObservableCollection<Report> reportList = new ObservableCollection<Report>();
            using (StreamReader reader = new StreamReader(FileName))
            {
                string headerLine = reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    bool altContent = false;
                    if (parts[10] == "1") altContent = true;
                    Show show = new Show
                    {
                        Date = DateTime.Parse(parts[6]),
                        Version = (Version)Enum.Parse(typeof(Version), parts[7]),
                        ScreeningFormat = parts[8],
                        YA = parts[15],
                        Movie = new Movie(parts[0], parts[1], parts[2], parts[3], parts[4], DateTime.Parse(parts[5]), altContent),
                    };
                    Report report = new Report(show, int.Parse(parts[11]), int.Parse(parts[12]), double.Parse(parts[14]), int.Parse(parts[13]));
                    reportList.Add(report);
                }
            }
            return reportList;
        }


        public void PrintToCSV(ObservableCollection<Movie> movies, ObservableCollection<Show> allshows)
        {
            StreamWriter sw = new StreamWriter(FileName);
            sw.WriteLine("1. ORIGINAL TITLE, 2. LOCAL TITLE, 3. DIRECTOR'S FIRST NAME, 4. DIRECTOR'S LAST NAME, 5. FILM'S MAIN NATIONALITY, 6. NATIONAL RELEASE DATE, 7. 1st DATE OF RELEASE IN YOUR CINEMA, 8. VO/DB/ST, 9. SCREENING FORMAT, 10. 3D, 11. ALTERNATIVE CONTENT, 12. NB OF WEEKS, 13. TOTAL SCREENINGS, 14. ADMISSIONS, 15. BOX OFFICE IN LOCAL CURRENCY, 16. YA");
            for (int i = 0; i < movies.Count; i++)
            {
                ObservableCollection<Show> shows = new ObservableCollection<Show>();

                foreach (Show show in allshows)
                {
                    if (show.Movie.LocalTitle.Equals(movies[i].LocalTitle))
                    {
                        shows.Add(show);
                    }
                }

                double admissions = 0;
                double boxoffice = 0;
                string ya = " ";
                double totalshows = 0;
                string altcontent = " ";
                if (shows[0].Category.Equals("Børnebiffen")) ya = "1";
                if (shows.Count == 1)
                {
                    totalshows = 1;
                }
                else
                {
                    totalshows = Math.Ceiling((shows.Last().Date - shows[0].Date).TotalDays / 7);
                }
                if (shows[0].Movie.AlternativeContent == true) altcontent = "1";
                for (int j = 0; j < shows.Count; j++)
                {
                    admissions += shows[j].Admissions;
                    boxoffice += shows[j].Admissions * shows[j].Price;
                }

                sw.WriteLine($"{movies[i].OriginalTitle}, {movies[i].LocalTitle}, {movies[i].DirectorFirstName}, {movies[i].DirectorLastName}, {movies[i].OriginalCountry}, {movies[i].NationalReleaseDate.ToString("dd-MM-yyyy")}, {shows[0].Date.ToString("dd-MM-yyyy")}, {shows[0].Version.ToString()}, {shows[0].ScreeningFormat.ToString()}, , {altcontent}, {totalshows}, {shows.Count}, {admissions}, {boxoffice}, {ya}");
            }
            sw.Close();

        }

    }
}
