using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace ValbyKino.Models
{
    public class MovieRepository : IRepository<Movie>
    {
        public ObservableCollection<Movie> Movies { get; set; }

        private readonly string _connectionString;

        public MovieRepository(string connectionString)
        {
            Movies = new ObservableCollection<Movie>();
            _connectionString = connectionString;
        }

        public void Add(Movie movie)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("uspCreateMovie", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OriginalTitle", movie.OriginalTitle);
                command.Parameters.AddWithValue("@LocalTitle", movie.LocalTitle);
                command.Parameters.AddWithValue("@OriginalCountry", movie.OriginalCountry);
                command.Parameters.AddWithValue("@NationalReleaseDate", movie.NationalReleaseDate);
                command.Parameters.AddWithValue("@AlternativeContent", movie.AlternativeContent);
                command.Parameters.AddWithValue("@DirectorFirstName", movie.DirectorFirstName);
                command.Parameters.AddWithValue("@DirectorLastName", movie.DirectorLastName);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(int id)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("uspRemoveMovie", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MovieID", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            var movies = new ObservableCollection<Movie>();
            string query = "SELECT * FROM vmovie_director;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie
                        {
                            OriginalTitle = (string)reader["OriginalTitle"],
                            LocalTitle = (string)reader["LocalTitle"],
                            DirectorFirstName = (string)reader["DirectorFirstName"],
                            DirectorLastName = (string)reader["DirectorLastName"],
                            OriginalCountry = (string)reader["OriginalCountry"],
                            NationalReleaseDate = (DateTime)reader["NationalReleaseDate"],
                            AlternativeContent = (bool)reader["AlternativeContent"]
                        });
                    }
                }
            }

            return movies;
        }

        public Movie GetById(int id)
        {
            Movie movie = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("uspGetMovieByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MovieID", id);
                command.ExecuteNonQuery();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        movie = new Movie
                        {
                            OriginalTitle = (string)reader["OriginalTitle"],
                            LocalTitle = (string)reader["LocalTitle"],
                            DirectorFirstName = (string)reader["DirectorFirstName"],
                            DirectorLastName = (string)reader["DirectorLastName"],
                            OriginalCountry = (string)reader["OriginalCountry"],
                            NationalReleaseDate = (DateTime)reader["NationalReleaseDate"],
                            AlternativeContent = (bool)reader["AlternativeContent"]
                        };
                    }
                }
            }

            return movie;
        }

        public void Update(Movie movie)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("uspUpdateMovie", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OriginalTitle", movie.OriginalTitle);
                command.Parameters.AddWithValue("@LocalTitle", movie.LocalTitle);
                command.Parameters.AddWithValue("@OriginalCountry", movie.OriginalCountry);
                command.Parameters.AddWithValue("@NationalReleaseDate", movie.NationalReleaseDate);
                command.Parameters.AddWithValue("@AlternativeContent", movie.AlternativeContent);
                command.Parameters.AddWithValue("@DirectorFirstName", movie.DirectorFirstName);
                command.Parameters.AddWithValue("@DirectorLastName", movie.DirectorLastName);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
