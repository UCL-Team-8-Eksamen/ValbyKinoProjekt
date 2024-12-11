using Microsoft.VisualStudio.TestTools.UnitTesting;
using ValbyKino.ViewModels;
using ValbyKino.Models;
using System.Collections.ObjectModel;
using System;

namespace ViewModelTests
{
    [TestClass]
    public class MovieViewModelTests
    {
        private MovieViewModel _movieViewModel;

        [TestInitialize]
        public void Setup()
        {
            _movieViewModel = new MovieViewModel();
        }

        [TestMethod]
        public void AddMovieCommand_ShouldAddMovieToMoviesCollection()
        {
            // Arrange
            _movieViewModel.OriginalTitle = "Aquaman";
            _movieViewModel.LocalTitle = "Aquaman";
            _movieViewModel.DirectorFirstName = "James";
            _movieViewModel.DirectorLastName = "Wan";
            _movieViewModel.OriginalCountry = "AU";
            _movieViewModel.NationalReleaseDate = new DateTime(2018, 12, 13);
            _movieViewModel.AlternativeContent = false;

            // Act
            _movieViewModel.AddMovieCommand.Execute(null);

            // Assert
            Assert.AreEqual(1, _movieViewModel.Movies.Count, "The movie collection should contain 1 item.");
            var addedMovie = _movieViewModel.Movies[0];
            Assert.AreEqual("Aquaman", addedMovie.OriginalTitle, "The OriginalTitle is incorrect.");
            Assert.AreEqual("Aquaman", addedMovie.LocalTitle, "The LocalTitle is incorrect.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Remove all test movies starting with "Aquaman"
            var testMovies = _movieViewModel.Movies.Where(m => m.OriginalTitle.StartsWith("Aquaman")).ToList();
            foreach (var movie in testMovies)
            {
                _movieViewModel.Movies.Remove(movie);
            }
        }

        [TestMethod]
        public void DeleteMovieCommand_ShouldRemoveSelectedMovieFromMoviesCollection()
        {
            // Arrange
            var testMovie = new Movie("MovieToDelete", "LocalMovie", "FirstName", "LastName", "IS", DateTime.Now, false);
            _movieViewModel.Movies.Add(testMovie);
            _movieViewModel.SelectedItem = testMovie;

            // Act
            _movieViewModel.DeleteMovieCommand.Execute(null);

            // Assert
            Assert.AreEqual(0, _movieViewModel.Movies.Count, "The movie should be removed from the collection.");
        }

        public void Cleanup()
        {
            // Remove all test movies starting with "Aquaman"
            var testMovies = _movieViewModel.Movies.Where(m => m.OriginalTitle.StartsWith("MovieToDelete")).ToList();
            foreach (var movie in testMovies)
            {
                _movieViewModel.Movies.Remove(movie);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
            }
        }

        [TestMethod]
        public void SelectedItem_ShouldRaisePropertyChangedEvent()
        {
            // Arrange
            bool propertyChangedRaised = false;
            _movieViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_movieViewModel.SelectedItem))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            _movieViewModel.SelectedItem = new Movie();

            // Assert
            Assert.IsTrue(propertyChangedRaised, "PropertyChanged event should be raised when SelectedItem is set.");
        }

        [TestMethod]
        public void DeleteMovieCommand_ShouldBeEnabledOnlyWhenSelectedItemIsNotNull()
        {
            // Arrange
            var testMovie = new Movie();
            _movieViewModel.SelectedItem = testMovie;

            // Assert before action
            Assert.IsTrue(_movieViewModel.DeleteMovieCommand.CanExecute(null), "DeleteMovieCommand should be enabled when an item is selected.");

            // Act
            _movieViewModel.SelectedItem = null;

            // Assert after action
            Assert.IsFalse(_movieViewModel.DeleteMovieCommand.CanExecute(null), "DeleteMovieCommand should be disabled when no item is selected.");
        }
    }

    [TestClass]
    public class ShowViewModelTests
    {
        private ShowViewModel _showViewModel;

        [TestInitialize]
        public void Setup()
        {
            _showViewModel = new ShowViewModel();
        }

        [TestMethod]
        public void AddShowCommand_ShouldAddShowToShowsCollection()
        {
            // Arrange
            _showViewModel.Date = new DateTime(2024, 1, 1);
            _showViewModel.Time = new DateTime(1, 1, 1, 18, 30, 0); // Time-only
            _showViewModel.Version = Version.VO;
            _showViewModel.ScreeningFormat = 2;
            _showViewModel.Category = "DramaToAdd";
            _showViewModel.RoomNumber = 1;

            // Act
            _showViewModel.AddShowCommand.Execute(null);

            // Assert
            Assert.AreEqual(1, _showViewModel.Shows.Count, "The show collection should contain 1 item.");
            var addedShow = _showViewModel.Shows[0];
            Assert.AreEqual(new DateTime(2024, 1, 1), addedShow.Date, "The Date is incorrect.");
            Assert.AreEqual("DramaToAdd", addedShow.Category, "The Category is incorrect.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Remove all test shows with Category starting with "Test"
            var testShows = _showViewModel.Shows.Where(s => s.Category.StartsWith("DramaToAdd")).ToList();
            foreach (var show in testShows)
            {
                _showViewModel.Shows.Remove(show);
            }
        }

        [TestMethod]
        public void DeleteShowCommand_ShouldRemoveSelectedShowFromShowsCollection()
        {
            // Arrange
            var testShow = new Show(DateTime.Now, DateTime.Now, Version.VO, 2, "DramaToDelete", 1);
            _showViewModel.Shows.Add(testShow);
            _showViewModel.SelectedItem = testShow;

            // Act
            _showViewModel.DeleteShowCommand.Execute(null);

            // Assert
            Assert.AreEqual(0, _showViewModel.Shows.Count, "The show should be removed from the collection.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Remove all test shows with Category starting with "Test"
            var testShows = _showViewModel.Shows.Where(s => s.Category.StartsWith("DramaToDelete")).ToList();
            foreach (var show in testShows)
            {
                _showViewModel.Shows.Remove(show);
            }
        }

        [TestMethod]
        public void SelectedItem_ShouldRaisePropertyChangedEvent()
        {
            // Arrange
            bool propertyChangedRaised = false;
            _showViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_showViewModel.SelectedItem))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            _showViewModel.SelectedItem = new Show();

            // Assert
            Assert.IsTrue(propertyChangedRaised, "PropertyChanged event should be raised when SelectedItem is set.");
        }

        [TestMethod]
        public void DeleteShowCommand_ShouldBeEnabledOnlyWhenSelectedItemIsNotNull()
        {
            // Arrange
            var testShow = new Show();
            _showViewModel.SelectedItem = testShow;

            // Assert before action
            Assert.IsTrue(_showViewModel.DeleteShowCommand.CanExecute(null), "DeleteShowCommand should be enabled when an item is selected.");

            // Act
            _showViewModel.SelectedItem = null;

            // Assert after action
            Assert.IsFalse(_showViewModel.DeleteShowCommand.CanExecute(null), "DeleteShowCommand should be disabled when no item is selected.");
        }
    }
}
