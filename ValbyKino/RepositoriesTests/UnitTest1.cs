using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValbyKino.Models;
using System.IO;

namespace RepositoriesTests
{
    [TestClass]
    public class MovieRepositoryTests
    {
        private Mock<SqlConnection> _mockConnection;
        private Mock<SqlCommand> _mockCommand;
        private MovieRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            // Arrange: Mock the SQL connection and command
            _mockConnection = new Mock<SqlConnection>();
            _mockCommand = new Mock<SqlCommand>();

            mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Verifiable();
            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);

            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockReader.Object);
  
            var connectionString = "TestConnectionString";
            _repository = new MovieRepository(connectionString);
        }

        [TestMethod]
        public void Add_ShouldAddMovieToDatabase()
        {
            // Arrange
            var movie = new Movie
            {
                OriginalTitle = "Aquaman",
                LocalTitle = "Aquaman",
                DirectorFirstName = "James",
                DirectorLastName = "Wan",
                OriginalCountry = "AU",
                NationalReleaseDate = new DateTime(2018, 12, 13),
                AlternativeContent = false
            };

            // Act
            _repository.Add(movie);

            // Assert: Here you would verify that the Add method invoked the expected SQL commands.
            _mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Once);
        }

        [TestMethod]
        public void Delete_ShoulDeleteMovieFromDatabase()
        {
            // Arrange
            var movieId = 5;

            // Act
            _repository.Delete(movieId);

            // Assert: Here you would verify that the Add method invoked the expected SQL commands.
            mockCommand.Verify(cmd => cmd.Parameters.AddWithValue("@MovieId", movieId), Times.Once);
            _mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Once);
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectMovie()
        {
            // Arrange
            var movieId = 11;
            var expectedMovie = new Movie
            {
            OriginalTitle = "A casa tutti bene",
            LocalTitle = "Min italienske familie",
            DirectorFirstName = "Gabriele",
            DirectorLastName = "Muccino",
            OriginalCountry = "IT",
            NationalReleaseDate = new DateTime(2019, 1, 3),
            AlternativeContent = false
            };

            var mockReader = new Mock<SqlDataReader>();
            mockReader.SetupSequence(reader => reader.Read())
                      .Returns(true)
                      .Returns(false); // Simulate a single row in the result set
            mockReader.Setup(reader => reader["OriginalTitle"]).Returns(expectedMovie.OriginalTitle);
            mockReader.Setup(reader => reader["LocalTitle"]).Returns(expectedMovie.LocalTitle);
            mockReader.Setup(reader => reader["DirectorFirstName"]).Returns(expectedMovie.DirectorFirstName);
            mockReader.Setup(reader => reader["DirectorLastName"]).Returns(expectedMovie.DirectorLastName);
            mockReader.Setup(reader => reader["OriginalCountry"]).Returns(expectedMovie.OriginalCountry);
            mockReader.Setup(reader => reader["NationalReleaseDate"]).Returns(expectedMovie.NationalReleaseDate);
            mockReader.Setup(reader => reader["AlternativeContent"]).Returns(expectedMovie.AlternativeContent);

            // Act
            var actualMovie = repository.GetById(movieId);

            // Assert
            Assert.IsNotNull(actualMovie);
            Assert.AreEqual(expectedMovie.OriginalTitle, actualMovie.OriginalTitle);
            Assert.AreEqual(expectedMovie.LocalTitle, actualMovie.LocalTitle);
            Assert.AreEqual(expectedMovie.DirectorFirstName, actualMovie.DirectorFirstName);
            Assert.AreEqual(expectedMovie.DirectorLastName, actualMovie.DirectorLastName);
            Assert.AreEqual(expectedMovie.OriginalCountry, actualMovie.OriginalCountry);
            Assert.AreEqual(expectedMovie.NationalReleaseDate, actualMovie.NationalReleaseDate);
            Assert.AreEqual(expectedMovie.AlternativeContent, actualMovie.AlternativeContent);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllMovies()
        {
            // Arrange
            var expectedMovies = new List<Movie>
    {
        new Movie
        {
            OriginalTitle = "A casa tutti bene",
            LocalTitle = "Min italienske familie",
            DirectorFirstName = "Gabriele",
            DirectorLastName = "Muccino",
            OriginalCountry = "IT",
            NationalReleaseDate = new DateTime(2019, 1, 3),
            AlternativeContent = false
        },
        new Movie
        {
            OriginalTitle = "A star is born",
            LocalTitle = "A star is born",
            DirectorFirstName = "Bradley",
            DirectorLastName = "Cooper",
            OriginalCountry = "US",
            NationalReleaseDate = new DateTime(1989, 12, 6),
            AlternativeContent = false
        },

         new Movie
        {
            OriginalTitle = "A star is born - ENCORE",
            LocalTitle = "A star is born - ENCORE",
            DirectorFirstName = "Guy",
            DirectorLastName = "Richie",
            OriginalCountry = "US",
            NationalReleaseDate = new DateTime(2019, 3, 21),
            AlternativeContent = false
        },

          new Movie
        {
            OriginalTitle = "Aladdin",
            LocalTitle = "Aladdin",
            DirectorFirstName = "James",
            DirectorLastName = "Cameron",
            OriginalCountry = "US",
            NationalReleaseDate = new DateTime(2019, 5, 23),
            AlternativeContent = false
        },

           new Movie
        {
            OriginalTitle = "Aliens",
            LocalTitle = "Aliens",
            DirectorFirstName = "Robert",
            DirectorLastName = "Rodriguez",
            OriginalCountry = "US",
            NationalReleaseDate = new DateTime(1986, 10, 31),
            AlternativeContent = false
        }
    };

            var mockReader = new Mock<SqlDataReader>();
            mockReader.SetupSequence(reader => reader.Read())
                      .Returns(true)
                      .Returns(true)
                      .Returns(false); // Simulate two rows in the result set

            // First movie
            mockReader.Setup(reader => reader["OriginalTitle"]).Returns(expectedMovies[0].OriginalTitle);
            mockReader.Setup(reader => reader["LocalTitle"]).Returns(expectedMovies[0].LocalTitle);
            mockReader.Setup(reader => reader["DirectorFirstName"]).Returns(expectedMovies[0].DirectorFirstName);
            mockReader.Setup(reader => reader["DirectorLastName"]).Returns(expectedMovies[0].DirectorLastName);
            mockReader.Setup(reader => reader["OriginalCountry"]).Returns(expectedMovies[0].OriginalCountry);
            mockReader.Setup(reader => reader["NationalReleaseDate"]).Returns(expectedMovies[0].NationalReleaseDate);
            mockReader.Setup(reader => reader["AlternativeContent"]).Returns(expectedMovies[0].AlternativeContent);

            // Second movie
            mockReader.Setup(reader => reader["OriginalTitle"]).Returns(expectedMovies[1].OriginalTitle);
            mockReader.Setup(reader => reader["LocalTitle"]).Returns(expectedMovies[1].LocalTitle);
            mockReader.Setup(reader => reader["DirectorFirstName"]).Returns(expectedMovies[1].DirectorFirstName);
            mockReader.Setup(reader => reader["DirectorLastName"]).Returns(expectedMovies[1].DirectorLastName);
            mockReader.Setup(reader => reader["OriginalCountry"]).Returns(expectedMovies[1].OriginalCountry);
            mockReader.Setup(reader => reader["NationalReleaseDate"]).Returns(expectedMovies[1].NationalReleaseDate);
            mockReader.Setup(reader => reader["AlternativeContent"]).Returns(expectedMovies[1].AlternativeContent);

            // Third movie
            mockReader.Setup(reader => reader["OriginalTitle"]).Returns(expectedMovies[1].OriginalTitle);
            mockReader.Setup(reader => reader["LocalTitle"]).Returns(expectedMovies[1].LocalTitle);
            mockReader.Setup(reader => reader["DirectorFirstName"]).Returns(expectedMovies[1].DirectorFirstName);
            mockReader.Setup(reader => reader["DirectorLastName"]).Returns(expectedMovies[1].DirectorLastName);
            mockReader.Setup(reader => reader["OriginalCountry"]).Returns(expectedMovies[1].OriginalCountry);
            mockReader.Setup(reader => reader["NationalReleaseDate"]).Returns(expectedMovies[1].NationalReleaseDate);
            mockReader.Setup(reader => reader["AlternativeContent"]).Returns(expectedMovies[1].AlternativeContent);

            // Fourth movie
            mockReader.Setup(reader => reader["OriginalTitle"]).Returns(expectedMovies[1].OriginalTitle);
            mockReader.Setup(reader => reader["LocalTitle"]).Returns(expectedMovies[1].LocalTitle);
            mockReader.Setup(reader => reader["DirectorFirstName"]).Returns(expectedMovies[1].DirectorFirstName);
            mockReader.Setup(reader => reader["DirectorLastName"]).Returns(expectedMovies[1].DirectorLastName);
            mockReader.Setup(reader => reader["OriginalCountry"]).Returns(expectedMovies[1].OriginalCountry);
            mockReader.Setup(reader => reader["NationalReleaseDate"]).Returns(expectedMovies[1].NationalReleaseDate);
            mockReader.Setup(reader => reader["AlternativeContent"]).Returns(expectedMovies[1].AlternativeContent);

            // Fifth movie
            mockReader.Setup(reader => reader["OriginalTitle"]).Returns(expectedMovies[1].OriginalTitle);
            mockReader.Setup(reader => reader["LocalTitle"]).Returns(expectedMovies[1].LocalTitle);
            mockReader.Setup(reader => reader["DirectorFirstName"]).Returns(expectedMovies[1].DirectorFirstName);
            mockReader.Setup(reader => reader["DirectorLastName"]).Returns(expectedMovies[1].DirectorLastName);
            mockReader.Setup(reader => reader["OriginalCountry"]).Returns(expectedMovies[1].OriginalCountry);
            mockReader.Setup(reader => reader["NationalReleaseDate"]).Returns(expectedMovies[1].NationalReleaseDate);
            mockReader.Setup(reader => reader["AlternativeContent"]).Returns(expectedMovies[1].AlternativeContent);

            // Act
            var actualMovies = repository.GetAll().ToList();

            // Assert
            Assert.AreEqual(expectedMovies.Count, actualMovies.Count);
            for (int i = 0; i < expectedMovies.Count; i++)
            {
                Assert.AreEqual(expectedMovies[i].OriginalTitle, actualMovies[i].OriginalTitle);
                Assert.AreEqual(expectedMovies[i].LocalTitle, actualMovies[i].LocalTitle);
                Assert.AreEqual(expectedMovies[i].DirectorFirstName, actualMovies[i].DirectorFirstName);
                Assert.AreEqual(expectedMovies[i].DirectorLastName, actualMovies[i].DirectorLastName);
                Assert.AreEqual(expectedMovies[i].OriginalCountry, actualMovies[i].OriginalCountry);
                Assert.AreEqual(expectedMovies[i].NationalReleaseDate, actualMovies[i].NationalReleaseDate);
                Assert.AreEqual(expectedMovies[i].AlternativeContent, actualMovies[i].AlternativeContent);
            }
        }

    }

    public class ShowRepositoryTests
    {
        private Mock<SqlConnection> _mockConnection;
        private Mock<SqlCommand> _mockCommand;
        private ShowRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            // Arrange: Mock the SQL connection and command
            _mockConnection = new Mock<SqlConnection>();
            _mockCommand = new Mock<SqlCommand>();

            mockCommand.Setup(cmd => cmd.ExecuteNonQuery()).Verifiable();
            mockConnection.Setup(conn => conn.CreateCommand()).Returns(mockCommand.Object);

            mockCommand.Setup(cmd => cmd.ExecuteReader()).Returns(mockReader.Object);

            var connectionString = "TestConnectionString";
            _repository = new ShowRepository(connectionString);
        }

        [TestMethod]
        public void Add_ShouldAddShowToDatabase()
        {
            // Arrange
            var show = new Show
            {
                Date = new DateTime(2019, 1, 1),
                Version = 3,
                ScreeningFormat = 1,
                Category = "Adventure",                
                RoomNumber = 1
            };

            // Act
            _repository.Add(show);

            // Assert: Here you would verify that the Add method invoked the expected SQL commands.
            _mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Once);
        }

        [TestMethod]
        public void Delete_ShoulDeleteShowFromDatabase()
        {
            // Arrange
            var showId = 25;

            // Act
            _repository.Delete(showId);

            // Assert: Here you would verify that the Add method invoked the expected SQL commands.
            mockCommand.Verify(cmd => cmd.Parameters.AddWithValue("@ShowId", showId), Times.Once);
            _mockCommand.Verify(cmd => cmd.ExecuteNonQuery(), Times.Once);
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectShow()
        {
            // Arrange
            var showId = 21;
            var expectedShow = new Show
            {
                Date = new DateTime(2019, 1, 12),
                Version = 3,
                ScreeningFormat = 1,
                Category = "Europa Kino",
                RoomNumber = 1
            };

            var mockReader = new Mock<SqlDataReader>();
            mockReader.SetupSequence(reader => reader.Read())
                      .Returns(true)
                      .Returns(false); // Simulate a single row in the result set
            mockReader.Setup(reader => reader["Date"]).Returns(expectedShow.Date);
            mockReader.Setup(reader => reader["Version"]).Returns(expectedShow.Version);
            mockReader.Setup(reader => reader["ScreeningFormat"]).Returns(expectedShow.ScreeningFormat);
            mockReader.Setup(reader => reader["Category"]).Returns(expectedShow.Category);
            mockReader.Setup(reader => reader["RoomNumber"]).Returns(expectedShow.RoomNumber);

            // Act
            var actualShow = repository.GetById(showId);

            // Assert
            Assert.IsNotNull(actualShow);
            Assert.AreEqual(expectedShow.Date, actualShow.Date);
            Assert.AreEqual(expectedShow.Version, actualShow.Version);
            Assert.AreEqual(expectedShow.ScreeningFormat, actualShow.ScreeningFormat);
            Assert.AreEqual(expectedShow.Category, actualShow.Category);
            Assert.AreEqual(expectedShow.RoomNumber, actualShow.RoomNumber);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllShows()
        {
            // Arrange
            var expectedShows = new List<Show>
    {
        new Show
        {
                Date = new DateTime(2019, 1, 12),
                Version = 3,
                ScreeningFormat = 1,
                Category = "Europa Kino",
                RoomNumber = 1
        },
        new Show
        {
                Date = new DateTime(2019, 1, 24),
                Version = 3,
                ScreeningFormat = 1,
                Category = "Drama",
                RoomNumber = 2
        },

         new Show
        {
                Date = new DateTime(2019, 1, 12),
                Version = 3,
                ScreeningFormat = 1,
                Category = "Drama",
                RoomNumber = 3
        },

          new Show
        {
                Date = new DateTime(2019, 5, 23),
                Version = 2,
                ScreeningFormat = 1,
                Category = "Børnebiffen",
                RoomNumber = 1
        },

           new Show
        {
                Date = new DateTime(2019, 1, 25),
                Version = 3,
                ScreeningFormat = 3,
                Category = "Action",
                RoomNumber = 3
        }
    };

            var mockReader = new Mock<SqlDataReader>();
            mockReader.SetupSequence(reader => reader.Read())
                      .Returns(true)
                      .Returns(true)
                      .Returns(false); // Simulate two rows in the result set

            // First show
            mockReader.Setup(reader => reader["Date"]).Returns(expectedShows[0].Date);
            mockReader.Setup(reader => reader["Version"]).Returns(expectedShows[0].Version);
            mockReader.Setup(reader => reader["ScreeningFormat"]).Returns(expectedShows[0].ScreeningFormat);
            mockReader.Setup(reader => reader["Category"]).Returns(expectedShows[0].Category);
            mockReader.Setup(reader => reader["RoomNumber"]).Returns(expectedShows[0].RoomNumber);

            // Second show
            mockReader.Setup(reader => reader["Date"]).Returns(expectedShows[0].Date);
            mockReader.Setup(reader => reader["Version"]).Returns(expectedShows[0].Version);
            mockReader.Setup(reader => reader["ScreeningFormat"]).Returns(expectedShows[0].ScreeningFormat);
            mockReader.Setup(reader => reader["Category"]).Returns(expectedShows[0].Category);
            mockReader.Setup(reader => reader["RoomNumber"]).Returns(expectedShows[0].RoomNumber);

            // Third show
            mockReader.Setup(reader => reader["Date"]).Returns(expectedShows[0].Date);
            mockReader.Setup(reader => reader["Version"]).Returns(expectedShows[0].Version);
            mockReader.Setup(reader => reader["ScreeningFormat"]).Returns(expectedShows[0].ScreeningFormat);
            mockReader.Setup(reader => reader["Category"]).Returns(expectedShows[0].Category);
            mockReader.Setup(reader => reader["RoomNumber"]).Returns(expectedShows[0].RoomNumber);

            // Fourth show
            mockReader.Setup(reader => reader["Date"]).Returns(expectedShows[0].Date);
            mockReader.Setup(reader => reader["Version"]).Returns(expectedShows[0].Version);
            mockReader.Setup(reader => reader["ScreeningFormat"]).Returns(expectedShows[0].ScreeningFormat);
            mockReader.Setup(reader => reader["Category"]).Returns(expectedShows[0].Category);
            mockReader.Setup(reader => reader["RoomNumber"]).Returns(expectedShows[0].RoomNumber);

            // Fifth show
            mockReader.Setup(reader => reader["Date"]).Returns(expectedShows[0].Date);
            mockReader.Setup(reader => reader["Version"]).Returns(expectedShows[0].Version);
            mockReader.Setup(reader => reader["ScreeningFormat"]).Returns(expectedShows[0].ScreeningFormat);
            mockReader.Setup(reader => reader["Category"]).Returns(expectedShows[0].Category);
            mockReader.Setup(reader => reader["RoomNumber"]).Returns(expectedShows[0].RoomNumber);

            // Act
            var actualShows = repository.GetAll().ToList();

            // Assert
            Assert.AreEqual(expectedShows.Count, actualShows.Count);
            for (int i = 0; i < expectedShows.Count; i++)
            {
                Assert.AreEqual(expectedShows[i].Date, actualShows[i].Date);
                Assert.AreEqual(expectedShows[i].Version, actualShows[i].Version);
                Assert.AreEqual(expectedShows[i].ScreeningFormat, actualShows[i].ScreeningFormat);
                Assert.AreEqual(expectedShows[i].Category, actualShows[i].Category);
                Assert.AreEqual(expectedShows[i].RoomNumber, actualShows[i].RoomNumber);
            }
        }


    }

}