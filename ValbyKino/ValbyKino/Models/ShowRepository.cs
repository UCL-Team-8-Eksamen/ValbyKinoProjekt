using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValbyKino.Models
{
    public class ShowRepository : IRepository<Show>
    {
        public ObservableCollection<Show> Shows { get; set; }
        public ShowRepository() 
        {
            Shows = new ObservableCollection<Show>();
        }
        public void Add(Show entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Show> GetAll()
        {
            throw new NotImplementedException();
        }

        public Show GetById(int id)
        {
            throw new NotImplementedException();
        }

        //public void Update(Show entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
