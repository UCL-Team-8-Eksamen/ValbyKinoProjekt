using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValbyKino.Models
{
    public class MovieRepository : IRepository<Movie>
    {
        public ObservableCollection<Movie> Movies { get; set; }
        public MovieRepository() 
        {
            Movies = new ObservableCollection<Movie>();

        }
        public void Add(Movie entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetAll()
        {
            throw new NotImplementedException();
        }

        public Movie GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Movie entity)
        {
            throw new NotImplementedException();
        }
    }
}
