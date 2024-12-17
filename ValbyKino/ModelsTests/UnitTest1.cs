using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValbyKino.Models;
using System.IO;

namespace ModelTest
{
    [TestClass]

    //Testing the Movie class for initializing properties
    public class MovieTest
    {
        [TestMethod]
        public void Constructor_ShouldInitializeMovieCorrectly()
        {
            // Arrange
            string originalTitle = "A casa tutti bene";
            string localTitle = "Min italienske familie";
            string firstName = "Gabriele";
            string lastName = "Muccino";
            string originalCountry = "IT";
            DateTime nationalReleaseDate = new DateTime(2019, 1, 3);
            bool alternativeContent = false;

            // Act
            var movie = new Movie(originalTitle, localTitle, firstName, lastName, originalCountry, nationalReleaseDate, alternativeContent);

            // Assert
            Assert.AreEqual(originalTitle, movie.OriginalTitle);
            Assert.AreEqual(localTitle, movie.LocalTitle);
            Assert.AreEqual(firstName, movie.DirectorFirstName);
            Assert.AreEqual(lastName, movie.DirectorLastName);
            Assert.AreEqual(originalCountry, movie.OriginalCountry);
            Assert.AreEqual(nationalReleaseDate, movie.NationalReleaseDate);
            Assert.AreEqual(alternativeContent, movie.AlternativeContent);
        }
    }

    //Testing the Show class for initializing properties
    public class ShowTest
    {
        [TestMethod]
        public void Constructor_ShouldInitializeShowCorrectly()
        {
            // Arrange
            DateTime date = new DateTime(2019, 1, 12);
            DateTime time = new DateTime(1, 1, 1, 15, 25, 0); //Initializes a DateTime with 15:25 as the time and 0001-01-01 as the date
            Version version = Version.ST;
            int screeningFormat = "1";
            string category = "Europa Kino";
            int roomNumber = 1;
            double price = 89.00;

            // Act
            var show = new Show(date, version, screeningFormat, category, roomNumber, price);

            // Assert
            Assert.AreEqual(date, show.Date);
            Assert.AreEqual(time.TimeOfDay, show.Time.TimeOfDay); // Compare only the time part
            Assert.AreEqual(version, show.Version);
            Assert.AreEqual(screeningFormat, show.ScreeningFormat);
            Assert.AreEqual(category, show.Category);
            Assert.AreEqual(roomNumber, show.RoomNumber);
            Assert.AreEqual(price, show.Price);
        }
    }

    //Testing the Report class methods
    public class ReportTests
    {
        [TestMethod]
        public void Constructor_WithFileName_ShouldInitializeCorrectly()
        {
            // Arrange
            string fileName = "test.csv";

            // Act
            var report = new Report(fileName);

            // Assert
            Assert.AreEqual(fileName, report.FileName);
            Assert.IsNull(report.Show);
        }

        //Testing if the constructor method initialize properly
        [TestMethod]
        public void Constructor_WithParameters_ShouldInitializeCorrectly()
        {
            // Arrange
            var show = new Show();
            int weeks = 5;
            int screenings = 15;
            double boxOffice = 6300;
            int admissions = 91;

            // Act
            var report = new Report(show, weeks, screenings, boxOffice, admissions);

            // Assert
            Assert.AreEqual(show, report.Show);
            Assert.AreEqual(weeks, report.AmountOfWeeks);
            Assert.AreEqual(screenings, report.TotalScreenings);
            Assert.AreEqual(boxOffice, report.BoxOffice);
            Assert.AreEqual(admissions, report.Admissions);
        }

        //Testing the method ReadFromCSV
        [TestMethod]
        public void ReadFromCSV_ShouldParseCSVFileCorrectly()
        {
            // Arrange
            string TextCsv = "OriginalTitle,LocalTitle,DirectorFirstName,DirectorLastName,OriginalCountry,NationalReleaseDate,Date,Version,ScreeningFormat,3D,AltContent,NbWeeks,TotalScreenings,Admissions,BoxOffice,YA\n" +
                             "Batman,Batman,Anthony,Rie,USA,2022-01-01,2023-01-01,VO,1,,1,5,15,91,6300,1";

            var fileName = "Read.csv";
            File.WriteAllText(fileName, TextCsv); // Write the mock CSV file to disk.

            var report = new Report(fileName);

            // Act
            var reports = report.ReadFromCSV();

            // Assert
            Assert.AreEqual(1, reports.Count);
            Assert.AreEqual("Batman", reports[0].Show.Movie.OriginalTitle);
            Assert.AreEqual("1", reports[0].Show.ScreeningFormat);
            Assert.AreEqual(5, reports[0].AmountOfWeeks);

            // Clean up
            File.Delete(fileName); // Remove mock file after test.
        }

        //Testing the method PrintToCSV
        [TestMethod]
        public void PrintToCSV_ShouldWriteExpectedOutput()
        {
            // Arrange
            string expectedHeader = "1. ORIGINAL TITLE, 2. LOCAL TITLE, 3. DIRECTOR'S FIRST NAME, 4. DIRECTOR'S LAST NAME, 5. FILM'S MAIN NATIONALITY, 6. NATIONAL RELEASE DATE, 7. 1st DATE OF RELEASE IN YOUR CINEMA, 8. VO/DB/ST, 9. SCREENING FORMAT, 10. 3D, 11. ALTERNATIVE CONTENT, 12. NB OF WEEKS, 13. TOTAL SCREENINGS, 14. ADMISSIONS, 15. BOX OFFICE IN LOCAL CURRENCY, 16. YA";
            var movies = new ObservableCollection<Movie>
            {
                new Movie("Batman", "Batman", "Anthony", "Rie", "USA", DateTime.Now, false)
            };
            var allShows = new ObservableCollection<Show>
            {
                new Show(DateTime.Now, DateTime.Now, Version.VO, "1", "Superhero", 1, 95.00)
            };

            var report = new Report("output.csv");

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                report.PrintToCSV(movies, allShows);

                // Assert
                var output = sw.ToString().Trim();
                Assert.IsTrue(output.StartsWith(expectedHeader));
            }
        }
    }
}