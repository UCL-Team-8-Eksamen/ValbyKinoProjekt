using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValbyKino.Models
{
    public class Datahandler
    {
        public string FileName { get; }
        IRepository<Show> showRepository = new ShowRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");
        public Datahandler(string fileName)
        {
            FileName = fileName;
        }


        public void PrintShows(ObservableCollection<Show> shows)
        {
            StreamWriter sw = new StreamWriter(FileName);
            sw.WriteLine("1. ORIGINAL TITLE; 2. LOCAL TITLE; 3. DIRECTOR'S FIRST NAME; 4. DIRECTOR'S LAST NAME; 5. FILM'S MAIN NATIONALITY; 6. NATIONAL RELEASE DATE; 7. 1st DATE OF RELEASE IN YOUR CINEMA; 8. VO/DB/ST; 9. SCREENING FORMAT; 10. 3D; 11. ALTERNATIVE CONTENT; 12. NB OF WEEKS; 13. TOTAL SCREENINGS; 14. ADMISSIONS; 15. BOX OFFICE IN LOCAL CURRENCY; 16. YA");
            for (int i = 0; i < shows.Count; i++)
            {
                int admissions = shows[i].Admissions;
                sw.WriteLine($"{shows[i].Movie.OriginalTitle}, {shows[i].Movie.LocalTitle}, {shows[i].Movie.DirectorFirstName}, {shows[i].Movie.DirectorLastName}, {shows[i].Movie.OriginalCountry}, {shows[i].Movie.NationalReleaseDate}, (first release date), {shows[i].Version.ToString()}, {shows[i].ScreeningFormat.ToString()}, , {shows[i].Movie.AlternativeContent}, (nb weeks), (total screenings), {shows[i].Admissions}, {shows[i].Admissions * shows[i].Price}, {shows[i].YA}");
            }
            sw.Close();

        }

        public void PrintMovies(ObservableCollection<Movie> movies)
        {
            StreamWriter sw = new StreamWriter(FileName);
            sw.WriteLine("1. ORIGINAL TITLE; 2. LOCAL TITLE; 3. DIRECTOR'S FIRST NAME; 4. DIRECTOR'S LAST NAME; 5. FILM'S MAIN NATIONALITY; 6. NATIONAL RELEASE DATE; 7. 1st DATE OF RELEASE IN YOUR CINEMA; 8. VO/DB/ST; 9. SCREENING FORMAT; 10. 3D; 11. ALTERNATIVE CONTENT; 12. NB OF WEEKS; 13. TOTAL SCREENINGS; 14. ADMISSIONS; 15. BOX OFFICE IN LOCAL CURRENCY; 16. YA");
            for (int i = 0; i < movies.Count; i++)
            {
                ObservableCollection<Show> shows = (ObservableCollection<Show>)showRepository.GetShowsByMovie(movies[i]);
                int admissions = 0;
                for (int j = 0; j < shows.Count; j++)
                {
                    admissions += shows[j].Admissions;
                }
                // if (shows.Count >= 1)
                //{
                sw.WriteLine($"{movies[i].OriginalTitle}, {movies[i].LocalTitle}, {movies[i].DirectorFirstName}, {movies[i].DirectorLastName}, {movies[i].OriginalCountry}, {movies[i].NationalReleaseDate}, {shows[0].Date},"); // {shows[1].Version.ToString()}, {shows[1].ScreeningFormat.ToString()}, , {shows[1].Movie.AlternativeContent}, (nb weeks), {shows.Count}, {admissions}, {shows[1].Admissions * shows[1].Price}, {shows[1].YA}");
                //}   
            }
            sw.Close();

        }

        public void PrintMovies(ObservableCollection<Movie> movies, ObservableCollection<Show> allshows)
        {
            StreamWriter sw = new StreamWriter(FileName);
            sw.WriteLine("1. ORIGINAL TITLE; 2. LOCAL TITLE; 3. DIRECTOR'S FIRST NAME; 4. DIRECTOR'S LAST NAME; 5. FILM'S MAIN NATIONALITY; 6. NATIONAL RELEASE DATE; 7. 1st DATE OF RELEASE IN YOUR CINEMA; 8. VO/DB/ST; 9. SCREENING FORMAT; 10. 3D; 11. ALTERNATIVE CONTENT; 12. NB OF WEEKS; 13. TOTAL SCREENINGS; 14. ADMISSIONS; 15. BOX OFFICE IN LOCAL CURRENCY; 16. YA");
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

                int admissions = 0;
                for (int j = 0; j < shows.Count; j++)
                {
                    admissions += shows[j].Admissions;
                }
                //sw.WriteLine(movies[i].MovieID);
                //sw.WriteLine(shows.Count);
                sw.WriteLine($"{movies[i].OriginalTitle}, {movies[i].LocalTitle}, {movies[i].DirectorFirstName}, {movies[i].DirectorLastName}, {movies[i].OriginalCountry}, {movies[i].NationalReleaseDate}, {shows[0].Date}, {shows[0].Version.ToString()}, {shows[0].ScreeningFormat.ToString()}, , {shows[0].Movie.AlternativeContent}, {(shows.Last().Date - shows[0].Date).TotalDays / 7}, {shows.Count}, {admissions}, {shows[0].Admissions * shows[0].Price}, {shows[0].YA}");
                //shows.Clear();
            }
            sw.Close();

        }

    }
}
