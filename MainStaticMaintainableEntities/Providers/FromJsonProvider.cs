using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace MainStaticMaintainableEntities.Providers
{
    public class FromJsonRepository<T> : IRepository<T>
        where T : PersistantEntity
    {
        #region Public Fields
        public string Path { get; set; }
        public DateTime NextFlush { get; private set; }

        private IRepository<T> _alternateRepository;
        public IRepository<T> AlternateRepository
        {
            get => _alternateRepository;
            set
            {
                _alternateRepository = value;
                if (value != null)
                {
                    _myList = AlternateRepository.GetAll().ToList();
                    Task.Run(Flush); // keep a local version as backup;
                }
            }
        }
        #endregion


        private static List<T> _myList;

        public FromJsonRepository(string _path)
        {
            // pretty long...
            Path = _path;
            NextFlush = DateTime.UtcNow.AddSeconds(1); // writes only once every 1 seconds
            if (string.IsNullOrWhiteSpace(_path) || !File.Exists(_path))
            {
                _myList = new List<T>();
                return;
            }
            //else
            string[] jsonStringLines = File.ReadAllLines(_path);
            StringBuilder sb = new StringBuilder();
            foreach (string line in jsonStringLines)
            {
                sb.Append(line);
            }


            try
            {
                _myList = JsonFormatter.FromJson<List<T>>(sb.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public FromJsonRepository(IRepository<T> anotherRepository, string path)
        {
            this.AlternateRepository = anotherRepository; // includes load and flush to text file.
            Path = path;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Task.Run(Flush);
            if (AlternateRepository != null)
                AlternateRepository.Delete(entity);
        }

        public IEnumerable<T> GetAll()
        {
            Task.Run(Flush);
            return _myList;
        }

        public IEnumerable<T> GetByExpression(Expression<Func<T, bool>> expression)
        {
            Task.Run(Flush);
            var func = expression.Compile();
            return _myList.Where(x => func(x));
        }

        public void Insert(T entity)
        {
            var exist = _myList.FirstOrDefault(x => x.Id == entity.Id);
            if (exist == null)
                _myList.Add(entity);
            Task.Run(Flush);
            if (AlternateRepository != null)
                AlternateRepository.Insert(entity);
        }

        public void Update(T entity)
        {
            var exist = _myList.FirstOrDefault(x => x.Id == entity.Id);
            if (exist == null)
                Insert(entity);
            else
            {
                _myList.Remove(exist);
                _myList.Add(entity);
            }

            Task.Run(Flush);
            if (AlternateRepository != null)
                AlternateRepository.Update(entity);
        }

        public T GetById(int Id)
        {
            var exist = _myList.FirstOrDefault(x => x.Id == Id);
            Task.Run(Flush);
            return exist;
        }

        private void Flush()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Path))
                    return;
                if (DateTime.UtcNow > NextFlush)
                {
                    NextFlush = DateTime.UtcNow.AddSeconds(1);
                    string whatToWrite = JsonFormatter.ToJson<List<T>>(_myList);
                    NextFlush = DateTime.UtcNow.AddSeconds(1);
                    File.WriteAllText(Path, whatToWrite);
                    NextFlush = DateTime.UtcNow.AddSeconds(1);
                }
                else return; // next time the repo is used it will flush
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
