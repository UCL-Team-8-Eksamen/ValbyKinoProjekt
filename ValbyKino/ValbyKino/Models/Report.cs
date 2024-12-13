using System.Collections.ObjectModel;
using System.IO;

namespace ValbyKino.Models
{
    public class Report
    {
        public string FileName { get; set; }
        public Report(string fileName)
        {
            FileName = fileName;
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
