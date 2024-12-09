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
                sw.WriteLine($"{shows[i].Movie.OriginalTitle}, {shows[i].Movie.LocalTitle}, {shows[i].Movie.DirectorFirstName}, {shows[i].Movie.DirectorLastName}, {shows[i].Movie.OriginalCountry}, {shows[i].Movie.NationalReleaseDate}, (first release date), {shows[i].Version.ToString()}, {shows[i].ScreeningFormat.ToString()}, , {shows[i].Movie.AlternativeContent}, (nb weeks), (total screenings), (admissions), (box office), {shows[i].YA}");
            }
            sw.Close();

        }
    }
}
