using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValbyKino.Models;
using System.IO;

namespace RepositoriesTests
{
    [TestClass]
    public class MovieRepositoryTests
    {
        private MovieRepository _movieRepository;

        [TestInitialize]
        public void Setup()
        {
            // Connecting to the database
            string connectionString = "Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;";
            _movieRepository = new MovieRepository(connectionString);
        }

        [TestMethod]
        public void Add_ShouldAddMovieSuccessfully()
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

            // Assert
            var addedMovie = _movieRepository.GetAll().FirstOrDefault(m => m.OriginalTitle == "Aquaman");
            Assert.IsNotNull(addedMovie, "The movie was not added.");
            Assert.AreEqual(movie.OriginalTitle, addedMovie.OriginalTitle);
        }
        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup any movies added with a specific identifier
            var testMovies = _movieRepository.GetAll()
                .Where(m => m.OriginalTitle.StartsWith("Aquaman"));

            foreach (var movie in testMovies)
            {
                _movieRepository.Delete(movie.MovieId);
            }
        }

        [TestMethod]
        public void Delete_ShoulDeleteMovieSuccessfully()
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

            _movieRepository.Add(movie);
            var addedMovie = _movieRepository.GetAll().First(m => m.OriginalTitle == "Aquaman");

            // Act
            _movieRepository.Delete(addedMovie.MovieId);

            // Assert
            var deletedMovie = _movieRepository.GetAll().FirstOrDefault(m => m.MovieID == addedMovie.MovieId);
            Assert.IsNull(deletedMovie, "The movie was not deleted.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup any movies added with a specific identifier
            var testMovies = _movieRepository.GetAll()
                .Where(m => m.OriginalTitle.StartsWith("Aquaman"));

            foreach (var movie in testMovies)
            {
                _movieRepository.Delete(movie.MovieId);
            }
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectMovie()
        {
            // Arrange
            var movie = new Movie
            {
                OriginalTitle = "Artic",
                LocalTitle = "Artic",
                DirectorFirstName = "Joe",
                DirectorLastName = "Penna",
                OriginalCountry = "IS",
                NationalReleaseDate = new DateTime(2019, 1, 31),
                AlternativeContent = false
            };
            _movieRepository.Add(movie);
            var addedMovie = _movieRepository.GetAll().First(m => m.OriginalTitle == "Artic");

            // Act
            var retrievedMovie = _movieRepository.GetById(addedMovie.MovieId);

            // Assert
            Assert.IsNotNull(retrievedMovie, "The movie was not retrieved.");
            Assert.AreEqual(movie.OriginalTitle, retrievedMovie.OriginalTitle);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup any movies added with a specific identifier
            var testMovies = _movieRepository.GetAll()
                .Where(m => m.OriginalTitle.StartsWith("Artic"));

            foreach (var movie in testMovies)
            {
                _movieRepository.Delete(movie.MovieID);
            }
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllMovies()
        {
            //Act
            var movies = _movieRepository.GetAll();

            // Assert
            Assert.IsNotNull(movies, "The movie list should not be null.");
            Assert.IsTrue(movies.Any(), "The movie list should not be empty.");
        }

        [TestMethod]
        public void Update_ShouldUpdateMovieSuccessfully()
        {
            // Arrange
            var originalMovie = new Movie
            {
                OriginalTitle = "OriginalTitle",
                LocalTitle = "OriginalLocalTitle",
                DirectorFirstName = "OriginalFirstName",
                DirectorLastName = "OriginalLastName",
                OriginalCountry = "US",
                NationalReleaseDate = new DateTime(2023, 1, 1),
                AlternativeContent = false
            };
            _movieRepository.Add(originalMovie);

            // Retrieve the added movie to get its MovieID
            var addedMovie = _movieRepository.GetAll().First(m => m.OriginalTitle == "OriginalTitle");

            // Update movie details
            var updatedMovie = new Movie
            {
                MovieID = addedMovie.MovieId, // Ensure the MovieID is carried over
                OriginalTitle = "UpdatedTitle",
                LocalTitle = "UpdatedLocalTitle",
                DirectorFirstName = "UpdatedFirstName",
                DirectorLastName = "UpdatedLastName",
                OriginalCountry = "UK",
                NationalReleaseDate = new DateTime(2024, 1, 1),
                AlternativeContent = true
            };

            // Act
            _movieRepository.Update(updatedMovie);

            // Assert
            var retrievedMovie = _movieRepository.GetById(addedMovie.MovieId);
            Assert.IsNotNull(retrievedMovie, "The updated movie was not retrieved.");
            Assert.AreEqual(updatedMovie.OriginalTitle, retrievedMovie.OriginalTitle, "OriginalTitle was not updated correctly.");
            Assert.AreEqual(updatedMovie.LocalTitle, retrievedMovie.LocalTitle, "LocalTitle was not updated correctly.");
            Assert.AreEqual(updatedMovie.DirectorFirstName, retrievedMovie.DirectorFirstName, "DirectorFirstName was not updated correctly.");
            Assert.AreEqual(updatedMovie.DirectorLastName, retrievedMovie.DirectorLastName, "DirectorLastName was not updated correctly.");
            Assert.AreEqual(updatedMovie.OriginalCountry, retrievedMovie.OriginalCountry, "OriginalCountry was not updated correctly.");
            Assert.AreEqual(updatedMovie.NationalReleaseDate, retrievedMovie.NationalReleaseDate, "NationalReleaseDate was not updated correctly.");
            Assert.AreEqual(updatedMovie.AlternativeContent, retrievedMovie.AlternativeContent, "AlternativeContent was not updated correctly.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup any movies added with a specific identifier
            var testMovies = _movieRepository.GetAll()
                .Where(m => m.OriginalTitle.StartsWith("UpdatedTitle"));

            foreach (var movie in testMovies)
            {
                _movieRepository.Delete(movie.MovieID);
            }
        }
    }

    public class ShowRepositoryTests
    {
        private ShowRepository _showRepository;

        [TestInitialize]
        public void Setup()
        {
            // Connecting to the database
            string connectionString = "Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;";
            _showRepository = new ShowRepository(connectionString);
        }

        [TestMethod]
        public void Add_ShouldAddShowSuccessfully()
        {
            // Arrange
            var show = new Show
            {
                Date = DateTime.Now,
                Time = DateTime.Now,
                Version = Version.VO,
                ScreeningFormat = "2",
                Category = "DramaAdd",
                RoomNumber = 1,
                Price = 89.00,
                Movie = new Movie { MovieID = 16 }
            };

            // Act
            _showRepository.Add(show);

            // Assert: Here you would verify that the Add method invoked the expected SQL commands.
            var addedShow = _showRepository.GetAll().FirstOrDefault(s => s.Category == "DramaAdd");
            Assert.IsNotNull(addedShow, "The show was not added.");
            Assert.AreEqual(show.Category, addedShow.Category);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup any movies added with a specific identifier
            var testShows = _showRepository.GetAll()
                .Where(m => m.Category.StartsWith("DramaAdd"));

            foreach (var show in testShows)
            {
                _showRepository.Delete(show.ShowID);
            }
        }

        [TestMethod]
        public void Delete_ShoulDeleteShowSuccessfully()
        {
            // Arrange
            var show = new Show
            {
                Date = DateTime.Now,
                Time = DateTime.Now,
                Version = Version.VO,
                ScreeningFormat = "2",
                Category = "DramaToDelete",
                RoomNumber = 1,
                Price = 90.00,
                Movie = new Movie { MovieID = 17 }
            };
            _showRepository.Add(show);
            var addedShow = _showRepository.GetAll().First(s => s.Category == "DramaToDelete");

            // Act
            _showRepository.Delete(addedShow.RoomNumber);

            // Assert
            var deletedShow = _showRepository.GetAll().FirstOrDefault(s => s.RoomNumber == addedShow.RoomNumber);
            Assert.IsNull(deletedShow, "The show was not deleted.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup any movies added with a specific identifier
            var testShows = _showRepository.GetAll()
                .Where(m => m.Category.StartsWith("DramaToDelete"));

            foreach (var show in testShows)
            {
                _showRepository.Delete(show.ShowID);
            }
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectShow()
        {
            // Arrange
            var show = new Show
            {
                Date = DateTime.Now,
                Time = DateTime.Now,
                Version = Version.VO,
                ScreeningFormat = "2",
                Category = "DramaById",
                RoomNumber = 1,
                Price = 95.00,
                Movie = new Movie { MovieID = 18 }
            };
            _showRepository.Add(show);
            var addedShow = _showRepository.GetAll().First(s => s.Category == "DramaById");

            // Act
            var retrievedShow = _showRepository.GetById(addedShow.RoomNumber);

            // Assert
            Assert.IsNotNull(retrievedShow, "The show was not retrieved.");
            Assert.AreEqual(show.Category, retrievedShow.Category);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup any movies added with a specific identifier
            var testShows = _showRepository.GetAll()
                .Where(m => m.Category.StartsWith("DramaById"));

            foreach (var show in testShows)
            {
                _showRepository.Delete(show.ShowID);
            }
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllShows()
        {
            // Act
            var shows = _showRepository.GetAll();

            // Assert
            Assert.IsNotNull(shows, "The show list should not be null.");
            Assert.IsTrue(shows.Any(), "The show list should not be empty.");
        }
    }
}


    