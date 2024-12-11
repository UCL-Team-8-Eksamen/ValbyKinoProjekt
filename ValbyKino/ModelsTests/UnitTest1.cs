using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValbyKino.Models;
using System.IO;

namespace ModelTest
{
    [TestClass]

    //Testing the Diector class for initializing properties
    public class MovieTest
    {
        [TestMethod]
        public void Constructor_ShouldInitializeDirectorCorrectly()
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
            Version version = 1;
            int screeningFormat = 1;
            string category = "Europa Kino";
            int roomNumber = 1;

            // Act
            var show = new Show(date, version, screeningFormat, category, roomNumber);

            // Assert
            Assert.AreEqual(date, show.Date);
            Assert.AreEqual(time.TimeOfDay, show.Time.TimeOfDay); // Compare only the time part
            Assert.AreEqual(version, show.Version);
            Assert.AreEqual(screeningFormat, show.ScreeningFormat);
            Assert.AreEqual(category, show.Category);
            Assert.AreEqual(roomNumber, show.RoomNumber);
        }
    }
}