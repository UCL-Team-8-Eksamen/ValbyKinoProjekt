using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ValbyKino.Models
{
    public class ShowRepository : IRepository<Show>
    {
        public ObservableCollection<Show> Shows { get; set; }
        IRepository<Movie> movieRepository = new MovieRepository("Server=localhost;Database=ValbyKinoBilletsystem;Trusted_Connection=True;TrustServerCertificate=true;");

        private readonly string _connectionString;
        public ShowRepository(string connectionString)
        {
            Shows = new ObservableCollection<Show>();
            _connectionString = connectionString;
        }
        public void Add(Show show)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("uspCreateShow", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Date", show.Date);
                command.Parameters.AddWithValue("@Time", show.Time);
                command.Parameters.AddWithValue("@Version", show.Version.ToString());
                command.Parameters.AddWithValue("@ScreeningFormat", show.ScreeningFormat);
                command.Parameters.AddWithValue("@Category", show.Category);
                command.Parameters.AddWithValue("@RoomNumber", show.RoomNumber);
                command.Parameters.AddWithValue("@MovieID", show.Movie.MovieID);
                command.ExecuteNonQuery();
                connection.Close();

            }

        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("uspRemoveShow", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ShowID", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public IEnumerable<Show> GetAll()
        {
            var shows = new ObservableCollection<Show>();
            string query = "SELECT * FROM vShow;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shows.Add(new Show
                        {
                            Date = (DateTime)reader["Date"],
                            Version = (Version)Enum.Parse(typeof(Version), (string)reader["Version"]),
                            ScreeningFormat = (string)reader["ScreeningFormat"],
                            Category = (string)reader["Category"],
                            RoomNumber = (int)reader["RoomNumber"],
                            Movie = movieRepository.GetById((int)reader["MovieId"]),
                            Admissions = (int)reader["Admissions"],
                            Price = (int)reader["Price"]
                        });
                    }
                }
            }

            return shows;
        }


        public Show GetById(int id)
        {
            Show show = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("uspGetShowByID", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ShowID", id);
                command.ExecuteNonQuery();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        show = new Show
                        {
                            Date = (DateTime)reader["Date"],
                            Version = (Version)reader["Version"],
                            ScreeningFormat = (string)reader["ScreeningFormat"],
                            Category = (string)reader["Category"],
                            RoomNumber = (int)reader["RoomNumber"],
                            Movie = movieRepository.GetById((int)reader["MovieId"])
                        };
                    }
                }
                return show;
            }
        }

        //public IEnumerable<Show> GetShowsByMovie(Movie movie)
        //{
        //    var shows = new ObservableCollection<Show>();
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand("uspShowsFromMovie", connection);
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.AddWithValue("@MovieID", movie.MovieID);
        //        command.ExecuteNonQuery();

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                shows.Add(new Show
        //                {
        //                    Date = (DateTime)reader["Date"],
        //                    Version = (Version)Enum.Parse(typeof(Version), (string)reader["Version"]),
        //                    ScreeningFormat = (string)reader["ScreeningFormat"],
        //                    Category = (string)reader["Category"],
        //                    RoomNumber = (int)reader["RoomNumber"],
        //                    Movie = movieRepository.GetById((int)reader["MovieId"]),
        //                    Admissions = (int)reader["Admissions"],
        //                    Price = (int)reader["Price"]
        //                });
        //            }
        //        }
        //    }

        //    return shows;
        //}

        //public void Update(Show entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}