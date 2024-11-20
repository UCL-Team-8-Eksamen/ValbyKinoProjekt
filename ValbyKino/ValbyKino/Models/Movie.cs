using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ValbyKino.Models
{
    public class Movie
    {
        public string OriginalTitle { get; set; }
        public string LocalTitle { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }
        public string Nationality { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool AlternativeContent { get; set; }

        public Movie(string originalTitle, string localTitle, string firstName, string lastName, string nationality, DateTime releaseDate, bool alternativeContent)
        {
            OriginalTitle = originalTitle;
            LocalTitle = localTitle;
            DirectorFirstName = firstName;
            DirectorLastName = lastName;
            Nationality = nationality;
            ReleaseDate = releaseDate;
            AlternativeContent = alternativeContent;
        }
    }
}
