using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValbyKino.Models
{
    public class ShowingRepository : IRepository<Showing>
    {
        public ObservableCollection<Showing> Showings { get; set; }
        public ShowingRepository() 
        {
            Showings = new ObservableCollection<Showing>();
        }
        public void Add(Showing entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Showing> GetAll()
        {
            throw new NotImplementedException();
        }

        public Showing GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Showing entity)
        {
            throw new NotImplementedException();
        }
    }
}
