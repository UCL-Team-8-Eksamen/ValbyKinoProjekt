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
            _showViewModel.ScreeningFormat = "2";
            _showViewModel.Category = "DramaToAdd";
            _showViewModel.RoomNumber = 1;
            _showViewModel.Price = 89.00;

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
            var testShow = new Show(DateTime.Now, DateTime.Now, Version.VO, "2", "DramaToDelete", 1, 90.00);
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

    [TestClass]
    public class AnnualReportViewModelTests
    {
        private AnnualReportViewModel _annualReportViewModel;
        private string testCsvFile = "test_movies.csv";

        [TestInitialize]
        public void Setup()
        {
            // Create a test CSV file for the Report class
            File.WriteAllText(testCsvFile, "OriginalTitle,LocalTitle,DirectorFirstName,DirectorLastName,OriginalCountry,NationalReleaseDate,Date,Version,ScreeningFormat,3D,AltContent,NbWeeks,TotalScreenings,Admissions,BoxOffice,YA\n" +
                                          "Title1,LocalTitle1,John,Doe,US,2023-01-01,2023-01-15,VO,2D,,0,10,50,1000,2000,1\n" +
                                          "Title2,LocalTitle2,Jane,Smith,DK,2023-02-01,2023-02-10,DB,3D,,0,5,20,500,1500,0");

            _annualReportViewModel = new AnnualReportViewModel();
        }

        [TestMethod]
        public void AnnualReportViewModel_ShouldPopulateReportListOnInitialization()
        {
            // Arrange & Act
            var reportList = _annualReportViewModel.ReportList;

            // Assert
            Assert.IsNotNull(reportList, "ReportList should not be null after initialization.");
            Assert.AreEqual(5, reportList.Count, "ReportList should contain 2 items based on the test CSV file.");
            Assert.AreEqual(15, reportList[0].AmountOfWeeks, "First report AmountOfWeeks is incorrect.");
            Assert.AreEqual(6300, reportList[1].BoxOffice, "Second report BoxOffice is incorrect.");
        }

        [TestMethod]
        public void DownloadReportCommand_ShouldExecuteWithoutErrors()
        {
            // Arrange & Act
            try
            {
                _annualReportViewModel.DownloadReportCommand.Execute(null);
            }
            catch (Exception ex)
            {
                Assert.Fail($"DownloadReportCommand threw an exception: {ex.Message}");
            }

            // Assert
            Assert.IsTrue(File.Exists(testCsvFile), "The CSV file should exist after executing DownloadReportCommand.");
        }

        [TestMethod]
        public void ReportCollectionView_ShouldRaisePropertyChangedEvent()
        {
            // Arrange
            bool propertyChangedRaised = false;

            _annualReportViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_annualReportViewModel.ReportCollectionView))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            _annualReportViewModel.ReportCollectionView = null;

            // Assert
            Assert.IsTrue(propertyChangedRaised, "PropertyChanged event should be raised when ReportCollectionView is set.");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Clean up test file
            if (File.Exists(testCsvFile))
            {
                File.Delete(testCsvFile);
            }
        }
    }

    [TestClass]
    public class MainWindowViewModelTests
    {
        private MainWindowViewModel _mainWindowViewModel;

        [TestInitialize]
        public void Setup()
        {
            // Arrange: Initialize the ViewModel before each test
            _mainWindowViewModel = new MainWindowViewModel();
        }

        [TestMethod]
        public void MainWindowViewModel_ShouldInitializeWithLoginView()
        {
            // Assert: Verify that the default view is LoginView
            Assert.IsNotNull(_mainWindowViewModel.CurrentView);
            Assert.IsInstanceOfType(_mainWindowViewModel.CurrentView, typeof(LoginView),
                "CurrentView should be initialized to LoginView.");
        }

        [TestMethod]
        public void ShowMovieViewCommand_ShouldUpdateCurrentViewToMovieView()
        {
            // Act: Execute the ShowMovieViewCommand
            _mainWindowViewModel.ShowMovieViewCommand.Execute(null);

            // Assert: Verify that CurrentView is updated to MovieView
            Assert.IsInstanceOfType(_mainWindowViewModel.CurrentView, typeof(MovieView),
                "CurrentView should be updated to MovieView.");
        }

        [TestMethod]
        public void ShowShowViewCommand_ShouldUpdateCurrentViewToShowView()
        {
            // Act: Execute the ShowShowViewCommand
            _mainWindowViewModel.ShowShowViewCommand.Execute(null);

            // Assert: Verify that CurrentView is updated to ShowView
            Assert.IsInstanceOfType(_mainWindowViewModel.CurrentView, typeof(ShowView),
                "CurrentView should be updated to ShowView.");
        }

        [TestMethod]
        public void ShowAnnualReportViewCommand_ShouldUpdateCurrentViewToAnnualReportView()
        {
            // Act: Execute the ShowAnnualReportViewCommand
            _mainWindowViewModel.ShowAnnualReportViewCommand.Execute(null);

            // Assert: Verify that CurrentView is updated to AnnualReportView
            Assert.IsInstanceOfType(_mainWindowViewModel.CurrentView, typeof(AnnualReportView),
                "CurrentView should be updated to AnnualReportView.");
        }

        [TestMethod]
        public void CurrentView_ShouldRaisePropertyChangedEvent()
        {
            // Arrange
            bool propertyChangedRaised = false;

            _mainWindowViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_mainWindowViewModel.CurrentView))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act: Change the CurrentView
            _mainWindowViewModel.ShowMovieViewCommand.Execute(null);

            // Assert: Verify that the PropertyChanged event was raised
            Assert.IsTrue(propertyChangedRaised, "PropertyChanged event should be raised when CurrentView changes.");
        }
    }
}
