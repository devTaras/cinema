using Cinema.Web.Models.Base;
using System.Collections.Generic;

namespace Cinema.Web.Services
{
    public interface IListService<T> where T:ItemBase
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        void Edit(T item);
        void Delete(int id);
        T? Find(int id);
    }

    public class ListService<T> : IListService<T> where T : ItemBase
    {
        private List<T> items = new List<T>();

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Delete(int id)
        {
            items.Remove(Find(id));
        }

        public void Edit(T item)
        {
            Delete(item.Id);
            Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return items;
        }

        public T? Find(int id)
        {
            return items.Find(x => x.Id == id);
        }
    }
}
